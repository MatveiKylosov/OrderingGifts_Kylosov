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
using System.Windows.Shapes;

namespace OrderingGifts_Kylosov.Windows
{
    /// <summary>
    /// Логика взаимодействия для CreateEditWindow.xaml
    /// </summary>
    public partial class CreateEditWindow : Window
    {
        GiftInfo gift;
        public CreateEditWindow(GiftInfo gift = null)
        {
            InitializeComponent();
            this.MouseDown += Window_MouseDown;
            this.gift = gift;

            if (this.gift != null)
            {
                MainButton.Content = "Изменить";

                FIO.Text = gift.FIO;
                TextMessage.Text = gift.TextMsg;
                Address.Text = gift.Address;
                DateAndTime.Text = gift.DateAndTime;
                Mail.Text = gift.Mail;
            }
            else
                MainButton.Content = "Добавить";

        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void MainButton_Click(object sender, RoutedEventArgs e)
        {
            bool done = gift != null ?
                MainWindow.main.Connection.CUDGift($"UPDATE Gift SET FIO = '{FIO.Text}', TextMessage = '{TextMessage.Text}', Address = '{Address.Text}', DateAndTime = '{DateAndTime.Text}', Mail = '{Mail.Text}' where Code = {gift.id}") :
                MainWindow.main.Connection.CUDGift($"INSERT INTO Gift (FIO, TextMessage, Address, DateAndTime, Mail) VALUES ('{FIO.Text}', '{TextMessage.Text}' , '{Address.Text}', '{DateAndTime.Text}', '{Mail.Text}')");

            MessageBox.Show($"Запрос {( done ? "" : "не")} был выполнен.");
            this.Close();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

}
