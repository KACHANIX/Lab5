using System;
using System.ServiceModel;

namespace Lab4Host
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var host = new ServiceHost(typeof(LAB4Service.Lab4Service)))
            {
                host.Open();
                Console.WriteLine("Host opened");
                Console.ReadLine();
            }
        }
    }
}
