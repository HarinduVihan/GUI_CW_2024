using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI_CW
{
    public partial class AdminMenu : Form
    {
        public AdminMenu()
        {
            InitializeComponent();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CusAdd cusAdd = new CusAdd();
            cusAdd.Show();
        }

        private void updateDeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CusUpdate cusUpdate = new CusUpdate();
            cusUpdate.Show();
        }

        private void addToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ItemAdd add = new ItemAdd();
            add.Show();
        }

        private void updateDeleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ItemUpdate update = new ItemUpdate();
            update.Show();
        }

        private void orderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            login Login = new login();
            Login.Show();
            
        }

        private void addUsersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UsersAdd usersAdd = new UsersAdd();
            usersAdd.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void placeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Order order = new Order();
            order.Show();
        }

        private void reportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OderReportViewer Orv = new OderReportViewer();
            Orv.Show();
        }
    }
}
