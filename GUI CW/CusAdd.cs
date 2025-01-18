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
    public partial class CusAdd : Form
    {
        public CusAdd()
        {
            InitializeComponent();
        }

        private void IdAutoIncremant()
        {
            try
            {
                //Create a connection

                string cs = "Data Source = LAPTOP-DOH91PI2;Initial Catalog = GUI_CW; Integrated Security = True ";
                SqlConnection con = new SqlConnection(cs);

                con.Open();

                string sql1 = "SELECT MAX(CusId) FROM Customers";
                SqlCommand cmd = new SqlCommand(sql1, con);

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    // Check if the value is convertible to int
                    if (int.TryParse(dr[0].ToString(), out int maxID))
                    {
                        int newID = maxID + 1;
                        this.txtId.Text = newID.ToString();
                    }
                    else
                    {
                        // Handle the case where the value is not a valid integer
                        this.txtId.Text = "1";
                    }
                }
                else
                {
                    // Handle the case where there are no records in the table
                    this.txtId.Text = "1";
                }

                //Disconnect from the sql server
                con.Close();

            }
            catch (SqlException)
            {
                MessageBox.Show(" Something went wrong ", "Information");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "")
            {
                MessageBox.Show("Enter a name");
            }
            else if (txtNo.Text == "")
            {
                MessageBox.Show("Enter a Contact number");
            }
            else if (txtAdd.Text == "")
            {
                MessageBox.Show("Enter an address");
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

                    string sql = "INSERT INTO Customers (CusId,Name,Contact,Address) VALUES (@id,@name,@contact,@add)";

                    SqlCommand com = new SqlCommand(sql, con);

                    com.Parameters.AddWithValue("@id", this.txtId.Text);
                    com.Parameters.AddWithValue("@name", this.txtName.Text);
                    com.Parameters.AddWithValue("@contact", this.txtNo.Text);
                    com.Parameters.AddWithValue("@add", this.txtAdd.Text);


                    //Execute the command
                    com.ExecuteNonQuery();
                    MessageBox.Show(" Customer Added Succesfully ", "Information");

                    IdAutoIncremant();

                    this.txtName.Text = "";
                    this.txtNo.Text = "";
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

        private void CusAdd_Load(object sender, EventArgs e)
        {
            IdAutoIncremant();

          /*  label1.BackColor = Color.FromArgb(0, 0, 0, 0);
            label2.BackColor = Color.FromArgb(0, 0, 0, 0);
            label3.BackColor = Color.FromArgb(0, 0, 0, 0);
            label4.BackColor = Color.FromArgb(0, 0, 0, 0);*/
        }

        private void txtNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            //prevent letters entering

            if (!char.IsControl(e.KeyChar) && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
