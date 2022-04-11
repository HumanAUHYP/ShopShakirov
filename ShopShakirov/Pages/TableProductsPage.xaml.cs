﻿using System;
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
            if (Properties.Settings.Default.RoleId < 3)
            {
                btnAdd.Visibility = Visibility.Visible;
                btnChange.Visibility = Visibility.Visible;
                btnDelete.Visibility = Visibility.Visible;
            }
            ProductTable.ItemsSource = MainWindow.dbConnection.Product.Where(a => a.IsDeleted == false).ToList();
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
    }
}
