using System;

namespace Lab4Client
{
    class Program
    {
        public static bool Calc(Lab4Service.Lab4ServiceClient client)
        {
            
            Console.Write("\nInput the first Date in next format:\n" +
               "DD/MM/YYYY : ");
            string dateinp = Console.ReadLine();
            DateTime date1;
            while (!(DateTime.TryParse(dateinp, out date1)))
            {
                Console.WriteLine("  Unable to parse '{0}'.", dateinp);
                Console.Write("\nInput the first Date in next format:\n" +
               "DD/MM/YYYY : ");
                dateinp = Console.ReadLine();
            }
            Console.Write("Input the second Date in next format:\n" +
               "DD/MM/YYYY : ");

            dateinp = Console.ReadLine();
            DateTime date2;
            while (!(DateTime.TryParse(dateinp, out date2)))
            {
                Console.WriteLine("  Unable to parse '{0}'.", dateinp);
                Console.Write("Input the second Date in next format:\n" +
               "DD/MM/YYYY : ");
                dateinp = Console.ReadLine();
            }

            try
            {
                Console.WriteLine(client.Calc(date1, date2));
            }
            catch (System.ServiceModel.CommunicationException e)
            {
                Console.WriteLine("There is no Host right now!");
                Console.ReadKey();
                return false;
            }
            return true;
        }
        public static bool Check(Lab4Service.Lab4ServiceClient client)
        {
            Console.Write("\nInput the year in next format:\n" +
                "YYYY : ");
            string yearInput = Console.ReadLine();

            int year;

            while (!int.TryParse(yearInput, out year) || !(year >= 1 && year <= 9999))
            {
                Console.WriteLine("Wrong input, Year must be in interval (1, 9999), Try again");
                yearInput = Console.ReadLine();
            }
            try
            {
                Console.WriteLine(client.Check(year));
            }
            catch (System.ServiceModel.CommunicationException e)
            {
                Console.WriteLine("There is no Host right now!");
                Console.ReadKey();
                return false;
            }

            return true;
        }
        public static bool Day(Lab4Service.Lab4ServiceClient client)
        {
            Console.Write("\nInput the Date in next format:\n" +
               "DD/MM/YYYY : ");
            string dateinp = Console.ReadLine();
            DateTime date1;
            while (!(DateTime.TryParse(dateinp, out date1)))
            {
                Console.WriteLine("  Unable to parse '{0}'.", dateinp);
                Console.Write("\nInput the Date in next format:\n" +
               "DD/MM/YYYY : ");
                dateinp = Console.ReadLine();
            }
            
            try
            {
                Console.WriteLine(client.Day(date1));
            }
            catch (System.ServiceModel.CommunicationException e)
            {
                Console.WriteLine("There is no Host right now!");
                Console.ReadKey();
                return false;
            }


            return true;
        }
        public static bool LastQuery(Lab4Service.Lab4ServiceClient client)
        {
            try
            {
                Console.WriteLine(client.SerializeFrom());
            }
            catch (System.ServiceModel.CommunicationException e)
            {
                Console.WriteLine("There is no Host right now!");
                Console.ReadKey();
                return false;
            }

            return true;
        }
        public static bool Quit()
        {
            Console.WriteLine("Goodbye!");
            return false;
        }

        static void Main(string[] args)
        {
            Console.Write("Please enter the ip address: ");
            string addressinp = Console.ReadLine();
            

            var client = new Lab4Service.Lab4ServiceClient("NetTcpBinding_ILab4Service", "net.tcp://" + addressinp +":282/LAB4Service.Service1");
            try
            {
                client.Test();
            }
            catch (System.ServiceModel.EndpointNotFoundException e)
            {
                Console.WriteLine("You have entered the wrong address!");
                Console.ReadKey();
                return;
            }
            bool myProgramIsRunning = true;
            int Call;
            while (myProgramIsRunning)
            {
                Console.WriteLine("1.Check if year is leap" +
                                  "\n2.Calculate interval between dates " +
                                  "\n3.Get the name of day of week" +
                                  "\n4.Serialize interval from last query" +
                                  "\n5.Quit");
                while (!int.TryParse(Console.ReadLine(), out Call));
                switch (Call)
                {
                    case 1:
                        myProgramIsRunning = Check(client);
                        break;
                    case 2:
                        myProgramIsRunning = Calc(client);
                        break;
                    case 3:
                        myProgramIsRunning = Day(client);
                        break;
                    case 4:
                        myProgramIsRunning = LastQuery(client);
                        break;
                    case 5:
                        myProgramIsRunning = Quit();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
