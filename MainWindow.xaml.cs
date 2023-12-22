using OrderingGifts_Kylosov.Elements;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OrderingGifts_Kylosov
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow main;
        public Classes.ConnectionDataBase Connection = new Classes.ConnectionDataBase(@"C:\Users\matve\Documents\PR23.accdb");
        public List<string> category;

        public MainWindow()
        {
            main = this;
            InitializeComponent();
            category = Connection.LoadCategory();
            if (Connection.err != "") MessageBox.Show(Connection.err);
            OutputGifts();
        }

        public void OutputGifts(string query = null)
        {
            parrent.Children.Clear();

            foreach (Classes.GiftInfo x in query == null | query == "" ? Connection.LoadGift() : Connection.LoadGift(query))
                parrent.Children.Add(new Elements.GiftElement(x));
        }

        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            Windows.FilterWindow filterWindow = new Windows.FilterWindow(category);
            filterWindow.ShowDialog();

            OutputGifts(filterWindow.query);
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            Windows.CreateEditWindow x = new Windows.CreateEditWindow();
            x.ShowDialog();
            OutputGifts();
        }

        private void Reset_Click(object sender, RoutedEventArgs e) => OutputGifts();
    }
}
