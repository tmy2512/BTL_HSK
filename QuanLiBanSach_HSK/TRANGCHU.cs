using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;   

namespace QuanLiBanSach_HSK
{
    public partial class TRANGCHU : Form
    {
       // string username = "", password = "", maquyen = "";
        public TRANGCHU()
        {
            InitializeComponent();
        }
        //public TRANGCHU( string username, string password, string maquyen)
        //{
        //    InitializeComponent();
        //    this.username = username;
        //    this.password = password;
        //    this.maquyen = maquyen;
        //}

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void TRANGCHU_Load(object sender, EventArgs e)
        {

        }

        private void đăngNhậpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LOGIN login = new LOGIN();
            login.Show();
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LOGIN login = new LOGIN();
            login.Show();
            this.Close();
        }

        private void quảnLíSáchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QUANLISACH qls = new QUANLISACH();
            qls.Show();
            //if (maquyen == "0")
            //{
            //    MessageBox.Show("b k có quyen truy cap");

            //}
        }

        private void quảnLíNXBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QUANLINXB qlNXB = new QUANLINXB();
            qlNXB.Show();
        }

        private void quảnLíHoáĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HOADON invoice = new HOADON();
            invoice.Show();
        }

        private void quảnLíChiTiếtHoáĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CHITIETHOADON invoicedetail = new CHITIETHOADON();
            invoicedetail.Show();
        }

        private void quảnLíNhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QUANLINHANVIEN qlnv = new QUANLINHANVIEN();
            qlnv.Show();
        }

        private void quảnLíKháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QUANLIKHACHHANG qlkh = new QUANLIKHACHHANG();
            qlkh.Show();
        }

        private void inHoáĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            INBaocCao print = new INBaocCao();
            print.Show();
        }
    }
}
