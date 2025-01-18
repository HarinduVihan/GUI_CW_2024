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
    public partial class UsersAdd : Form
    {
        public UsersAdd()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "")
            {
                MessageBox.Show("Enter a name");
            }
            else if (txtAdd.Text == "")
            {
                MessageBox.Show("Enter a password");
            }
            else
            {
                try
                {
                    //Create a connection

                    string cs = "Data Source = LAPTOP-DOH91PI2;Initial Catalog = GUI_CW; Integrated Security = True ";
                    SqlConnection con = new SqlConnection(cs);

                    con.Open();

                    //Define a command

                    string sql = "INSERT INTO Users VALUES (@name,@add)";

                    SqlCommand com = new SqlCommand(sql, con);

                    com.Parameters.AddWithValue("@name", this.txtName.Text);
                    com.Parameters.AddWithValue("@add", this.txtAdd.Text);


                    //Execute the command
                    com.ExecuteNonQuery();
                    MessageBox.Show(" User Added Succesfully ", "Information");

                   
                    this.txtName.Text = "";
                    this.txtAdd.Text = "";

                    //Disconnect from the sql server
                    con.Close();
                }
                catch (SqlException)
                {
                    MessageBox.Show(" Something went wrong ", "Information");
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
