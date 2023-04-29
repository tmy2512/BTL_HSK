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
    public partial class LOGIN : Form
    {
        public LOGIN()
        {
            InitializeComponent();
        }

        private void LOGIN_Load(object sender, EventArgs e)
        {

        }
        void resetData()
        {
            txtPW.Text = "";
            txtUser.Text = "";
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["db_qlbs"].ConnectionString;
            using(SqlConnection cnn = new SqlConnection(constr))
            {
                using(SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Account";
                    cmd.Connection = cnn;
                    cmd.Parameters.AddWithValue("@username", txtUser.Text);
                    cmd.Parameters.AddWithValue("@password", txtPW.Text);
                    //cmd.Parameters.AddWithValue("@maquyen", .Text);
                    if (txtUser.Text == "")
                    {
                        MessageBox.Show("Please enter your infor");
                    }
                    else
                    {
                        cnn.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        //while (dr.Read())
                        //{
                            if (dr.Read() == true)
                            {
                                MessageBox.Show("Logged in successfully!");
                                this.Hide();
                                Class1.maquyen = (Boolean) dr["bMaQuyenTruyCap"];
                            //Class1.maquyen = dr.GetBoolean(2);
                                TRANGCHU homePage = new TRANGCHU();
                                homePage.Show();
                            }
                            else
                            {
                                MessageBox.Show("Username or Password is incorrect!");
                                this.Show();

                            }
                       // }

                    }
                }    
                
                    cnn.Close();

                }
            }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ChangePassWord change = new ChangePassWord();
            change.ShowDialog();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            resetData();
        }
        
    }
    }

