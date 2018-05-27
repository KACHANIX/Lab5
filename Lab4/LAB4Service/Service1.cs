using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.XPath;

namespace LAB4Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Lab4Service : ILab4Service
    {
        public static void SerializeIn(DateTime date1, DateTime date2)
        {
            List<DateTime> allDates = new List<DateTime>();
            for (DateTime date = date1; date <= date2; date = date.AddDays(1))
            {
                allDates.Add(date);
            }

            string path = $@"C:\Users\{Environment.UserName}\Result.xml";
            var xml = new XmlDocument();
            XmlElement root = xml.CreateElement("AllYears");
            xml.AppendChild(root);

            XmlElement year = xml.CreateElement("Year");
            year.SetAttribute("Number", Convert.ToString(allDates[0].Year));
            root.AppendChild(year);

            XmlElement month = xml.CreateElement("Month");
            month.SetAttribute("Number", Convert.ToString(allDates[0].Month));
            year.AppendChild(month);

            XmlElement day = xml.CreateElement("Day");
            day.SetAttribute("Number", Convert.ToString(allDates[0].Day));
            month.AppendChild(day);

            for (int i = 1; i < (allDates.Count - 1); i++)
            {
                if (allDates[i].Year != Convert.ToInt32(year.GetAttribute("Number")))
                {
                    year = xml.CreateElement("Year");
                    year.SetAttribute("Number", Convert.ToString(allDates[i].Year));
                    root.AppendChild(year);
                }
                if (allDates[i].Month != Convert.ToInt32(month.GetAttribute("Number")))
                {
                    month = xml.CreateElement("Month");
                    month.SetAttribute("Number", Convert.ToString(allDates[i].Month));
                    year.AppendChild(month);
                }
                if (allDates[i].Day != Convert.ToInt32(day.GetAttribute("Number")))
                {
                    day = xml.CreateElement("Day");
                    day.SetAttribute("Number", Convert.ToString(allDates[i].Day));
                    month.AppendChild(day);
                }
            }
            xml.Save(path);

        }


        public string Calc(DateTime date1, DateTime date2)
        {
            if (date1 > date2)
            {
                DateTime tmp = date1;
                date1 = date2;
                date2 = tmp;
            }
            double difdates = (date2 - date1).TotalDays;


            SerializeIn(date1, date2);

            return ($"\n\n___________\nThe difference between dates is {difdates} day(s)\n___________");
        }

        public string Check(int year)
        {
            if (DateTime.IsLeapYear(year))
            {
                return ($"\n___________\n{year} is leap\n___________");
            }
            else
            {
                return ($"\n___________\n{year} isn't leap\n___________");
            }
        }

        public string Day(DateTime date)
        {
            return ($"\n\n___________\nIt's {date.DayOfWeek}\n___________");
        }

        public string SerializeFrom()
        {
            Console.Clear();
            XPathDocument doc = new XPathDocument($@"C:\Users\{Environment.UserName}\Result.xml");
            XPathNavigator navigator = doc.CreateNavigator();
            XPathNodeIterator nodes = navigator.Select("//Day");
            int count = 0;
            while (nodes.MoveNext())
            {
                count++;
            }
            return ($"\n\n___________\nThe difference between days from the last query is {count} day(s)\n___________");

        }
        
        public void Test()
        {
        }
    }
}
