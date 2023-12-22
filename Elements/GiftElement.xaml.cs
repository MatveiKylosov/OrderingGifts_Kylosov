using OrderingGifts_Kylosov.Classes;
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

namespace OrderingGifts_Kylosov.Elements
{
    /// <summary>
    /// Логика взаимодействия для GiftElement.xaml
    /// </summary>
    public partial class GiftElement : UserControl
    {
        GiftInfo gift;

        public GiftElement(GiftInfo gift)
        {
            this.gift = gift;
            InitializeComponent();

            FIO.Text = gift.FIO;
            TextMessage.Text = gift.TextMsg;
            Address.Text = gift.Address;
            DateAndTime.Text = gift.DateAndTime;
            Mail.Text = gift.Mail;
            Category.Text = gift.Category;
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            //Открытие нового окна, получение введённых данных и после редактирование.
            Windows.CreateEditWindow x = new Windows.CreateEditWindow(gift);
            x.ShowDialog();
            MainWindow.main.OutputGifts();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.main.Connection.CUDGift($"DELETE FROM Gift where Code = {gift.id}");
            MainWindow.main.OutputGifts();
        }
    }
}
