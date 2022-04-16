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
        List<Product> AllProducts;
        public TableProductsPage()
        {
            InitializeComponent();
            if (Properties.Settings.Default.RoleId < 3)
            {
                btnAdd.Visibility = Visibility.Visible;
                btnChange.Visibility = Visibility.Visible;
                btnDelete.Visibility = Visibility.Visible;
            }
            var Units = MainWindow.dbConnection.Unit.ToList();
            Units.Add(new Unit{ Name = "Все" });
            cbUnitSort.ItemsSource = Units;
            AllProducts = Products;
            
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
            Products = Products.Where(a => a.IsDeleted == false).ToList();
            AllProducts = AllProducts.Where(a => a.IsDeleted == false).ToList();
            DisplayProductsInPage();
        }

        private void BtnLessPageClick(object sender, RoutedEventArgs e)
        {
            if (pageIndex > 1)
            {
                pageIndex--;
                DisplayProductsInPage();
            }
        }

        private void BtnNextPageClick(object sender, RoutedEventArgs e)
        {
            if (countInPage * pageIndex < Products.Count())
            {
                pageIndex++;
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
            DisplayProductsInPage();
        }

        private void DisplayProductsInPage()
        {
            tbPageIndex.Text = Convert.ToString(pageIndex);
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

        private void CbUnitSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AllFilters();
        }

        private void TbxSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            AllFilters();
        }

        private void CbOrderSortSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AllFilters();
        }

        private void AllFilters()
        {
            Products = AllProducts;
            var selectedUnit = cbUnitSort.SelectedItem as Unit;
            if (selectedUnit != null && selectedUnit.Name != "Все")
            {
                Products = Products.FindAll(a => a.UnitId == selectedUnit.Id);
                pageIndex = 1;
            }

            if (tbxSearch.Text != "")
                Products = Products.Where(a => a.Name.Contains($"{tbxSearch.Text}") || a.Description.Contains($"{tbxSearch.Text}")).ToList();

            var selectedSort = cbOrderSort.SelectedItem as TextBlock;
            if (selectedSort != null)
            {
                if (selectedSort == tbOrderName)
                    Products = Products.OrderBy(a => a.Name).ToList();
                else if (selectedSort == tbOrderDescName)
                    Products = Products.OrderByDescending(a => a.Name).ToList();
                else if (selectedSort == tbOrderDate)
                    Products = Products.OrderBy(a => a.AddDate).ToList();
                else if (selectedSort == tbOrderDescDate)
                    Products = Products.OrderByDescending(a => a.AddDate).ToList();
            }
            DisplayProductsInPage();
        }

        private void BtnResetClick(object sender, RoutedEventArgs e)
        {
            cbOrderSort.SelectedIndex = -1;
            cbUnitSort.SelectedIndex = -1;
            tbxSearch.Text = "";
            Products = AllProducts;
            DisplayProductsInPage();
        }
    }
}
