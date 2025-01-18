using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI_CW
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            { //Connection
                string cs = "Data Source = LAPTOP-DOH91PI2;Initial Catalog = GUI_CW; Integrated Security = True ";
                SqlConnection conn = new SqlConnection(cs);
                conn.Open();

                if (this.checkBox1.Checked)
                {
                    //command
                    String sql = "SELECT * FROM Admin WHERE UserName = @un AND Password=@pw";
                    SqlCommand com = new SqlCommand(sql, conn);
                    com.Parameters.AddWithValue("@un", this.txtName.Text);
                    com.Parameters.AddWithValue("@pw", this.txtPass.Text);

                    //Access data using data reader method
                    SqlDataReader dr = com.ExecuteReader();
                    if (dr.Read() == true)
                    {
                        AdminMenu Am = new AdminMenu();
                        Am.Show();
                        this.Hide();

                    }
                    else
                    {
                        MessageBox.Show("Enter a correct user name and password");
                    }
                }
                else
                {
                    //command
                    String sql = "SELECT * FROM Users WHERE UserName = @un AND Password=@pw";
                    SqlCommand com = new SqlCommand(sql, conn);
                    com.Parameters.AddWithValue("@un", this.txtName.Text);
                    com.Parameters.AddWithValue("@pw", this.txtPass.Text);

                    //Access data using data reader method
                    SqlDataReader dr = com.ExecuteReader();
                    if (dr.Read() == true)
                    {
                        UserMenu Um = new UserMenu();
                        Um.Show();
                        this.Hide();

                    }
                    else
                    {
                        MessageBox.Show("Enter a correct user name and password");
                    }
                }


                //Disconnect
                conn.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong");
            }
            
        }

        private void checkPass_CheckedChanged(object sender, EventArgs e)
        {
            if (txtPass.PasswordChar == (char)0)
            {
                txtPass.PasswordChar = '*';
            }
            else
            {
                txtPass.PasswordChar = (char)0;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void login_Load(object sender, EventArgs e)
        {
           panel1.BackColor = Color.FromArgb(150,0,0,0);
           label1.BackColor = Color.FromArgb(0, 0, 0, 0);
           label2.BackColor = Color.FromArgb(0, 0, 0, 0);
            label3.BackColor = Color.FromArgb(0, 0, 0, 0);
            checkBox1.BackColor = Color.FromArgb(0, 0, 0, 0);
            checkPass.BackColor = Color.FromArgb(0, 0, 0, 0);
            
        }
    }
}
