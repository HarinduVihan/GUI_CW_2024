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
    public partial class ItemAdd : Form
    {
        public ItemAdd()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "")
            {
                MessageBox.Show("Enter a name");
            }
            else if (txtPrice.Text == "")
            {
                MessageBox.Show("Enter a price");
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

                    string sql = "INSERT INTO Items (ItemId,Name,Price) VALUES (@id,@name,@price)";

                    SqlCommand com = new SqlCommand(sql, con);

                    com.Parameters.AddWithValue("@id", this.txtId.Text);
                    com.Parameters.AddWithValue("@name", this.txtName.Text);
                    com.Parameters.AddWithValue("@price", this.txtPrice.Text);


                    //Execute the command
                    com.ExecuteNonQuery();
                    MessageBox.Show(" Item Added Succesfully ", "Information");




                    try
                    {

                        string sql1 = "SELECT MAX(ItemId) FROM Items";
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

                        this.txtName.Text = "";
                        this.txtPrice.Text = "";
                    }
                    catch (SqlException)
                    {
                        MessageBox.Show(" Something went wrong ", "Information");
                    }

                    //Disconnect from the sql server
                    con.Close();
                }
                catch (SqlException)
                {
                    MessageBox.Show(" Something went wrong ", "Information");
                }
            }

        }

        private void ItemAdd_Load(object sender, EventArgs e)
        {
            try
            {
                string cs = "Data Source = LAPTOP-DOH91PI2;Initial Catalog = GUI_CW; Integrated Security = True ";
                SqlConnection con = new SqlConnection(cs);

                con.Open();

                string sql = "SELECT MAX(ItemId) FROM Items";
                SqlCommand cmd = new SqlCommand(sql, con);

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
                con.Close();
            }
            catch (SqlException)
            {
                MessageBox.Show(" Something went wrong ", "Information");
            }


        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
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
