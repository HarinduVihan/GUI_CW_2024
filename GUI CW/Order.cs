using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Windows.Forms;
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
using System.Xml.Linq;
using CrystalDecisions.Shared;
using System.Security.Cryptography;

namespace GUI_CW
{
    public partial class Order : Form
    {
        public Order()
        {
            InitializeComponent();
        }

        int subId = 0;
        double total = 0;



        public void OrderIdIncrement()
        {
            try
            {
                //Create a connection

                string cs = "Data Source = LAPTOP-DOH91PI2;Initial Catalog = GUI_CW; Integrated Security = True ";
                SqlConnection con = new SqlConnection(cs);

                con.Open();

                string sql1 = "SELECT MAX(OrderID) FROM [Order]";
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

        private void getValuesTotxtCus()
        {
            //loading values to the combo box txtCus
            try
            {
                //Create a connection

                string cs = "Data Source = LAPTOP-DOH91PI2;Initial Catalog = GUI_CW; Integrated Security = True ";
                SqlConnection con = new SqlConnection(cs);

                con.Open();

                //Define a command

                String sql = "SELECT Name FROM Customers";

                SqlCommand cmd = new SqlCommand(sql, con);


                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    txtCus.Items.Add(dr["Name"].ToString());

                }
                dr.Close();
                con.Close();
            }
            catch
            {
                MessageBox.Show(" Something went wrong ", "Information");
            }
        }

        private void getValuesTotxtItem()
        {
            //loading values to the combo box txtCus
            try
            {
                //Create a connection

                string cs = "Data Source = LAPTOP-DOH91PI2;Initial Catalog = GUI_CW; Integrated Security = True ";
                SqlConnection con = new SqlConnection(cs);

                con.Open();

                //Define a command

                String sql = "SELECT Name FROM Items";

                SqlCommand cmd = new SqlCommand(sql, con);


                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    txtItem.Items.Add(dr["Name"].ToString());

                }
                dr.Close();
                con.Close();
            }
            catch
            {
                MessageBox.Show(" Something went wrong ", "Information");
            }
        }

        public void truncateTable()
        {

            try
            {
                //Create a connection

                string cs = "Data Source = LAPTOP-DOH91PI2;Initial Catalog = GUI_CW; Integrated Security = True ";
                SqlConnection con = new SqlConnection(cs);

                con.Open();

                String sql = "TRUNCATE TABLE SubOrder";
                SqlCommand com = new SqlCommand(sql, con);
                com.ExecuteNonQuery();

                con.Close();
            }
            catch
            {
                MessageBox.Show(" Something went wrong ", "Information");
            }
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtPrice.Text = "";
            this.txtQnt.Text = "1";
            this.txtSubTotal.Text = "";

            try
            {
                //Create a connection

                string cs = "Data Source = LAPTOP-DOH91PI2;Initial Catalog = GUI_CW; Integrated Security = True ";
                SqlConnection con = new SqlConnection(cs);

                con.Open();

                //Define a command

                String sql = "SELECT Price FROM Items WHERE Name = @name ";

                SqlCommand cmd = new SqlCommand(sql, con);

                cmd.Parameters.AddWithValue("@name", this.txtItem.Text);


                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();

                this.txtPrice.Text = dr.GetValue(0).ToString();

                dr.Close();
                con.Close();
            }
            catch
            {
                MessageBox.Show(" Something went wrong ", "Information");
            }
        }

        private void Order_Load(object sender, EventArgs e)
        {
            OrderIdIncrement();
            getValuesTotxtCus();
            getValuesTotxtItem();
            truncateTable();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double subTotal = 0;
            double price = 0;
            int qnt = 0;

            if (txtCus.Text == "")
            {
                MessageBox.Show("Select a Customer");
            }
            else if (txtItem.Text == "")
            {
                MessageBox.Show("Select an Item");
            }
            else if (txtQnt.Text == "")
            {
                MessageBox.Show("Select a quantitiy");
            }
            else
            {
                subId++;

                price = Convert.ToDouble(this.txtPrice.Text);
                qnt = Convert.ToInt32(this.txtQnt.Text);

                subTotal = price * qnt;

                this.txtSubTotal.Text = subTotal.ToString();

                try
                {
                    //Create a connection

                    string cs = "Data Source = LAPTOP-DOH91PI2;Initial Catalog = GUI_CW; Integrated Security = True ";
                    SqlConnection con = new SqlConnection(cs);

                    con.Open();

                    //Define a command

                    string sql = "INSERT INTO SubOrder (SubOrderId,Item,Quantity,SubTotal,OrderId) VALUES (@id,@item,@qnt,@sub,@oid)";

                    SqlCommand com = new SqlCommand(sql, con);

                    com.Parameters.AddWithValue("@id", subId);
                    com.Parameters.AddWithValue("@item", this.txtItem.Text);
                    com.Parameters.AddWithValue("@qnt", this.txtQnt.Text);
                    com.Parameters.AddWithValue("@sub", subTotal);
                    com.Parameters.AddWithValue("@oid", this.txtId.Text);


                    //Execute the command
                    int ret = com.ExecuteNonQuery();
                    //MessageBox.Show(ret + " Item Added Succesfully ", "Information");

                    // Create a command

                    string sql2 = "SELECT * FROM SubOrder WHERE OrderId = @oid";
                    SqlCommand com2 = new SqlCommand(sql2, con);

                    com2.Parameters.AddWithValue("@oid", this.txtId.Text);


                    //Execute command

                    SqlDataAdapter dap = new SqlDataAdapter(com2);
                    DataSet dataSet = new DataSet();
                    dap.Fill(dataSet);

                    this.dataGridView1.DataSource = dataSet.Tables[0];

                    total += subTotal;

                    this.txtTotal.Text = total.ToString();

                    //Disconnect from the sql server
                    con.Close();

                    btnSave.Enabled = true;
                    btnPrint.Enabled = true;
                   


                }
                catch (SqlException)
                {
                    MessageBox.Show(" Something went wrong ", "Information");
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtItem.Text = "";
            this.txtPrice.Text = "";
            this.txtQnt.Text = "1";
            this.txtTotal.Text = "";
            this.txtSubTotal.Text = "";
            this.dataGridView1.Columns.Clear();

            subId = 0;

            OrderIdIncrement();
            truncateTable();

        }

        private void txtQnt_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtQnt_VisibleChanged(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
    
            try
            {
                //Create a connection

                string cs = "Data Source = LAPTOP-DOH91PI2;Initial Catalog = GUI_CW; Integrated Security = True ";
                SqlConnection con = new SqlConnection(cs);

                con.Open();

                //Define a command

                string sql = "INSERT INTO [Order] (OrderID,Customer,Total) VALUES (@id,@cus,@total)";

                SqlCommand com = new SqlCommand(sql, con);

                com.Parameters.AddWithValue("@id", this.txtId.Text);
                com.Parameters.AddWithValue("@cus", this.txtCus.Text);
                com.Parameters.AddWithValue("@total", this.txtTotal.Text);


                //Execute the command
                com.ExecuteNonQuery();
                MessageBox.Show(" Order Saved Succesfully ", "Information");

            }
            catch (SqlException)
            {
                MessageBox.Show(" Something went wrong ", "Information");
            }
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            truncateTable();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            InvoiceViewer invoice = new InvoiceViewer();

            Invoice cr = new Invoice();

            TextObject Textname = (TextObject)cr.ReportDefinition.Sections["Section1"].ReportObjects["Text14"];
            TextObject Textoid = (TextObject)cr.ReportDefinition.Sections["Section2"].ReportObjects["Text7"];
            TextObject Texttotal = (TextObject)cr.ReportDefinition.Sections["Section4"].ReportObjects["Text12"];

            Textname.Text = txtCus.Text;
            Textoid.Text = txtId.Text;
            Texttotal.Text = txtTotal.Text;

            invoice.crystalReportViewer1.ReportSource = cr;

            invoice.Show();
        }
    }
}
