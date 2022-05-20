﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        #region Binding
        public event PropertyChangedEventHandler PropertyChanged;

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

        protected virtual void OnPropertyChanged(string newName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(newName));
            }
        }
        #endregion
        public WBaoCaoDoanhThu()
        {
            InitializeComponent();

            this.DataContext = this;


            LoadCbbChonNam();

            Load((int)DateTime.Now.Year);

            FullName = "Hong Truong Vinh";
        }

        void Load(int nam)
        {
            CotSoVe_ChuyenBay(nam);
            CotDoanhThu_ChuyenBay(nam);
        }

        void LoadCbbChonNam()
        {
            cbb_ChonNam.Items.Clear();
            //string currentMonth = DateTime.Now.Month.ToString();
            
            int currentYear = (int)DateTime.Now.Year;

            for (int i = currentYear; i >= 2015; i--)
            {
                cbb_ChonNam.Items.Add(i);
            }

            cbb_ChonNam.SelectedItem = (int)DateTime.Now.Year;


            cbb_ChonThang.Items.Clear();
            for (int i = 1; i <= 12; i++)
            {
                cbb_ChonThang.Items.Add(i);
            }

            cbb_ChonThang.SelectedItem = (int)DateTime.Now.Month;
        }

        private void CotSoVe_ChuyenBay(int nam)
        {
            Col_CountTicket.ItemsSource = BaoCaoDoanhThuDAO.Instance.LaySoChuyenBayTheoNam(nam);
        }

        private void CotDoanhThu_ChuyenBay(int nam)
        {
            Line_TurnoverFlight.ItemsSource = BaoCaoDoanhThuDAO.Instance.LayDoanhThuTheoNam(nam);
        }

        private void cbb_ChonNam_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            int nam = (int)(sender as ComboBox).SelectedItem;

            Load(nam);
        }

        private void cbb_ChonThang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int nam = (int)cbb_ChonNam.SelectedItem;

            //ComboBoxItem typeItem = (ComboBoxItem)(cbb_ChonThang).SelectedItem;

            //string value = typeItem.Content.ToString();

            //int thang = int.Parse(value);

            int thang = (int)cbb_ChonThang.SelectedItem;

            Duong_DoanhThuThang.ItemsSource = BaoCaoDoanhThuDAO.Instance.LayDoanhThuTheoThang(nam, thang);
            Cot_SoVeThang.ItemsSource = BaoCaoDoanhThuDAO.Instance.LaySoVeTheoThang(nam, thang);
        }

        private void btn_ExportMonth_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int nam = (int)cbb_ChonNam.SelectedItem;

                //ComboBoxItem typeItem = (ComboBoxItem)(cbb_ChonThang).SelectedItem;

                //string value = typeItem.Content.ToString();

                //int thang = int.Parse(value);

                int thang = (int)cbb_ChonThang.SelectedItem;

                BaoCaoDoanhThuDAO.Instance.XuatFileExelTheoThang(nam, thang);
            }
            catch
            {
                MessageBox.Show("Vui lòng nhập tháng");
            }

        }

        private void btn_ExportYear_Click(object sender, RoutedEventArgs e)
        {
            int nam = (int)cbb_ChonNam.SelectedItem;

            BaoCaoDoanhThuDAO.Instance.XuatFileExelTheoNam(nam);
        }
    }
}
