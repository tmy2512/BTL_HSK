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
using System.Configuration;

namespace QuanLiBanSach_HSK
{
    public partial class CHITIETHOADON : Form
    {
        public CHITIETHOADON()
        {
            InitializeComponent();
        }
        public bool checkValidation()
        {
            if(txtmaHD.Text == "")
            {
                MessageBox.Show("Please enter your Invoice's ID", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtmaHD.Focus();
                return false;
            }
            if (cbomaSach.Text == "")
            {
                MessageBox.Show("Please enter your Book's ID", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbomaSach.Focus();
                return false;
            }
            if (txtQuantity.Text == "")
            {
                MessageBox.Show("Please enter your Product's quantity", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtQuantity.Focus();
                return false;
            }
            if (txtPrice.Text == "")
            {
                MessageBox.Show("Please enter your Product's price", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPrice.Focus();
                return false;
            }
            if (txtDiscount.Text == "")
            {
                MessageBox.Show("Please enter your Invoice's discount", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDiscount.Focus();
                return false;
            }
            return true;
        }
        void load_InvoiceDetail()
        {
            string constr = ConfigurationManager.ConnectionStrings["db_qlbs"].ConnectionString;
            using(SqlConnection cnn = new SqlConnection(constr))
            {
                using(SqlCommand cmd = new SqlCommand("Select *from tblChiTietHoaDon", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    cnn.Open();
                    using(SqlDataAdapter adp = new SqlDataAdapter("Select *from tblChiTietHoaDon", cnn))
                    {
                        DataTable db = new DataTable();
                        adp.Fill(db);
                        dgv_CTHD.DataSource = db;
                        cnn.Close();
                    }
                }
            }
        }
        void load_cboMaSach()
        {
            string constr = ConfigurationManager.ConnectionStrings["db_qlbs"].ConnectionString;
            using(SqlConnection cnn = new SqlConnection(constr))
            {
                using(SqlCommand cmd = new SqlCommand("Select sMaSach from tblSach", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    using(SqlDataAdapter adp = new SqlDataAdapter("Select sMaSach from tblSach", cnn))
                    {
                        DataTable db = new DataTable();
                        adp.Fill(db);
                        cbomaSach.DataSource = db;
                        cbomaSach.DisplayMember = "sMaSach";
                    }
                }
            }
        }
        void resetData()
        {
            txtmaHD.Text = "";
            cbomaSach.Text = "";
            txtPrice.Text = "";
            txtQuantity.Text = "";
            txtDiscount.Text = "";
        }
        private void CHITIETHOADON_Load(object sender, EventArgs e)
        {
            load_InvoiceDetail();
            load_cboMaSach();
        }

        private void dgv_CTHD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtmaHD.Text = dgv_CTHD.CurrentRow.Cells["sMaHD"].Value.ToString();
            cbomaSach.Text = dgv_CTHD.CurrentRow.Cells["sMaSach"].Value.ToString();
            txtQuantity.Text = dgv_CTHD.CurrentRow.Cells["iSoluong"].Value.ToString();
            txtPrice.Text = dgv_CTHD.CurrentRow.Cells["fGiaban"].Value.ToString();
            txtDiscount.Text = dgv_CTHD.CurrentRow.Cells["fMucgiamgia"].Value.ToString();
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["db_qlbs"].ConnectionString;
            using(SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "insert_Invoicedetail";
                    cmd.Connection = cnn;
                    cmd.Parameters.Add("@maHD", SqlDbType.VarChar).Value = txtmaHD.Text;
                    cmd.Parameters.Add("@maSach", SqlDbType.VarChar).Value = cbomaSach.Text;
                    cmd.Parameters.Add("@soLuong", SqlDbType.Int).Value = (txtQuantity.Text);
                    cmd.Parameters.Add("@giaBan", SqlDbType.Float).Value = (txtPrice.Text);
                    cmd.Parameters.Add("@mucGiamgia", SqlDbType.Float).Value = txtDiscount.Text;
                    if (checkValidation())
                    {
                            cnn.Open();
                            cmd.ExecuteNonQuery();
                            load_InvoiceDetail();
                            resetData();
                            cnn.Close();
                    }
                }
            }
        }

        private void btn_Edit_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["db_qlbs"].ConnectionString;
            using(SqlConnection cnn = new SqlConnection(constr))
            {
                using(SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "edit_InvoiceDetail";
                    cmd.Connection = cnn;
                    cmd.Parameters.Add("@maHD", SqlDbType.VarChar).Value = txtmaHD.Text;
                    cmd.Parameters.Add("@maSach", SqlDbType.VarChar).Value = cbomaSach.Text;
                    cmd.Parameters.Add("@soLuong", SqlDbType.Int).Value = (txtQuantity.Text);
                    cmd.Parameters.Add("@giaBan", SqlDbType.Float).Value = (txtPrice.Text);
                    cmd.Parameters.Add("@mucGiamgia", SqlDbType.Float).Value = txtDiscount.Text;
                    if (checkValidation())
                    {
                        if(MessageBox.Show("Do you wanna modify this record?", "Notification", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            cnn.Open();
                            cmd.ExecuteNonQuery();
                            load_InvoiceDetail();
                            resetData();
                            cnn.Close();
                        }
                        else
                        {
                            resetData();
                        }
                    }
                }
            }
        }

        private void btn_Del_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["db_qlbs"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using(SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "del_InvoiceDetail";
                    cmd.Connection = cnn;
                    cmd.Parameters.Add("@maHD", SqlDbType.VarChar).Value = txtmaHD.Text;
                    cmd.Parameters.Add("@maSach", SqlDbType.VarChar).Value = cbomaSach.Text;
                    if (checkValidation())
                    {
                        if(MessageBox.Show("Do you wanna delete?", "Notification", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            cnn.Open();
                            cmd.ExecuteNonQuery();
                            load_InvoiceDetail();
                            resetData();
                            cnn.Close();
                        }
                        else
                        {
                            resetData();
                            this.Show();
                        }
                        
                    }
                }
            }
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["db_qlbs"].ConnectionString;
           // string sql = "Select *from tblChiTietHoaDon where 1=1";
            using(SqlConnection cnn = new SqlConnection(constr))
            {
                using(SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "search_InvoiceDetail";
                    cmd.Connection = cnn;
                    cmd.Parameters.Add("@maHD", SqlDbType.VarChar).Value = txtmaHD.Text;

                    if (txtmaHD.Text == "")
                    {
                        MessageBox.Show("Please enter Invoice's ID");
                        txtmaHD.Focus();
                    }
                    else
                    {
                        cnn.Open();
                        using(SqlDataAdapter adp = new SqlDataAdapter(cmd))
                        {
                            DataTable db = new DataTable();
                            adp.Fill(db);
                            if (db.Rows.Count == 0)
                                MessageBox.Show("There aren't any records as found", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            else
                                MessageBox.Show("There are " + db.Rows.Count + " records as found", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dgv_CTHD.DataSource = db;
                            resetData();
                        }
                        cnn.Close();
                    }
                }
            }
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to exit?", "Notification", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                this.Close();
            else
                this.Show();
        }
    }
}
