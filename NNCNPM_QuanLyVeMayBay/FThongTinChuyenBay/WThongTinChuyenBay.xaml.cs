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
using System.Data;

namespace NNCNPM_QuanLyVeMayBay
{
    /// <summary>
    /// Interaction logic for WThongTinChuyenBay.xaml
    /// </summary>
   
    public partial class WThongTinChuyenBay : Window
    {
        private VMThongTinChuyenBay vmThongTinChuyenBay;
        public WThongTinChuyenBay()
        {
            InitializeComponent();
            this.vmThongTinChuyenBay = new VMThongTinChuyenBay();
            LoadFormData();
        }



        private void LoadFormData()
        {
            DataTable table = new DataTable();
            table.Clear();
            table = DataProvider.Instance.ExecuteQuery("select * from SANBAY");
            foreach (DataRow i in table.Rows)
            {
                CB_SanBayDi.Items.Add(i.ItemArray[1].ToString());
                CB_SanBayDen.Items.Add(i.ItemArray[1].ToString());
            }

            table.Clear();
            table = DataProvider.Instance.ExecuteQuery("select * from HANGVE");

            foreach(DataRow i in table.Rows)
            {
                vmThongTinChuyenBay.dataLoaiVes.Add(new DataLoaiVe(i.ItemArray[1].ToString(), i.ItemArray[2].ToString()));
            }
            DG_LoaiVe.ItemsSource = vmThongTinChuyenBay.dataLoaiVes;
        }   
     
        private void IsNumberic_only(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            var fullText = textBox.Text.Insert(textBox.SelectionStart, e.Text);
            double val;
            e.Handled = !double.TryParse(fullText, out val);
        }
    }
}
