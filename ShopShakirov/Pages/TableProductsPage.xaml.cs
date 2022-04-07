using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
    /// Логика взаимодействия для TableProductsPage.xaml
    /// </summary>
    public partial class TableProductsPage : Page
    {
        public TableProductsPage()
        {
            InitializeComponent();
            ProductTable.ItemsSource = MainWindow.dbConnection.Product.ToList();
        }

        private void BtnAddClick(object sender, RoutedEventArgs e)
        {

        }

        private void BtnChangeClick(object sender, RoutedEventArgs e)
        {
            var postProduct = ProductTable.SelectedItem as Product;
            try
            {
                NavigationService.Navigate(new ChangeProductPage(postProduct));
            }
            catch (TargetException)
            {
                MessageBox.Show("Не выбран продукт для изменения");
            }
        }

        private void BtnDeleteClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
