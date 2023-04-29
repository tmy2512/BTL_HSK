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
    public partial class ChangePassWord : Form
    {
        public ChangePassWord()
        {
            InitializeComponent();
        }

        void resetData()
        {
            txtUser.Text = "";
            txtnewPW.Text = "";
            txtcfPW.Text = "";
        }
        private void ChangePassWord_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["db_qlbs"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "changePassword";
                    cmd.Connection = cnn;
                    cmd.Parameters.Add("@username", SqlDbType.VarChar).Value = txtUser.Text;
                    cmd.Parameters.Add("@pass", SqlDbType.Char).Value = txtnewPW.Text;
                    if (txtnewPW.Text == txtcfPW.Text)
                    {
                        cnn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("success");
                        LOGIN login = new LOGIN();
                        login.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("xác nhận lại");
                        this.Show();
                    }


                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                txtnewPW.UseSystemPasswordChar = true;
                txtcfPW.UseSystemPasswordChar = true;
            }
            else
            {
                txtnewPW.UseSystemPasswordChar = false;
                txtcfPW.UseSystemPasswordChar = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
