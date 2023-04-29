using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using CrystalDecisions;

namespace QuanLiBanSach_HSK
{
    public partial class INBaocCao : Form
    {
        public INBaocCao()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["db_qlbs"].ConnectionString;
            using(SqlConnection cnn = new SqlConnection(constr))
            {
                using(SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_HoadonKH";
                    cmd.Connection = cnn;
                    cmd.Parameters.Add("@maKH", SqlDbType.VarChar).Value = textBox1.Text;
                    cmd.Parameters.Add("@ngaymua", SqlDbType.Date).Value = maskedTextBox1.Text;
                    cnn.Open();
                    using(SqlDataAdapter adp = new SqlDataAdapter(cmd))
                    {
                        DataTable db = new DataTable();
                        adp.Fill(db);
                        BaoCaoHDKH rptS = new BaoCaoHDKH();
                        rptS.SetDataSource(db);
                        INBaocCao print = new INBaocCao();
                        print.crystalReportViewer1.ReportSource = rptS;
                        print.ShowDialog();
                    }
                }
            }
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {


        }

        private void btnKPI_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["db_qlbs"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using(SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "ThongkeKPI";
                    cmd.Connection = cnn;
                    cmd.Parameters.Add("@maNV", SqlDbType.VarChar).Value = txtTenNV.Text;
                    cmd.Parameters.Add("@year", SqlDbType.Int).Value = txtnam.Text;
                    cmd.Parameters.Add("@month", SqlDbType.Int).Value = txtThang.Text;
                    cnn.Open();
                    using(SqlDataAdapter adp = new SqlDataAdapter(cmd))
                    {
                        DataTable db = new DataTable();
                        adp.Fill(db);
                        ThongKeKPI_NhanVien kpi = new ThongKeKPI_NhanVien();
                        kpi.SetDataSource(db);
                        INBaocCao print = new INBaocCao();
                        print.crystalReportViewer1.ReportSource = kpi;
                        print.Show();
                    }
                }
            }
        }
    }
}
