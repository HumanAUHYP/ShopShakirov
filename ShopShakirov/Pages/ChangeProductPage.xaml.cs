using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.Reflection;

namespace ShopShakirov.Pages
{
    /// <summary>
    /// Логика взаимодействия для ChangeProductPage.xaml
    /// </summary>
    public partial class ChangeProductPage : Page
    {
        Product product;
        public ChangeProductPage(Product postProduct)
        {
            InitializeComponent();

            product = postProduct;
            UpdateCountryList();
            cbUnit.ItemsSource = MainWindow.dbConnection.Unit.ToList();
            cbCountries.ItemsSource = MainWindow.dbConnection.Country.ToList();

            this.DataContext = product;
        }

        private void BtnSelectPhotoClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog() { Filter = "*.jpg|*.jpg|*.png|*.png|*.jpeg|*.jpeg" };
            if (openFile.ShowDialog().GetValueOrDefault())
            {
                product.Photo = File.ReadAllBytes(openFile.FileName);
                photoImage.Source = new BitmapImage(new Uri(openFile.FileName));
            }
        }

        private void BtnSaveClick(object sender, RoutedEventArgs e)
        {
            product.Name = tbxName.Text;
            product.Description = tbxDescription.Text;
            product.UnitId = (cbUnit.SelectedItem as Unit).Id;

            MainWindow.dbConnection.SaveChanges();

            MessageBox.Show("Изменения сохранены");
            NavigationService.Navigate(new TableProductsPage());
        }

        private void BtnAddCountryClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var productCountry = new ProductCountry();
                var selectedCountry = cbCountries.SelectedItem as Country;
                if (selectedCountry != null)
                    productCountry.CountryId = selectedCountry.Id;
                productCountry.ProductId = product.Id;

                if (MainWindow.dbConnection.ProductCountry.Where(a => a.CountryId == selectedCountry.Id && a.ProductId == product.Id).Count() == 0)
                {
                    MainWindow.dbConnection.ProductCountry.Add(productCountry);
                    MainWindow.dbConnection.SaveChanges();
                    UpdateCountryList();
                }
            }
            catch (TargetException)
            {
                MessageBox.Show("Не выбрана страна для добавления");
            }
        }

        private void BtnDeleteCountryClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedCountry = Countries.SelectedItem as ProductCountry;
                if (selectedCountry != null)
                {
                    var productCountry = MainWindow.dbConnection.ProductCountry.ToList().Find(a => a.ProductId == product.Id && a.CountryId == selectedCountry.CountryId);
                    MainWindow.dbConnection.ProductCountry.Remove(productCountry);
                    MainWindow.dbConnection.SaveChanges();
                    UpdateCountryList();
                }
                else MessageBox.Show("Не выбрана страна для удаления");

            }
            catch (Exception ex)
            {

                MessageBox.Show($"{ex}");
            }
        }

        private void UpdateCountryList()
        {
            Countries.ItemsSource = MainWindow.dbConnection.ProductCountry.Where(e => e.ProductId == product.Id).ToList();
        }
    }
}
