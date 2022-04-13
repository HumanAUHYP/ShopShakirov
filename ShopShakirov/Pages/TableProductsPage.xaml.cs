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
        int countInPage = 10;
        int pageIndex = 1;
        List<Product> Products = MainWindow.dbConnection.Product.Where(a => a.IsDeleted == false).ToList();
        public TableProductsPage()
        {
            InitializeComponent();
            if (Properties.Settings.Default.RoleId < 3)
            {
                btnAdd.Visibility = Visibility.Visible;
                btnChange.Visibility = Visibility.Visible;
                btnDelete.Visibility = Visibility.Visible;
            }
            
            DisplayProductsInPage();
            cbCountInPage.SelectedIndex = 0;
        }



        private void BtnAddClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddProductPage());
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
            var product = ProductTable.SelectedItem as Product;
            product.IsDeleted = true;
            MainWindow.dbConnection.SaveChanges();
            ProductTable.ItemsSource = MainWindow.dbConnection.Product.Where(a => a.IsDeleted == false).ToList();
        }

        private void BtnLessPageClick(object sender, RoutedEventArgs e)
        {
            if (pageIndex > 1)
            {
                tbPageIndex.Text = Convert.ToString(--pageIndex);
                DisplayProductsInPage();
            }
        }

        private void BtnNextPageClick(object sender, RoutedEventArgs e)
        {
            if (countInPage * pageIndex < Products.Count())
            {
                tbPageIndex.Text = Convert.ToString(++pageIndex);
                DisplayProductsInPage();
            }
        }

        private void CbCountInPageSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                countInPage = int.Parse((cbCountInPage.SelectedItem as TextBlock).Text);
            }
            catch (Exception)
            {
                countInPage = Products.Count();
            }
            pageIndex = 1;
            tbPageIndex.Text = Convert.ToString(pageIndex);
            DisplayProductsInPage();
        }

        private void DisplayProductsInPage()
        {
            List<Product> ProductsInPage = new List<Product>();
            for (int i = (pageIndex - 1) * countInPage; i < countInPage * pageIndex; i++)
            {
                try
                {
                    ProductsInPage.Add(Products[i]);
                }
                catch (Exception)
                {
                    break;
                }
            }

            tbProductCountInPage.Text = $"{ProductsInPage.Count() + (pageIndex-1) * countInPage} из {Products.Count()}";
            ProductTable.ItemsSource = ProductsInPage;
        }
    }
}
