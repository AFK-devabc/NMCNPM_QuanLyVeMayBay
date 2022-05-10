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
using System.Windows.Shapes;

namespace NNCNPM_QuanLyVeMayBay
{
    /// <summary>
    /// Interaction logic for WDatVeMayBay.xaml
    /// </summary>
    public partial class WDatVeMayBay : Window
    {
        public WDatVeMayBay()
        {
            InitializeComponent();

            this.DataContext = this;

            SetDefaultControls();
        }


        void SetDefaultControls()
        {
            for (int i = 0; i < 10; i++)
            {
                UserControls.AvailableFlights available = new UserControls.AvailableFlights();
                
                SPN_ShowList.Children.Add(available);
            }

        }

        private void btn_Book_Click(object sender, RoutedEventArgs e)
        {
            MaterialDesignThemes.Wpf.Snackbar snackbar = new MaterialDesignThemes.Wpf.Snackbar();
            
            MaterialDesignThemes.Wpf.SnackbarMessageQueue s = new MaterialDesignThemes.Wpf.SnackbarMessageQueue();
            s.Enqueue("Chỗ này đã có người đặt!");

            snackbar.MessageQueue = s;
            
            gr_book.Children.Add(snackbar);
        }

     
    }
}
