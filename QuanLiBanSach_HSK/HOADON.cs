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

namespace QuanLiBanSach_HSK
{
    public partial class HOADON : Form
    {
        public HOADON()
        {
            InitializeComponent();
        }

        private void HOADON_Load(object sender, EventArgs e)
        {
            load_HD();
            load_cboNV();
            load_cboKH();
        }
        public void load_cboKH()
        {
            string constr = ConfigurationManager.ConnectionStrings["db_qlbs"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using(SqlCommand cmd = new SqlCommand("Select sMaKH from tblKhachHang", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    using(SqlDataAdapter adp = new SqlDataAdapter("Select sMaKH from tblKhachHang", cnn))
                    {
                        DataTable db = new DataTable();
                        adp.Fill(db);
                        cboMaKH.DataSource = db;
                        cboMaKH.DisplayMember = "sMaKH";
                    }
                }
            }
        }
        public void load_cboNV()
        {
            string constr = ConfigurationManager.ConnectionStrings["db_qlbs"].ConnectionString;
            using(SqlConnection cnn = new SqlConnection(constr))
            {
                using(SqlCommand cmd = new SqlCommand("Select sMaNV from tblNhanVien", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter adp = new SqlDataAdapter("Select sMaNV from tblNhanVien", cnn))
                    {
                        DataTable db = new DataTable();
                        adp.Fill(db);
                        cnn.Close();
                        cboMaNV.DataSource = db;
                        cboMaNV.DisplayMember = "sMaNV";
                        
                    }
                }
            }
        }
        void load_HD()
        {
            string constr = ConfigurationManager.ConnectionStrings["db_qlbs"].ConnectionString;
            using(SqlConnection cnn = new SqlConnection(constr))
            {
                using(SqlCommand cmd = new SqlCommand("Select *from tblHoaDon", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    cnn.Open();
                    using(SqlDataAdapter adp = new SqlDataAdapter(cmd))
                    {
                        DataTable db = new DataTable();
                        adp.Fill(db);
                        dgv_HD.DataSource = db;
                        cnn.Close();
                    }
                }
            }
        }
        void resetData()
        {
            txtmaHD.Text = "";
            cboMaNV.Text = "";
            cboMaKH.Text = "";
            mtbInvoiceDate.Text = "";
            mtbPayday.Text = "";
        }
        public bool checkValidation()
        {
            if(txtmaHD.Text == "")
            {
                MessageBox.Show("Please enter your Invoice's ID", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtmaHD.Focus();
                return false;
            }
            if (cboMaKH.Text == "")
            {
                MessageBox.Show("Please enter your Customer's ID", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboMaKH.Focus();
                return false;
            }
            if (cboMaNV.Text == "")
            {
                MessageBox.Show("Please enter your Staff's ID", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboMaNV.Focus();
                return false;
            }
            if (mtbInvoiceDate.Text == "")
            {
                MessageBox.Show("Please enter your InvoiceDay", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                mtbInvoiceDate.Focus();
                return false;
            }
            if (mtbPayday.Text == "")
            {
                MessageBox.Show("Please enter your PayDay", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                mtbPayday.Focus();
                return false;
            }
            return true;
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["db_qlbs"].ConnectionString;
            using(SqlConnection cnn = new SqlConnection(constr))
            {
                using(SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "insert_Invoice";
                    cmd.Connection = cnn;
                    cmd.Parameters.Add("@maHD", SqlDbType.VarChar).Value = txtmaHD.Text;
                    cmd.Parameters.Add("@maNV", SqlDbType.VarChar).Value = cboMaNV.Text;
                    cmd.Parameters.Add("@maKH", SqlDbType.VarChar).Value = cboMaKH.Text;
                    cmd.Parameters.Add("@ngayLap", SqlDbType.Date).Value = mtbInvoiceDate.Text;
                    cmd.Parameters.Add("@ngayThanhtoan", SqlDbType.Date).Value = (mtbPayday.Text);
                    if (checkValidation())
                    {
                        try
                        {
                            cnn.Open();
                            cmd.ExecuteNonQuery();
                            load_HD();
                            resetData();
                            cnn.Close();
                        }
                        catch
                        {
                            MessageBox.Show("Can't insert duplicate key", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            resetData();
                        }
                        
                    }
                }
            }
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["db_qlbs"].ConnectionString;
            
            using(SqlConnection cnn = new SqlConnection(constr))
            {
                using(SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "search_Invoice";
                    cmd.Connection = cnn;
                    cnn.Open();
                    if (txtmaHD.Text == "") { MessageBox.Show("Please enter your Invoice's ID", "Notification", MessageBoxButtons.OK); txtmaHD.Focus(); }
                    else
                    {
                        cmd.Parameters.Add("@maHD", SqlDbType.VarChar).Value = txtmaHD.Text;
                        using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                        {
                            DataTable db = new DataTable();
                            adp.Fill(db);
                            if (db.Rows.Count == 0)
                                MessageBox.Show("There aren't any records as found", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            else
                                MessageBox.Show("There are " + db.Rows.Count + " records as found", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dgv_HD.DataSource = db;

                        }
                    }
                    //for (int i = 0; i<dgv_HD.Rows.Count - 1; i++)
                    //{
                    //    if(dgv_HD[0, i].Value.ToString() == txtmaHD.Text)
                    //    {
                    //        dgv_HD.Rows.Add(dgv_HD[0, i].Value, dgv_HD[1, i].Value, dgv_HD[2, i].Value, dgv_HD[3, i].Value, dgv_HD[4, i].Value);
                    //    }
                    //}
                }
            }
        }

        private void cboMaNV_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void btn_Edit_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["db_qlbs"].ConnectionString;
            using(SqlConnection cnn = new SqlConnection(constr))
            {
                using(SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "edit_Invoice";
                    cmd.Connection = cnn;
                    cmd.Parameters.Add("@maHD", SqlDbType.VarChar).Value = txtmaHD.Text;
                    cmd.Parameters.Add("@maNV", SqlDbType.VarChar).Value = cboMaNV.Text;
                    cmd.Parameters.Add("@maKH", SqlDbType.VarChar).Value = cboMaKH.Text;
                    cmd.Parameters.Add("@ngayLap", SqlDbType.Date).Value = mtbInvoiceDate.Text;
                    cmd.Parameters.Add("@ngayThanhtoan", SqlDbType.Date).Value = mtbPayday.Text;
                    if (checkValidation())
                    {
                        try
                        {
                            cnn.Open();
                            cmd.ExecuteNonQuery();
                            load_HD();
                            resetData();
                            cnn.Close();
                        }
                        catch
                        {
                            MessageBox.Show("Can't modify record as ID");
                        }
                    }
                }
            }
        }

        private void dgv_HD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtmaHD.Text = dgv_HD.CurrentRow.Cells["sMaHD"].Value.ToString();
            cboMaNV.Text = dgv_HD.CurrentRow.Cells["sMaNV"].Value.ToString();
            cboMaKH.Text = dgv_HD.CurrentRow.Cells["sMaKH"].Value.ToString();
            mtbInvoiceDate.Text = dgv_HD.CurrentRow.Cells["dNgaylap"].Value.ToString();
            mtbPayday.Text = dgv_HD.CurrentRow.Cells["dNgaythanhtoan"].Value.ToString();
        }

        private void btn_Del_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["db_qlbs"].ConnectionString;
            using(SqlConnection cnn = new SqlConnection(constr))
            {
                using(SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "del_Invoice";
                    cmd.Connection = cnn;
                    cmd.Parameters.Add("@maHD", SqlDbType.VarChar).Value = txtmaHD.Text;
                    if (checkValidation())
                    {
                        if (MessageBox.Show("Do you wanna delete this record?", "Notification", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {

                            try
                            {
                                cnn.Open();
                                cmd.ExecuteNonQuery();
                                load_HD();
                                resetData();
                                cnn.Close();
                            }
                            catch
                            {
                                MessageBox.Show("Can't delete record which relate to foreign key", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                resetData();
                                this.Show();
                            }

                        }
                        else
                        {
                            this.Show();
                            resetData();
                        }
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
