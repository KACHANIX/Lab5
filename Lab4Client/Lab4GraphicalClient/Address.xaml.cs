using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Lab4GraphicalClient
{
    /// <summary>
    /// Interaction logic for Address.xaml
    /// </summary>
    public partial class Address : Window
    {
        public Address()
        {
            InitializeComponent();
            Addr.KeyDown += new KeyEventHandler(TextBox_PressEnter);

        }

        private void TextBox_PressEnter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Button_Click(sender, e);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string str = Addr.Text;


            var client = new Lab4Service.Lab4ServiceClient("NetTcpBinding_ILab4Service", "net.tcp://" + str + "/LAB4Service.Service1");
            try
            {
                client.Test();
            }
            catch (System.ServiceModel.EndpointNotFoundException a)
            {
                MessageBox.Show("You have entered the wrong address!");
                return;
            }
            MainWindow main = new MainWindow();
            this.Hide();
            main.Show();
            this.Close();

        }
    }
}
