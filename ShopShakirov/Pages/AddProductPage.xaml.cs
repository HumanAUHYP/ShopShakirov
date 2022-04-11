using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для AddProductPage.xaml
    /// </summary>
    public partial class AddProductPage : Page
    {
        Product product = new Product();
        public AddProductPage()
        {
            InitializeComponent();
            cbUnit.ItemsSource = MainWindow.dbConnection.Unit.ToList();

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

        private void BtnAddClick(object sender, RoutedEventArgs e)
        {
            product.Name = tbxName.Text;
            product.Description = tbxDescription.Text;
            product.UnitId = (cbUnit.SelectedItem as Unit).Id;
            product.AddDate = DateTime.Today;
            product.IsDeleted = false;

            MainWindow.dbConnection.Product.Add(product);
            MainWindow.dbConnection.SaveChanges();


            MessageBox.Show("Продукт добавлен");
            NavigationService.Navigate(new TableProductsPage());
        }
    }
}
