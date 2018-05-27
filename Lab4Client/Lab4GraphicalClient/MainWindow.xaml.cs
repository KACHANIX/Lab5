using System;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Lab4GraphicalClient
{
    public partial class MainWindow : Window
    {
        Lab4Service.Lab4ServiceClient client = new Lab4Service.Lab4ServiceClient();
        int counter = 0;
        public MainWindow()
        {
            InitializeComponent();
            Add.Click += Add_Click;
            Calc.Click += Calc_Click;
            Leap.Click += Leap_Click;
            Day.Click += Day_Click;
        }

        TaskCompletionSource<bool> tcs = null;
        private async void Day_Click(object sender, EventArgs e)
        {
            tcs = new TaskCompletionSource<bool>();
            MessageBox.Show("Select a day in a treeview to learn the name of the day of the week");
            await tcs.Task;
            TreeViewItem date1 = (TreeViewItem)TV.SelectedItem;

            StringBuilder str1 = new StringBuilder();
            TreeViewItem Parent1 = (TreeViewItem)date1.Parent;
            TreeViewItem GrandParent1 = (TreeViewItem)Parent1.Parent;
            str1.Append(date1.Header);
            str1.Append(".");
            str1.Append(Parent1.Header);
            str1.Append(".");
            str1.Append(GrandParent1.Header);
            DateTime Date1 = Convert.ToDateTime(Convert.ToString(str1));
            try
            {
                MessageBox.Show(client.Day(Date1));
            }
            catch (System.ServiceModel.CommunicationException)
            {
                MessageBox.Show("There is no host anymore!");
            }
        }

        private async void Leap_Click(object sender, EventArgs e)
        {
            tcs = new TaskCompletionSource<bool>();
            MessageBox.Show("Click on a day to get the information about the year");
            await tcs.Task;
            TreeViewItem date1 = (TreeViewItem)TV.SelectedItem;
            TreeViewItem Parent1 = (TreeViewItem)date1.Parent;
            TreeViewItem GrandParent1 = (TreeViewItem)Parent1.Parent;
            int Year = Convert.ToInt32(GrandParent1.Header);
            try
            { 
                MessageBox.Show(client.Check(Year));
            }
            catch (System.ServiceModel.CommunicationException)
            {
                MessageBox.Show("There is no host anymore!");
            }
}

        private async void Calc_Click(object sender, EventArgs e)
        {
            tcs = new TaskCompletionSource<bool>();
            MessageBox.Show("Select first day in a treeview");
            await tcs.Task;
            TreeViewItem date1 = (TreeViewItem)TV.SelectedItem;

            tcs = new TaskCompletionSource<bool>();
            MessageBox.Show("Select second day in a treeview");
            await tcs.Task;
            TreeViewItem date2 = (TreeViewItem)TV.SelectedItem;

            StringBuilder str1 = new StringBuilder();
            TreeViewItem Parent1 = (TreeViewItem)date1.Parent;
            TreeViewItem GrandParent1 = (TreeViewItem)Parent1.Parent;
            str1.Append(date1.Header);
            str1.Append(".");
            str1.Append(Parent1.Header);
            str1.Append(".");
            str1.Append(GrandParent1.Header);

            StringBuilder str2 = new StringBuilder();
            TreeViewItem Parent2 = (TreeViewItem)date2.Parent;
            TreeViewItem GrandParent2 = (TreeViewItem)Parent2.Parent;
            str2.Append(date2.Header);
            str2.Append(".");
            str2.Append(Parent2.Header);
            str2.Append(".");
            str2.Append(GrandParent2.Header);
            
            DateTime Date1 = Convert.ToDateTime(Convert.ToString(str1));
            DateTime Date2 = Convert.ToDateTime(Convert.ToString(str2));
            try
            { 
                MessageBox.Show(client.Calc(Date1, Date2));
            }
            catch (System.ServiceModel.CommunicationException)
            {
                MessageBox.Show("There is no host anymore!");
            }

}
        private void DontLetViewItem_MouseLeftButtonUp(object sender, RoutedEventArgs e)
        {
            e.Handled = false;
            return;
        }

        private void LetViewItem_MouseLeftButtonUp(object sender, RoutedEventArgs e)
        {
            TreeViewItem tvi = (TreeViewItem)sender;
            try
            {
                tcs.TrySetResult(true);
            }
            catch (Exception) { }
            e.Handled = true;
        }


        private void Add_Click(object sender, EventArgs e)
        {
            Adding adding = new Adding();
            adding.ShowDialog();
            //MessageBox.Show($"{adding.date1}");
            if (adding.IsSet == false)
            {
                return;
            }
            int CountYears = 0;
            int CountMonths = 0;
            int CountDays = 0;

            string year = Convert.ToString(adding.date1.Year);
            string month = Convert.ToString(adding.date1.Month);
            string day = Convert.ToString(adding.date1.Day);
            TreeViewItem YearTV = new TreeViewItem() { Header = year };
            TreeViewItem MonthTV = new TreeViewItem() { Header = month };
            TreeViewItem DayTV = new TreeViewItem() { Header = day };
            YearTV.Selected += DontLetViewItem_MouseLeftButtonUp;
            MonthTV.Selected += DontLetViewItem_MouseLeftButtonUp;
            DayTV.Selected += LetViewItem_MouseLeftButtonUp; 


            if (counter == 0)
            {
                MonthTV.Items.Add(DayTV);
                YearTV.Items.Add(MonthTV);
                TV.Items.Add(YearTV);
                counter++;
            }
            else
            {
                foreach (TreeViewItem item in TV.Items)
                {
                    if (year == Convert.ToString(item.Header))
                    {
                        CountYears++;
                    }
                }
                if (CountYears == 0)
                {
                    MonthTV.Items.Add(DayTV);
                    YearTV.Items.Add(MonthTV);
                    TV.Items.Add(YearTV);
                }
                else
                {
                    foreach (TreeViewItem item in TV.Items)
                    {
                        if (year == Convert.ToString(item.Header))
                        {
                            foreach(TreeViewItem child1 in item.Items)
                            {
                                if (month == Convert.ToString(child1.Header))
                                {
                                    CountMonths++;
                                }
                            }
                        }
                    }
                    if (CountMonths == 0)
                    {
                        foreach (TreeViewItem item in TV.Items)
                        {
                            if (year == Convert.ToString(item.Header))
                            {
                                MonthTV.Items.Add(DayTV);
                                item.Items.Add(MonthTV);
                            }
                        }
                    }
                    else
                    {
                        foreach (TreeViewItem item in TV.Items)
                        {
                            if (year == Convert.ToString(item.Header))
                            {
                                foreach (TreeViewItem child1 in item.Items)
                                {
                                    if (month == Convert.ToString(child1.Header))
                                    {
                                        foreach (TreeViewItem child2 in child1.Items)
                                        {
                                            if (day == Convert.ToString(child2.Header))
                                            {
                                                CountDays++;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (CountDays == 0)
                        {
                            foreach (TreeViewItem item in TV.Items)
                            {
                                if (year == Convert.ToString(item.Header))
                                {
                                    foreach (TreeViewItem child1 in item.Items)
                                    {
                                        if (month == Convert.ToString(child1.Header))
                                        {
                                            child1.Items.Add(DayTV);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                ItemCollection icchild = TV.Items;
                TV.Items.SortDescriptions.Add(new SortDescription("Header", ListSortDirection.Descending));
                foreach (TreeViewItem item in TV.Items)
                {
                    item.Items.SortDescriptions.Add(new SortDescription("Header", ListSortDirection.Descending));
                    foreach (TreeViewItem item1 in item.Items)
                    {
                        item1.Items.SortDescriptions.Add(new SortDescription("Header", ListSortDirection.Descending));
                    }
                }
            }
        }
        
    }
}
