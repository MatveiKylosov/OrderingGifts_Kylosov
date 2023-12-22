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

namespace OrderingGifts_Kylosov.Windows
{
    /// <summary>
    /// Логика взаимодействия для FilterWindow.xaml
    /// </summary>
    public partial class FilterWindow : Window
    {
        public string query = "";
        public FilterWindow(List<string> category)
        {
            InitializeComponent();
            this.MouseDown += Window_MouseDown;
            foreach (string x in category)
                Category.Items.Add(x);

            Category.SelectedIndex = 0;
        }

        private void MainButton_Click(object sender, RoutedEventArgs e)
        {
            query = "SELECT * FROM Gift WHERE ";
            List<string> fioParts = new List<string>();

            if (!string.IsNullOrEmpty(F.Text))
                fioParts.Add(F.Text);
            if (!string.IsNullOrEmpty(I.Text))
                fioParts.Add(I.Text);
            if (!string.IsNullOrEmpty(O.Text))
                fioParts.Add(O.Text);

            if (fioParts.Count > 0)
            {
                string fio = string.Join(" ", fioParts);
                query += $"FIO LIKE '%{fio}%'";
            }

            if(TextMessage.Text != "")
                query += $", TextMessage LIKE '%{TextMessage.Text}%'";

            if(Address.Text != "")
                 query += $", Address LIKE '%{Address.Text}%'";

            if(DateAndTime.Text != "")
                query += $", DateAndTime LIKE '%{DateAndTime.Text}%'";

            if (Mail.Text != "")
                query += $", Mail LIKE '%{Mail.Text}%';";

            if (query == "SELECT * FROM Gift WHERE ") 
                query = null;

            this.Close();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            F.Text = I.Text = O.Text = TextMessage.Text = Address.Text = DateAndTime.Text = Mail.Text = "";
        }
    }
}
