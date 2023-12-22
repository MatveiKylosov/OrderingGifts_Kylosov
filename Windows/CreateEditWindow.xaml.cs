using OrderingGifts_Kylosov.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
                Category.Text = gift.Category;
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
            if(!new Regex(@"^[А-ЯЁ][а-яё]*\s[А-ЯЁ][а-яё]*\s[А-ЯЁ][а-яё]*$").IsMatch(FIO.Text))
            {
                MessageBox.Show("ФИО введено не корректно!");
                return;
            }
            if (!new Regex(@"^(0[1-9]|[12][0-9]|3[01])\.(0[1-9]|1[012])\.\d{4}\s([01][0-9]|2[0-3]):[0-5][0-9]$").IsMatch(DateAndTime.Text))
            {
                MessageBox.Show("Укажите корректное время.");
                return;
            }
            if (!new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$").IsMatch(Mail.Text))
            {
                MessageBox.Show("Введите корректно почту.");
                return;
            }


            if (!MainWindow.main.category.Exists(x => x == Category.Text))
            {
                MainWindow.main.Connection.CUDGift($"INSERT INTO Category (Name) VALUES ('{Category.Text}')");
                MainWindow.main.category.Add(Category.Text);
            }

            bool done = gift != null ?
                MainWindow.main.Connection.CUDGift($"UPDATE Gift SET FIO = '{FIO.Text}', TextMessage = '{TextMessage.Text}', Address = '{Address.Text}', DateAndTime = '{DateAndTime.Text}', Mail = '{Mail.Text}', Category = '{Category.Text}' where Code = {gift.id}" ) :
                MainWindow.main.Connection.CUDGift($"INSERT INTO Gift (FIO, TextMessage, Address, DateAndTime, Mail, Category) VALUES ('{FIO.Text}', '{TextMessage.Text}' , '{Address.Text}', '{DateAndTime.Text}', '{Mail.Text}', '{Category.Text}')");



            MessageBox.Show($"Запрос {( done ? "" : "не")} был выполнен.");
            this.Close();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

}
