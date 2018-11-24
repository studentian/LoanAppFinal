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
using System.Windows.Shapes;

namespace LoanApplication
{
    /// <summary>
    /// Interaction logic for DashboardProvider.xaml
    /// </summary>
    public partial class DashboardProvider : Window
    {
        public DashboardProvider()
        {
            InitializeComponent();
        }


        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Environment.Exit(0);
        }

        private void BtnDashProvider(object sender, RoutedEventArgs e)
        {
            DashboardProvider btnDashboardProvider = new DashboardProvider();
        }

        private void btnlogout_Click(object sender, RoutedEventArgs e)
        {
            DashboardProvider dashboardProvider = new DashboardProvider();
            dashboardProvider.Close();
        }
    }
}