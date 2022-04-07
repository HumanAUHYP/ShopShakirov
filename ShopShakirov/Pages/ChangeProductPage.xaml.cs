﻿using System;
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
    /// Логика взаимодействия для ChangeProductPage.xaml
    /// </summary>
    public partial class ChangeProductPage : Page
    {
        public ChangeProductPage(Product postProduct)
        {
            InitializeComponent();

            Countries.ItemsSource = MainWindow.dbConnection.ProductCountry.Where(a => a.ProductId == postProduct.Id).ToList();
        }
    }
}
