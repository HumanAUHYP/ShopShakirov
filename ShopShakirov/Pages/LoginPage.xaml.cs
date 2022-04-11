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
        int idUser;
        public LoginPage()
        {
            InitializeComponent();
        }

        private void BtnLoginClick(object sender, RoutedEventArgs e)
        {
            if (CorrectLoginPassword())
                NavigationService.Navigate(new TableProductsPage());
            else
                MessageBox.Show("Неверный логин или пароль");
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
    }
}
