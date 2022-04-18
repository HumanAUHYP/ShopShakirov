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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ShopShakirov.Pages
{
    /// <summary>
    /// Логика взаимодействия для RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        int clientRoleId = 3;
        public RegisterPage()
        {
            InitializeComponent();
        }

        private void BtnRegisterClick(object sender, RoutedEventArgs e)
        {
            var login = tbxLogin.Text;
            var password = pbxPassword.Password;
            var users = MainWindow.dbConnection.User;

            if (users.Where(a => a.Login == login).Count() != 0)
            {
                MessageBox.Show("Данный логин уже занят", "Ошибка");
            }
            else if (!new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^])(?=.*[^a-zA-Z0-9])\S{6,16}$").IsMatch(password))
            {
                MessageBox.Show("Неверный формат пароля", "Ошибка");
            }
            else
            {
                User newUser = new User();
                newUser.Login = login;
                newUser.Password = password;
                newUser.RoleId = clientRoleId;
                users.Add(newUser);
                MainWindow.dbConnection.SaveChanges();
                MessageBox.Show("Вы успешно зарегестрировались!");
                NavigationService.Navigate(new LoginPage());
            }
            
        }
    }
}
