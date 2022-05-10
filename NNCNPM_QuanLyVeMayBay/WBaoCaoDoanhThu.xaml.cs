using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NNCNPM_QuanLyVeMayBay
{
    /// <summary>
    /// Interaction logic for WBaoCaoDoanhThu.xaml
    /// </summary>
    public partial class WBaoCaoDoanhThu : Window, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string newName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(newName));
            }
        }

        public WBaoCaoDoanhThu()
        {
            InitializeComponent();

            this.DataContext = this;

            CotSoVe_ChuyenBay();

            CotDoanhThu_ChuyenBay();

            FullName = "Hong Truong Vinh";
        }

        private void CotSoVe_ChuyenBay()
        {
            List<KeyValuePair<string, int>> SoVe = new List<KeyValuePair<string, int>>();

            SoVe.Add(new KeyValuePair<string, int>("B52", 100));
            SoVe.Add(new KeyValuePair<string, int>("Vietnam Airlines", 200));
            SoVe.Add(new KeyValuePair<string, int>("Vietjet Air", 150));
            SoVe.Add(new KeyValuePair<string, int>("Bamboo Airways", 260));
            SoVe.Add(new KeyValuePair<string, int>("Jetstar Pacific", 230));

            //Setting data for column chart

            Col_CountTicket.ItemsSource = SoVe;
        }

        private void CotDoanhThu_ChuyenBay()
        {
            List<KeyValuePair<string, int>> SoVe = new List<KeyValuePair<string, int>>();

            SoVe.Add(new KeyValuePair<string, int>("B52", 2500));
            SoVe.Add(new KeyValuePair<string, int>("Vietnam Airlines", 3000));
            SoVe.Add(new KeyValuePair<string, int>("Vietjet Air", 2800));
            SoVe.Add(new KeyValuePair<string, int>("Bamboo Airways", 4000));
            SoVe.Add(new KeyValuePair<string, int>("Jetstar Pacific", 2000));

            //Setting data for column chart

            Col_TurnoverFlight.ItemsSource = SoVe;
        }

        private string fullName;

        public string FullName
        {
            get
            {
                return fullName;
            }

            set
            {
                fullName = value;
                OnPropertyChanged("FullName");
            }
        }
    }
}
