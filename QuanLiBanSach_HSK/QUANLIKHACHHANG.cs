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
    public partial class QUANLIKHACHHANG : Form
    {
        public QUANLIKHACHHANG()
        {
            InitializeComponent();
        }

        private void QUANLIKHACHHANG_Load(object sender, EventArgs e)
        {
            load_dgvKH();
        }
        void load_dgvKH()
        {
            string constr = ConfigurationManager.ConnectionStrings["db_qlbs"].ConnectionString;
            using(SqlConnection cnn = new SqlConnection(constr))
            {
                using(SqlCommand cmd = new SqlCommand("Select *from tblKhachHang", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    cnn.Open();
                    using(SqlDataAdapter adp = new SqlDataAdapter(cmd))// keo du lieu tu khi tao cau lenh cmd
                    {
                        DataTable db = new DataTable();
                        adp.Fill(db);
                        dgv_KH.DataSource = db;
                    }
                }
            }
        }
        void ressetData1()
        {
            txtmaKH.Text = "";
            txttenKH.Text = "";
            cboGen.Text = "";
            mtbBirth.Text = "";
            txtPhone.Text = "";
            txtAdd.Text = "";
        }

        private void dgv_KH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtmaKH.Text = dgv_KH.CurrentRow.Cells["sMaKH"].Value.ToString();
            txttenKH.Text = dgv_KH.CurrentRow.Cells["sTenKH"].Value.ToString();
            cboGen.Text = dgv_KH.CurrentRow.Cells["sGioiTinh"].Value.ToString();
            mtbBirth.Text = dgv_KH.CurrentRow.Cells["dNgaySinh"].Value.ToString();
            txtPhone.Text = dgv_KH.CurrentRow.Cells["sSoDT"].Value.ToString();
            txtAdd.Text = dgv_KH.CurrentRow.Cells["sDiaChi"].Value.ToString();
        }
        public bool checkValidation()
        {
            if(txtmaKH.Text == "")
            {
                MessageBox.Show("Please enter your Customer's ID", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtmaKH.Focus();
                return false;
            }
            if (txttenKH.Text == "")
            {
                MessageBox.Show("Please enter your Customer's name", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txttenKH.Focus();
                return false;
            }
            if (cboGen.Text == "")
            {
                MessageBox.Show("Please enter your Customer's generate", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboGen.Focus();
                return false;
            }
            if (mtbBirth.Text == "")
            {
                MessageBox.Show("Please enter your Customer's birthday", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                mtbBirth.Focus();
                return false;
            }
            if (txtPhone.Text == "")
            {
                MessageBox.Show("Please enter your Customer's phone", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPhone.Focus();
                return false;
            }
            if (txtAdd.Text == "")
            {
                MessageBox.Show("Please enter your Customer's address", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAdd.Focus();
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
                    cmd.CommandText = "insert_Customer";
                    cmd.Connection = cnn;
                    cmd.Parameters.Add("@maKH", SqlDbType.VarChar).Value = txtmaKH.Text;
                    cmd.Parameters.Add("@tenKH", SqlDbType.NVarChar).Value = txttenKH.Text;
                    cmd.Parameters.Add("@Gioitinh", SqlDbType.NVarChar).Value = cboGen.Text;
                    cmd.Parameters.Add("@NgaySinh", SqlDbType.Date).Value = (mtbBirth.Text);
                    cmd.Parameters.Add("@soDT", SqlDbType.Char).Value = txtPhone.Text;
                    cmd.Parameters.Add("@DiaChi", SqlDbType.NVarChar).Value = txtAdd.Text;
                    if (checkValidation())
                    {
                        try
                        {
                            cnn.Open();
                            cmd.ExecuteNonQuery();
                            load_dgvKH();
                            ressetData1();
                            cnn.Close();
                        }
                        catch
                        {
                            MessageBox.Show("Can't insert duplicate key", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            ressetData1();
                        }
                        
                    }
                    
                }
            }
        }

        private void btnResetdata_Click(object sender, EventArgs e)
        {
            ressetData1();
        }

        private void btn_Edit_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["db_qlbs"].ConnectionString;
            using(SqlConnection cnn = new SqlConnection(constr))
            {
                using(SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "edit_Customer";
                    cmd.Connection = cnn;
                    cmd.Parameters.Add("@maKH", SqlDbType.VarChar).Value = txtmaKH.Text;
                    cmd.Parameters.Add("@tenKH", SqlDbType.NVarChar).Value = txttenKH.Text;
                    cmd.Parameters.Add("@Gioitinh", SqlDbType.NVarChar).Value = cboGen.Text;
                    cmd.Parameters.Add("@NgaySinh", SqlDbType.Date).Value = mtbBirth.Text;
                    cmd.Parameters.Add("@soDT", SqlDbType.Char).Value = txtPhone.Text;
                    cmd.Parameters.Add("@DiaChi", SqlDbType.NVarChar).Value = txtAdd.Text;
                    if (checkValidation())
                    {
                        cnn.Open();
                        cmd.ExecuteNonQuery();
                        cnn.Close();
                        load_dgvKH();
                        ressetData1();
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
                    cmd.CommandText = "del_Customer";
                    cmd.Connection = cnn;
                    cmd.Parameters.Add("@maKH", SqlDbType.VarChar).Value = txtmaKH.Text;
                    cnn.Open();
                    if (checkValidation())
                    {
                        try
                        {
                            if (MessageBox.Show("Are you sure to delete this record?", "Notification", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                cmd.ExecuteNonQuery();
                                load_dgvKH();
                                ressetData1();
                            }
                            else
                            {
                                this.Show();
                                ressetData1();
                            }
                        }
                        catch
                        {
                            MessageBox.Show("Can't delete the record which relate to foreign key", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            this.Show();
                            ressetData1();
                        }
                        
                    }
                    cnn.Close();
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
                    cmd.CommandText = "search_Customer";
                    cmd.Connection = cnn;
                    cmd.Parameters.Add("@maKH", SqlDbType.VarChar).Value = txtmaKH.Text;
                    if (txtmaKH.Text == "")
                    {
                        MessageBox.Show("Please enter your Customer's ID", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtmaKH.Focus();
                    }
                    else
                    {
                        cnn.Open();
                        using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                        {
                            DataTable db = new DataTable();
                            adp.Fill(db);
                            if (db.Rows.Count == 0)
                                MessageBox.Show("There aren't any record as found result", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            else
                                MessageBox.Show("There are " + db.Rows.Count + " as found result", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dgv_KH.DataSource = db;
                            ressetData1();
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
