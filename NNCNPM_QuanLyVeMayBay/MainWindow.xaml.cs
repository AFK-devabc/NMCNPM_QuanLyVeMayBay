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

namespace NNCNPM_QuanLyVeMayBay
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool islogout = false;
        string UserName = "";
        public MainWindow(string name = "")
        {
            InitializeComponent();
            WTrangChu child = new WTrangChu(name);
            object content = child.Content;
            child.Content = null;
            this.GridTabWindow.Children.Clear();
            this.GridTabWindow.Children.Add(content as UIElement);
            UserName = name;
            if (UserName.ToLower() != "admin")
            {
                BTN_CaiDat.Visibility = Visibility.Hidden;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.GridTabWindow.Children.Clear();

        }

        private void bnt_ChuyenBay_Click(object sender, RoutedEventArgs e)
        {
            WThongTinChuyenBay child = new WThongTinChuyenBay();
            object content = child.Content;
            child.Content = null;
            this.GridTabWindow.Children.Clear();
            this.GridTabWindow.Children.Add(content as UIElement);
        }

        private void bnt_VeMayBay_Click(object sender, RoutedEventArgs e)
        {
            WVeMayBay child = new WVeMayBay();
            object content = child.Content;
            child.Content = null;
            this.GridTabWindow.Children.Clear();
            this.GridTabWindow.Children.Add(content as UIElement);
        }

        private void bnt_DatVeMayBay_Click(object sender, RoutedEventArgs e)
        {
            WDatVeMayBay child = new WDatVeMayBay();
            object content = child.Content;
            child.Content = null;
            this.GridTabWindow.Children.Clear();
            this.GridTabWindow.Children.Add(content as UIElement);
        }

        private void bnt_TimKiemChuyenBay_Click(object sender, RoutedEventArgs e)
        {
            WTimKiemChuyenBay child = new WTimKiemChuyenBay();
            object content = child.Content;
            child.Content = null;
            this.GridTabWindow.Children.Clear();
            this.GridTabWindow.Children.Add(content as UIElement);
        }

        private void bnt_BaoCaoDoanhThu_Click(object sender, RoutedEventArgs e)
        {
            WBaoCaoDoanhThu child = new WBaoCaoDoanhThu();
            object content = child.Content;
            child.Content = null;
            this.GridTabWindow.Children.Clear();
            this.GridTabWindow.Children.Add(content as UIElement);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Owner.Show();
            islogout = true;
            this.Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (islogout == false)
            {
                Environment.Exit(0);
            }
        }


        private void BTN_CaiDat_Click(object sender, RoutedEventArgs e)
        {
            WCaiDat child = new WCaiDat();
            object content = child.Content;
            child.Content = null;
            this.GridTabWindow.Children.Clear();
            this.GridTabWindow.Children.Add(content as UIElement);
        }

        private void BTN_TrangChu_Click(object sender, RoutedEventArgs e)
        {
            WTrangChu child = new WTrangChu(UserName);
            object content = child.Content;
            child.Content = null;
            this.GridTabWindow.Children.Clear();
            this.GridTabWindow.Children.Add(content as UIElement);
        }
    }
}
