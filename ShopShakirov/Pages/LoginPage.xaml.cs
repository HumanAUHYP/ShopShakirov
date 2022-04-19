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

namespace ShopShakirov.Pages
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        int TryLoginCounts = 0;
        TimeSpan TimeOfBan = new TimeSpan(0, 0, 1, 0);
        public LoginPage()
        {
            InitializeComponent();
            tbxLogin.Text = Properties.Settings.Default.Login;
        }

        private void BtnLoginClick(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.IsBaned)
            {
                var UnbanTime = Properties.Settings.Default.BanTime + TimeOfBan;
                var TimeOfUnban = UnbanTime - DateTime.Now;

                if (DateTime.Now >= UnbanTime)
                {
                    Properties.Settings.Default.IsBaned = false;
                    Properties.Settings.Default.Save();
                    TimeOfUnban = TimeSpan.Zero;
                    TryLoginCounts = 0;
                    Login();
                }
                else MessageBox.Show($"До следующей попытки ввода {Math.Round(TimeOfUnban.TotalSeconds)} секунд");
            }
            else 
            {
                Login();
            }
        }

        private void BtnRegisterClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegisterPage());
        }

        public bool CorrectLoginPassword()
        {
            var user = MainWindow.dbConnection.User.Where(a => a.Login == tbxLogin.Text && a.Password == pbxPassword.Password);
            if (user.Count() != 0)
            {
                Properties.Settings.Default.RoleId = user.ToList()[0].RoleId;
                return true;
            }
            else return false;
        }

        private void Login()
        {
            if (CorrectLoginPassword())
            {
                NavigationService.Navigate(new TableProductsPage());
                if (cbRemember.IsChecked.GetValueOrDefault())
                    Properties.Settings.Default.Login = tbxLogin.Text;
                else
                    Properties.Settings.Default.Login = null;
                Properties.Settings.Default.Save();
                TryLoginCounts = 0;
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль");
                TryLoginCounts++;
                if (TryLoginCounts == 3)
                {
                    Properties.Settings.Default.BanTime = DateTime.Now;
                    Properties.Settings.Default.IsBaned = true;
                    Properties.Settings.Default.Save();
                    MessageBox.Show($"Логин или пароль были введены неверно 3 раза, до следующей попытки ввода {Math.Round(TimeOfBan.TotalSeconds)} секунд");
                }
            }
        }
    }
}
