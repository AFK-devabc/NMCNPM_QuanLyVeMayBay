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
using NNCNPM_QuanLyVeMayBay.DAO;

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

            Load();

            FullName = "Hong Truong Vinh";
        }

        void Load()
        {
            CotSoVe_ChuyenBay();
            CotDoanhThu_ChuyenBay();
        }

        private void CotSoVe_ChuyenBay()
        {
            Col_CountTicket.ItemsSource = BaoCaoDoanhThuDAO.Instance.LaySoChuyenBayTheoNam(2021);
        }

        private void CotDoanhThu_ChuyenBay()
        {
            Line_TurnoverFlight.ItemsSource = BaoCaoDoanhThuDAO.Instance.LayDoanhThuTheoNam(2021);
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
