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
using CrystalDecisions.CrystalReports.Engine;

namespace GUI_CW
{
    public partial class InvoiceViewer : Form
    {
      
        ReportDocument cryrpt = new ReportDocument();
        SqlConnection con = new SqlConnection("Data Source = LAPTOP-DOH91PI2;Initial Catalog = GUI_CW; Integrated Security = True ");


        public InvoiceViewer()
        {
            InitializeComponent();
        }

        private void InvoiceViewer_Load(object sender, EventArgs e)
        {
            crystalReportViewer1.RefreshReport();
        
        }
    }
}
