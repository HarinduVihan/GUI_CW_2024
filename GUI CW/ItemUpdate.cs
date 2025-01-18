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
    public partial class ItemUpdate : Form
    {
        public ItemUpdate()
        {
            InitializeComponent();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                //Create a connection

                string cs = "Data Source = LAPTOP-DOH91PI2;Initial Catalog = GUI_CW; Integrated Security = True ";
                SqlConnection con = new SqlConnection(cs);

                con.Open();

                //Define a command

                string sql = "UPDATE Items SET Name=@name, Price=@price WHERE ItemId = @id";
                SqlCommand com = new SqlCommand(sql, con);

                com.Parameters.AddWithValue("@id", this.txtId.Text);
                com.Parameters.AddWithValue("@name", this.txtName.Text);
                com.Parameters.AddWithValue("@price", this.txtPrice.Text);


                //Execute the command
                com.ExecuteNonQuery();
                MessageBox.Show(" Item Updated Succesfully ", "Information");


                //Disconnect from the sql server
                con.Close();
            }
            catch (SqlException)
            {
                MessageBox.Show(" Something went wrong ", "Information");
            }

            try
            {
                //Create a connection

                string cs = "Data Source = LAPTOP-DOH91PI2;Initial Catalog = GUI_CW; Integrated Security = True ";
                SqlConnection con = new SqlConnection(cs);

                con.Open();

                //Define a command with Sql Statement

                String sql1 = "SELECT * FROM Items";
                SqlCommand com1 = new SqlCommand(sql1, con);

                //Access data using data adapter method
                SqlDataAdapter adp = new SqlDataAdapter(com1);
                DataSet ds;
                ds = new DataSet();
                adp.Fill(ds);

                this.dataGridView1.DataSource = ds.Tables[0];

                //Disconnect from the sql server
                con.Close();
            }
            catch (SqlException)
            {
                MessageBox.Show(" Something went wrong ", "Information");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                //Create a connection

                string cs = "Data Source = LAPTOP-DOH91PI2;Initial Catalog = GUI_CW; Integrated Security = True ";
                SqlConnection con = new SqlConnection(cs);

                con.Open();

                //Define a command

                string sql = "DELETE FROM Items WHERE ItemId = @id";

                SqlCommand com = new SqlCommand(sql, con);

                com.Parameters.AddWithValue("@id", this.txtId.Text);

                //Execute the command
                DialogResult msgret = MessageBox.Show("Are you sure You want to delete this item?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (msgret == DialogResult.Yes)
                {
                    com.ExecuteNonQuery();
                    MessageBox.Show(" Item Deleted Succesfully ", "Information");

                    //Define a command with Sql Statement

                    String sql1 = "SELECT * FROM Items";
                    SqlCommand com1 = new SqlCommand(sql1, con);

                    //Access data using data adapter method
                    SqlDataAdapter adp = new SqlDataAdapter(com1);
                    DataSet ds;
                    ds = new DataSet();
                    adp.Fill(ds);

                    this.dataGridView1.DataSource = ds.Tables[0];

                    //Disconnect from the sql server
                    con.Close();
                }

                //Disconnect from the sql server
                con.Close();
            }
            catch (SqlException)
            {
                MessageBox.Show(" Something went wrong ", "Information");
            }

        }

        private void ItemUpdate_Load(object sender, EventArgs e)
        {
            try
            {
                //Create a connection

                string cs = "Data Source = LAPTOP-DOH91PI2;Initial Catalog = GUI_CW; Integrated Security = True ";
                SqlConnection con = new SqlConnection(cs);

                con.Open();

                //Define a command with Sql Statement

                String sql = "SELECT * FROM Items";
                SqlCommand com = new SqlCommand(sql, con);

                //Access data using data adapter method
                SqlDataAdapter adp = new SqlDataAdapter(com);
                DataSet ds;
                ds = new DataSet();
                adp.Fill(ds);

                this.dataGridView1.DataSource = ds.Tables[0];

                //Disconnect from the sql server
                con.Close();
            }
            catch (SqlException)
            {
                MessageBox.Show(" Something went wrong ", "Information");
            }
        }

        private void txtId_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void btnSearch_Click(object sender, EventArgs e)
        { 
            if (txtId.Text == "")
            {
                MessageBox.Show("Enter an id to search");
            }
            else
            {
                try
                {
                    //Create a connection

                    string cs = "Data Source = LAPTOP-DOH91PI2;Initial Catalog = GUI_CW; Integrated Security = True ";
                    SqlConnection con = new SqlConnection(cs);

                    con.Open();

                    //Define a command with Sql Statement

                    String sql = "SELECT * FROM Items WHERE ItemId=@id";
                    SqlCommand com = new SqlCommand(sql, con);
                    com.Parameters.AddWithValue("@id", this.txtId.Text);

                    //Access data using data Reader method
                    SqlDataReader dr = com.ExecuteReader();


                    if (dr.Read() == true)
                    {
                        //Bind Data with Conrols
                        this.txtName.Text = dr.GetValue(1).ToString();
                        this.txtPrice.Text = dr.GetValue(2).ToString();

                    }
                    else
                    {
                        MessageBox.Show("Item Id Does not exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.txtName.Text = "";
                        this.txtPrice.Text = "";
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            ItemReportViewer itemReportViewer = new ItemReportViewer();
            itemReportViewer.Show();
        }
    }
}
