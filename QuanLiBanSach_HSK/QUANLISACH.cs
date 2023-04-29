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
    public partial class QUANLISACH : Form
    {
        public QUANLISACH()
        {
            InitializeComponent();
        }

        private void QUANLISACH_Load(object sender, EventArgs e)
        {
            load_dgvSach();
        }
        void load_dgvSach()
        {
            string constr = ConfigurationManager.ConnectionStrings["db_qlbs"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using(SqlCommand cmd = new SqlCommand("Select *from tblSach", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    cnn.Open();
                    using(SqlDataAdapter adp = new SqlDataAdapter(cmd)) // chuyển dữ liệu về
                    {
                        DataTable db = new DataTable();//tạo kho ảo để lưu trữ dlieu
                        adp.Fill(db); // đổ dữ liệu vào kho
                        dgv_Sach.DataSource = db;
                    }
                }
            }
        }
        //hàm reset dữ liệu
        void resetData()
        {
            txtmaSach.Text = "";
            txttenSach.Text = "";
            cboNXB.Text = "";
            txtsoLuong.Text = "";
            txtdonGia.Text = "";
            txtnamXB.Text = "";
        }
        // hàm check dữ liệu tồn tại
        public bool checkValidation()
        {
            if(txttenSach.Text == "")
            {
                MessageBox.Show("Please enter your book's name", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txttenSach.Focus();
                return false;
            }
            if (txtmaSach.Text == "")
            {
                MessageBox.Show("Please enter your book's ID", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtmaSach.Focus();
                return false;
            }
            if (cboNXB.Text == "")
            {
                MessageBox.Show("Please enter your book's publisher", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboNXB.Focus();
                return false;
            }
            if (txtsoLuong.Text == "")
            {
                MessageBox.Show("Please enter your book's quantity", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtsoLuong.Focus();
                return false;
            }
            if (txtnamXB.Text == "")
            {
                MessageBox.Show("Please enter your book's debut year", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtnamXB.Focus();
                return false;
            }
            if (txtdonGia.Text == "")
            {
                MessageBox.Show("Please enter your book's price", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtdonGia.Focus();
                return false;
            }
            return true;
        }
        // viết cell-click event
        private void dgv_Sach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtmaSach.Text = dgv_Sach.CurrentRow.Cells["sMaSach"].Value.ToString();
            txttenSach.Text = dgv_Sach.CurrentRow.Cells["sTenSach"].Value.ToString();
            cboNXB.Text = dgv_Sach.CurrentRow.Cells["sMaNXB"].Value.ToString();
            txtsoLuong.Text = dgv_Sach.CurrentRow.Cells["iSoLuong"].Value.ToString();
            txtdonGia.Text = dgv_Sach.CurrentRow.Cells["fDonGia"].Value.ToString();
            txtnamXB.Text = dgv_Sach.CurrentRow.Cells["sNamXB"].Value.ToString();
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["db_qlbs"].ConnectionString;
            using(SqlConnection cnn = new SqlConnection(constr))
            {
                using(SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "insert_SBook";
                    cmd.Connection = cnn;
                    cmd.Parameters.Add("@maSach", SqlDbType.VarChar).Value = txtmaSach.Text;
                    cmd.Parameters.Add("@tenSach", SqlDbType.NVarChar).Value = txttenSach.Text;
                    cmd.Parameters.Add("@maNXB", SqlDbType.VarChar).Value = cboNXB.SelectedItem;
                    cmd.Parameters.Add("@soLuong", SqlDbType.Int).Value = (txtsoLuong.Text);
                    cmd.Parameters.Add("@donGia", SqlDbType.Float).Value = txtdonGia.Text;
                    cmd.Parameters.Add("@namXB", SqlDbType.Char).Value = txtnamXB.Text;
                    if (checkValidation())
                    {
                        try
                        {
                            cnn.Open();
                            cmd.ExecuteNonQuery();
                            cnn.Close();
                            load_dgvSach();
                            resetData();
                        }
                        catch
                        {
                            MessageBox.Show("Can't insert duplicate key", "Notification",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            this.Close();
                        }
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
                    cmd.CommandText = "edit_Book";
                    cmd.Connection = cnn;
                    cmd.Parameters.Add("@maSach", SqlDbType.NVarChar).Value = txtmaSach.Text;
                    cmd.Parameters.Add("@tenSach", SqlDbType.NVarChar).Value = txttenSach.Text;
                    cmd.Parameters.Add("@maNXB", SqlDbType.VarChar).Value = cboNXB.SelectedItem;
                    cmd.Parameters.Add("@soLuong", SqlDbType.Int).Value = (txtsoLuong.Text);
                    cmd.Parameters.Add("@donGia", SqlDbType.Float).Value = txtdonGia.Text;
                    cmd.Parameters.Add("@namXB", SqlDbType.Char).Value = txtnamXB.Text;
                    if (checkValidation())
                    {
                        cnn.Open();
                        cmd.ExecuteNonQuery();
                        cnn.Close();
                        load_dgvSach();
                        resetData();
                    }
                    
                }
            }
        }

        private void btn_Del_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["db_qlbs"].ConnectionString;
            using(SqlConnection cnn = new SqlConnection(constr))
            {
                using(SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "del_Book";
                    cmd.Connection = cnn;
                    cmd.Parameters.Add("@maSach", SqlDbType.VarChar).Value = txtmaSach.Text;
                    cnn.Open();
                    if (checkValidation())
                    {
                        try
                        {
                            if(MessageBox.Show("Do you wanna delete this record?", "Notification", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                cmd.ExecuteNonQuery();
                                resetData();
                            }
                            else
                            {
                                resetData();
                            }
                            
                        }
                        catch
                        {
                            MessageBox.Show("Can't delete this record as maSach is refered to other table", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    cnn.Close();
                    load_dgvSach();
                    resetData();
                }
            }
        }
        // hàm láy dữ liệu
        //public static DataTable
        private void btn_Search_Click(object sender, EventArgs e)
        {
            string sql = "Select *from tblSach where 1=1";
            string constr = ConfigurationManager.ConnectionStrings["db_qlbs"].ConnectionString;
            using(SqlConnection cnn = new SqlConnection(constr))
            {
                using(SqlCommand cmd = new SqlCommand(sql, cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql;
                    if (checkValidation())
                    {  
                        }
                    else
                    {
                            if (txtmaSach.Text != "")
                                sql += "AND sMaSach LIKE N'%" + txtmaSach.Text + "%'";
                            if (txttenSach.Text != "")
                                sql += "AND sTenSach LIKE N'%" + txttenSach.Text + "%'";
                            if (cboNXB.Text != "")
                                sql += "AND sMaNXB LIKE N'%" + cboNXB.SelectedValue + "%'";
                            if (txtsoLuong.Text != "")
                                sql += "AND iSoLuong LIKE N'%" + txtsoLuong.Text + "%'";
                            if (txtdonGia.Text != "")
                                sql += "AND fDongia LIKE N'%" + txtdonGia.Text + "%'";
                            if (txtnamXB.Text != "")
                                sql += "AND sNamXB LIKE N'%" + txtnamXB.Text + "%'";
                        using (SqlDataAdapter adp = new SqlDataAdapter(sql, cnn))
                        {
                            DataTable db = new DataTable();
                            adp.Fill(db);
                            if (db.Rows.Count == 0)
                                MessageBox.Show("There aren't any records as found", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            else
                                MessageBox.Show("There are " + db.Rows.Count + " as found", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dgv_Sach.DataSource = db;
                            resetData();
                        }
                        
                            
                        
                    }
                        

                    }
                }
            }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you know exit Sach form?", "Notification", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                this.Close();
            else
                this.Show();
        }

        
        private void btnPrint_Click(object sender, EventArgs e)
        {
          
        }
    }

      
    }

