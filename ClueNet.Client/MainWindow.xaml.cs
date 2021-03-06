﻿using ClueNet.Client.Models;
using ClueNet.Client.Views.Pages;
using ClueNet.Core;
using System;
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

namespace ClueNet.Client
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            txtName.Text = "";
            //Global.Devices.Connect();

            object obj = new Uri("Views/Pages/SignalPage.xaml", UriKind.Relative);
            //pnlBody.Navigate(new SignalPage());
            pnlBody.Navigate(new Uri("Views/Pages/SignalPage.xaml", UriKind.Relative), obj);

            //NavigationService.GetNavigationService(this)
            //    .Navigate(new Uri("Views/Pages/SignalPage.xaml", UriKind.Relative), obj);

            //NavigationService.GetNavigationService(this).Navigate(new Uri("Page2.xaml", UriKind.Relative));
            //NavigationService.GetNavigationService(this).GoForward(); //向後轉
            //NavigationService.GetNavigationService(this).GoBack(); //向前轉
        }
    }
}
