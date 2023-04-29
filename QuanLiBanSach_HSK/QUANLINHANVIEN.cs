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
    public partial class QUANLINHANVIEN : Form
    {
        public QUANLINHANVIEN()
        {
            InitializeComponent();
        }
        void  Decentralization()
        {
            //InitializeComponent();
            if(Class1.maquyen == false)
            {
                btn_Add.Enabled = btn_Edit.Enabled = btn_Del.Enabled = false;
                dgv_NV.Columns["sPassWord"].Visible = false;
            }
        }

        private void QUANLINHANVIEN_Load(object sender, EventArgs e)
        {
            load_NV();
            Decentralization();
        }
        void load_NV()
        {
            string constr = ConfigurationManager.ConnectionStrings["db_qlbs"].ConnectionString;
            using(SqlConnection cnn = new SqlConnection(constr))
            {
                using(SqlCommand cmd = new SqlCommand("Select *from tblNhanVien", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    cnn.Open();
                    using(SqlDataAdapter adp = new SqlDataAdapter(cmd))
                    {
                        DataTable db = new DataTable();
                        adp.Fill(db);
                        dgv_NV.DataSource = db;
                        cnn.Close();
                    }
                }
            }
        }
        void resetData()
        {
            txtmaNV.Text = "";
            txttenNV.Text = "";
            cboGen.Text = "";
            mtbBirth.Text = "";
            txtPhone.Text = "";
            txtAddress.Text = "";
            txtSalary.Text = "";
            txthesoLuong.Text = "";
            cbomaQuyen.Text = "";
            txtPW.Text = "";
        }
        public bool checkValidation()
        {
            if(txtmaNV.Text == "")
            {
                MessageBox.Show("Please enter staff's ID", "Noticationn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtmaNV.Focus();
                return false;
            }
            if (txttenNV.Text == "")
            {
                MessageBox.Show("Please enter staff's name", "Noticationn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txttenNV.Focus();
                return false;
            }
            if (cboGen.Text == "")
            {
                MessageBox.Show("Please enter staff's generate", "Noticationn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboGen.Focus();
                return false;
            }
            if (mtbBirth.Text == "")
            {
                MessageBox.Show("Please enter staff's birthday", "Noticationn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                mtbBirth.Focus();
                return false;
            }
            if (txtPhone.Text == "")
            {
                MessageBox.Show("Please enter staff's phone", "Noticationn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPhone.Focus();
                return false;
            }
            if (txtAddress.Text == "")
            {
                MessageBox.Show("Please enter staff's Address", "Noticationn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAddress.Focus();
                return false;
            }
            if (txtSalary.Text == "")
            {
                MessageBox.Show("Please enter staff's Salary", "Noticationn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSalary.Focus();
                return false;
            }
            if (txthesoLuong.Text == "")
            {
                MessageBox.Show("Please enter staff's HeSoLuong", "Noticationn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txthesoLuong.Focus();
                return false;
            }
            if (cbomaQuyen.Text == "")
            {
                MessageBox.Show("Please enter staff's maQuyen", "Noticationn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbomaQuyen.Focus();
                return false;
            }
            if (txtPW.Text == "")
            {
                MessageBox.Show("Please enter staff's password", "Noticationn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPW.Focus();
                return false;
            }
            return true;

        }

        private void dgv_NV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtmaNV.Text = dgv_NV.CurrentRow.Cells["sMaNV"].Value.ToString();
            txttenNV.Text = dgv_NV.CurrentRow.Cells["sTenNV"].Value.ToString();
            cboGen.Text = dgv_NV.CurrentRow.Cells["sGioiTinh"].Value.ToString();
            mtbBirth.Text = dgv_NV.CurrentRow.Cells["dNgaySinh"].Value.ToString();
            txtAddress.Text = dgv_NV.CurrentRow.Cells["sDiaChi"].Value.ToString();
            txtPhone.Text = dgv_NV.CurrentRow.Cells["sSoDT"].Value.ToString();
            txtSalary.Text = dgv_NV.CurrentRow.Cells["fLuongcoban"].Value.ToString();
            txthesoLuong.Text = dgv_NV.CurrentRow.Cells["fHSL"].Value.ToString();
            cbomaQuyen.Text = dgv_NV.CurrentRow.Cells["bMaQuyenTruyCap"].Value.ToString();
            txtPW.Text = dgv_NV.CurrentRow.Cells["sPassWord"].Value.ToString();
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["db_qlbs"].ConnectionString;
            using(SqlConnection cnn = new SqlConnection(constr))
            {
                using(SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "insert_Staff";
                    cmd.Connection = cnn;
                    cmd.Parameters.Add("@maNV", SqlDbType.VarChar).Value = txtmaNV.Text;
                    cmd.Parameters.Add("@tenNV", SqlDbType.NVarChar).Value = txttenNV.Text;
                    cmd.Parameters.Add("@gioiTinh", SqlDbType.NVarChar).Value = cboGen.Text;
                    cmd.Parameters.Add("@ngaySinh", SqlDbType.Date).Value = mtbBirth.Text;
                    cmd.Parameters.Add("@diaChi", SqlDbType.NVarChar).Value = txtAddress.Text;
                    cmd.Parameters.Add("@soDT", SqlDbType.Char).Value = txtPhone.Text;
                    cmd.Parameters.Add("@luongCB", SqlDbType.Float).Value = txtSalary.Text;
                    cmd.Parameters.Add("@hsluong", SqlDbType.Float).Value = txthesoLuong.Text;
                    cmd.Parameters.Add("@maQuyen", SqlDbType.Bit).Value = cbomaQuyen.SelectedItem;
                    cmd.Parameters.Add("@passWord", SqlDbType.VarChar).Value = txtPW.Text;
                    cnn.Open();
                    if (checkValidation())
                    {
                        try
                        {
                            cmd.ExecuteNonQuery();
                            load_NV();
                            resetData();
                        }
                        catch
                        {
                            MessageBox.Show("Primary key is not duplicated");
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
                    cmd.CommandText = "edit_Staff";
                    cmd.Connection = cnn;
                    cmd.Parameters.Add("@maNV", SqlDbType.VarChar).Value = txtmaNV.Text;
                    cmd.Parameters.Add("@tenNV", SqlDbType.NVarChar).Value = txttenNV.Text;
                    cmd.Parameters.Add("@gioiTinh", SqlDbType.NVarChar).Value = cboGen.SelectedItem;
                    cmd.Parameters.Add("@ngaySinh", SqlDbType.Date).Value = mtbBirth.Text;
                    cmd.Parameters.Add("@diaChi", SqlDbType.NVarChar).Value = txtAddress.Text;
                    cmd.Parameters.Add("@soDT", SqlDbType.Char).Value = txtPhone.Text;
                    cmd.Parameters.Add("@luongCB", SqlDbType.Float).Value = txtSalary.Text;
                    cmd.Parameters.Add("@hsluong", SqlDbType.Float).Value = txthesoLuong.Text;
                    cmd.Parameters.Add("@maQuyen", SqlDbType.Bit).Value = cbomaQuyen.SelectedItem;
                    cmd.Parameters.Add("@passWord", SqlDbType.VarChar).Value = txtPW.Text;
                    if (checkValidation())
                    {
                        cnn.Open();
                        cmd.ExecuteNonQuery();
                        load_NV();
                        resetData();
                        cnn.Close();
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
                    cmd.CommandText = "del_Staff";
                    cmd.Connection = cnn;
                    cmd.Parameters.Add("@maNV", SqlDbType.VarChar).Value = txtmaNV.Text;
                    if (checkValidation())
                    {
                        try
                        {
                            if(MessageBox.Show("Are you sure to delete this record?", "Notification", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                cnn.Open();
                                cmd.ExecuteNonQuery();
                                load_NV();
                                resetData();
                                cnn.Close();
                            }
                            else
                            {
                                this.Show();
                            }
                        }
                        catch
                        {
                            MessageBox.Show("Can't delete this record which relate to foreign key", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            this.Show();
                        }
                        
                    }
                }
            }
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            //string sql = "select *from tblNhanVien WHERE 1=1";
            string constr = ConfigurationManager.ConnectionStrings["db_qlbs"].ConnectionString;
            using(SqlConnection cnn = new SqlConnection(constr))
            {
                using(SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "search_Staff";
                    cmd.Connection = cnn;
                    cmd.Parameters.Add("@maNV", SqlDbType.VarChar).Value = txtmaNV.Text;
                    if(txtmaNV.Text == "")
                    {
                        MessageBox.Show("Please enter your Staff's ID", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtmaNV.Focus();
                    }
                    else
                    {
                        cnn.Open();
                        using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                        {
                            DataTable db = new DataTable();
                            adp.Fill(db);
                            if(db.Rows.Count == 0)
                            {
                                MessageBox.Show("There aren't any records as found", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("There are " + db.Rows.Count + " records as found", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            dgv_NV.DataSource = db;
                        }
                        cnn.Close();
                    }
                }
            }
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you wanna exit?", "Notification", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                this.Close();
            else
                this.Show();
        }
    }
}
