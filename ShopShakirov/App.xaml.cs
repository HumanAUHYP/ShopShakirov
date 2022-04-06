using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ShopShakirov
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void TextSizeChanger(object sender, SizeChangedEventArgs e)
        {
            //Size n = e.NewSize;
            //Size p = e.PreviousSize;
            //double l = n.Width / p.Width;
            //if (l != double.PositiveInfinity)
            //{
            //    if (sender is TextBox)
            //        (sender as TextBox).FontSize *= l;
            //    if (sender is PasswordBox)
            //        (sender as PasswordBox).FontSize *= l;
            //    else if (sender is TextBlock)
            //        (sender as TextBlock).FontSize *= l;
            //    else if (sender is Button)
            //        (sender as Button).FontSize *= l;
            //    else if (sender is CheckBox)
            //    {
            //        ScaleTransform scale = new ScaleTransform(2, 2);
            //        if (l < 1)
            //        {
            //            scale.ScaleX *= l / 4;
            //            scale.ScaleY *= l / 4;
            //        }
            //        else
            //        {
            //            scale.ScaleX *= l;
            //            scale.ScaleY *= l;
            //        }
            //        (sender as CheckBox).RenderTransform = scale;
            //    }
            //}
        }
    }
}
