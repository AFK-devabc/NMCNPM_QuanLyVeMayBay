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

namespace NNCNPM_QuanLyVeMayBay.UserControls
{
    /// <summary>
    /// Interaction logic for ButtonChair.xaml
    /// </summary>
    public partial class ButtonChair : UserControl
    {
        public ButtonChair()
        {
            InitializeComponent();
        }

        bool isGreen = true;
        private void myself_Click(object sender, RoutedEventArgs e)
        {
            if (isGreen)
            {
                (sender as Button).Background = Brushes.Yellow;
                isGreen = false;
            }
            else
            {
                (sender as Button).Background = Brushes.Lime;
                isGreen = true;
            }
            
        }
    }
}
