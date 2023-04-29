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
    public partial class QUANLINXB : Form
    {
        public QUANLINXB()
        {
            InitializeComponent();
        }

        private void QUANLINXB_Load(object sender, EventArgs e)
        {
            load_dgvPublisher();
        }
        // hàm reset dữ liệu
        void resetData()
        {
            txtAddress.Text = "";
            txtmaNXB.Text = "";
            txttenNXB.Text = "";
            txtPhone.Text = "";
        }
        // hàm load dữ liệu
        void load_dgvPublisher()
        {
            string constr = ConfigurationManager.ConnectionStrings["db_qlbs"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("Select *from tblNhaXuatBan", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    cnn.Open();
                    using (SqlDataAdapter adp = new SqlDataAdapter(cmd)) // chuyển dữ liệu về
                    {
                        DataTable db = new DataTable();//tạo kho ảo để lưu trữ dlieu
                        adp.Fill(db); // đổ dữ liệu vào kho
                        dgv_NXB.DataSource = db;
                    }
                }
            }
        }

        private void dgv_NXB_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtmaNXB.Text = dgv_NXB.CurrentRow.Cells["sMaNXB"].Value.ToString();
            txttenNXB.Text = dgv_NXB.CurrentRow.Cells["sTenNXB"].Value.ToString();
            txtPhone.Text = dgv_NXB.CurrentRow.Cells["sSoDT"].Value.ToString();
            txtAddress.Text = dgv_NXB.CurrentRow.Cells["sDiaChi"].Value.ToString();
        }
        // KIỂM TRA DATA
        public bool checkValidation()
        {
            if (txtmaNXB.Text == "")
            {
                MessageBox.Show("Please enter your publisher's ID", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtmaNXB.Focus();
                return false;
            }
            if(txttenNXB.Text == "")
            {
                MessageBox.Show("Please enter your publisher's name", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txttenNXB.Focus();
                return false;
            }
            if(txtPhone.Text == "")
            {
                MessageBox.Show("Please enter your publisher's phone", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPhone.Focus();
            }
            if(txtAddress.Text == "")
            {
                MessageBox.Show("Please enter your publisher's address", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAddress.Focus();
                return false;
            }
            return true;
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["db_qlbs"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using(SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "insert_Publisher";
                    cmd.Connection = cnn;
                    cmd.Parameters.Add("@maNXB", SqlDbType.VarChar).Value = txtmaNXB.Text;
                    cmd.Parameters.Add("@tenNXB", SqlDbType.NVarChar).Value = txttenNXB.Text;
                    cmd.Parameters.Add("@soDT", SqlDbType.Char).Value = txtPhone.Text;
                    cmd.Parameters.Add("@diaChi", SqlDbType.NVarChar).Value = txtAddress.Text;
                    if (checkValidation())
                    {
                        try
                        {
                            cnn.Open();
                            cmd.ExecuteNonQuery();
                            load_dgvPublisher();
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

        private void btn_Edit_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["db_qlbs"].ConnectionString;
            using(SqlConnection cnn = new SqlConnection(constr))
            {
                using(SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "edit_Publisher";
                    cmd.Connection = cnn;
                    cmd.Parameters.Add("@maNXB", SqlDbType.VarChar).Value = txtmaNXB.Text;
                    cmd.Parameters.Add("@tenNXB", SqlDbType.NVarChar).Value = txttenNXB.Text;
                    cmd.Parameters.Add("@diaChi", SqlDbType.NVarChar).Value = txtAddress.Text;
                    cmd.Parameters.Add("@soDT", SqlDbType.VarChar).Value = txtPhone.Text;
                    if (checkValidation())
                    {
                        try
                        {
                            cnn.Open();
                            cmd.ExecuteNonQuery();
                            load_dgvPublisher();
                            resetData();
                            cnn.Close();
                        }
                        catch
                        {
                            MessageBox.Show("Can't modify key");
                        }
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
                    cmd.CommandText = "del_Publisher";
                    cmd.Connection = cnn;
                    cmd.Parameters.Add("@maNXB", SqlDbType.VarChar).Value = txtmaNXB.Text;
                    cnn.Open();
                    if (checkValidation())
                    {
                        if (MessageBox.Show("Do you wanna delete this record?", "Notification", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            try
                            {
                                cmd.ExecuteNonQuery();
                                load_dgvPublisher();
                                resetData();
                                cnn.Close();
                            }
                            catch
                            {
                                MessageBox.Show("Can't delete this record which relate to foreign key", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                this.Show();
                                resetData();
                            }
                        }
                        else
                        {
                            resetData();
                        }
                        
                           
                    }
                    else
                    {
                        
                    }
                }
            }
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["db_qlbs"].ConnectionString;
            string sql = "Select *from tblNhaXuatBan where 1=1 ";
            using (SqlConnection cnn = new SqlConnection(constr))
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
                        if (txtmaNXB.Text != "")
                            sql += "AND sMaNXB LIKE '%" + txtmaNXB.Text + "%'";
                        if (txttenNXB.Text != "")
                            sql += "and sTenNXB LIKE '%" + txttenNXB.Text + "%'";
                        if (txtPhone.Text != "")
                            sql += "AND sSoDT LIKE '%" + txtPhone.Text + "%'";
                        if (txtAddress.Text != "")
                            sql += "and sDiaChi LIKE '%" + txtAddress.Text + "%'";
                        using(SqlDataAdapter adp = new SqlDataAdapter(sql, cnn))
                        {
                            DataTable db = new DataTable();
                            adp.Fill(db);
                            if(db.Rows.Count == 0)
                            {
                                MessageBox.Show("There aren't any records as found", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Show();
                            }
                            else
                            {
                                MessageBox.Show("There are" + db.Rows.Count+" records as found", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                dgv_NXB.DataSource = db;
                                resetData();
                                //this.Show();
                            }
                        }
                    }
                }
            }
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you wanna exit this form?", "Notification", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                this.Close();
            else
                this.Show();
        }
    }
}
