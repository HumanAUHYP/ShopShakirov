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
            Countries.ItemsSource = MainWindow.dbConnection.ProductCountry.Where(a => a.ProductId == product.Id).ToList();
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
    }
}
