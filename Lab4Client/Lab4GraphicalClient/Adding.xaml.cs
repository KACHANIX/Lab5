using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for Adding.xaml
    /// </summary>
    public partial class Adding : Window
    {
        public DateTime date1 = new DateTime();
        public bool IsSet = false;
        public Adding()
        {
            InitializeComponent();
            DateInput.KeyDown += new KeyEventHandler(TextBox_Press);
            DateInput.MaxLength = 10;
        }
        

        private void TextBox_Press(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AddButton_Click(sender, e);
            }
        }

        public void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(DateTime.TryParse(DateInput.Text, out  date1)))
            { 
                MessageBox.Show($"  Unable to parse '{DateInput.Text}'.");
                return;
            }
            IsSet = true;
            this.Close();
        }

        
    }
}
