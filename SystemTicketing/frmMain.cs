using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SystemTicketing
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }


        private void Navigation(Button btn)
        {
            foreach (Control ctl in this.sidebar.Controls)
            {
                if (ctl.Equals(btn))
                {
                    ctl.BackColor = Color.FromArgb(65,70,70);
                }

                else
                {
                    ctl.BackColor = Color.FromArgb(45,45,45);
                }
            }
        }

        private void btnToggle_Click(object sender, EventArgs e)
        {
            if (sidebar.Visible == true && title.Visible == true)
            {
                title.Hide();
                sidebar.Hide();
                label1.Visible = true;
            }
            else
            {
                title.Show();
                sidebar.Show();
                label1.Visible = false;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult fk = MessageBox.Show("Are you sure want to Exit this Program?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (fk == DialogResult.No) return;
            Application.ExitThread();
        }

        public void btnHome_Click(object sender, EventArgs e)
        {
            Navigation(btnHome);
            this.body.Controls.Clear();
            frmHome frm = new frmHome();
            frm.TopLevel = false;
            frm.Dock = DockStyle.Fill;
            frm.Show();
            this.body.Controls.Add(frm);
        }

        public void btnCustomer_Click(object sender, EventArgs e)
        {
            Navigation(btnCustomer);
            this.body.Controls.Clear();
            frmCustomer frm = new frmCustomer();
            frm.TopLevel = false;
            frm.Dock = DockStyle.Fill;
            frm.Show();
            this.body.Controls.Add(frm);
        }

        public void btnTrain_Click(object sender, EventArgs e)
        {
            Navigation(btnTrain);
            this.body.Controls.Clear();
            frmTrain frm = new frmTrain();
            frm.TopLevel = false;
            frm.Dock = DockStyle.Fill;
            frm.Show();
            this.body.Controls.Add(frm);
        }

        public void btnVenue_Click(object sender, EventArgs e)
        {
            Navigation(btnVenue);
            this.body.Controls.Clear();
            frmStation frm = new frmStation();
            frm.TopLevel = false;
            frm.Dock = DockStyle.Fill;
            frm.Show();
            this.body.Controls.Add(frm);
        }

        public void btnClassTicket_Click(object sender, EventArgs e)
        {
            Navigation(btnClassTicket);
            this.body.Controls.Clear();
            frmClassTicket frm = new frmClassTicket();
            frm.TopLevel = false;
            frm.Dock = DockStyle.Fill;
            frm.Show();
            this.body.Controls.Add(frm);
        }

        public void btnTypeTicket_Click(object sender, EventArgs e)
        {
            Navigation(btnTypeTicket);
            this.body.Controls.Clear();
            frmTypeTicket frm = new frmTypeTicket();
            frm.TopLevel = false;
            frm.Dock = DockStyle.Fill;
            frm.Show();
            this.body.Controls.Add(frm);
        }

        public void btnSchedule_Click(object sender, EventArgs e)
        {
            Navigation(btnSchedule);
            this.body.Controls.Clear();
            frmSchedule frm = new frmSchedule();
            frm.TopLevel = false;
            frm.Dock = DockStyle.Fill;
            frm.Show();
            this.body.Controls.Add(frm);
        }

        public void btnBooking_Click(object sender, EventArgs e)
        {
            Navigation(btnBooking);
            this.body.Controls.Clear();
            frmBooking frm = new frmBooking();
            frm.TopLevel = false;
            frm.Dock = DockStyle.Fill;
            frm.Show();
            this.body.Controls.Add(frm);
        }

        public void btnPayment_Click(object sender, EventArgs e)
        {
            Navigation(btnPayment);
            this.body.Controls.Clear();
            frmPayment frm = new frmPayment();
            frm.TopLevel = false;
            frm.Dock = DockStyle.Fill;
            frm.Show();
            this.body.Controls.Add(frm);
        }

        public void frmMain_Load(object sender, EventArgs e)
        {
            btnHome_Click(sender, e);
        }

        private void btnRoute_Click(object sender, EventArgs e)
        {
            Navigation(btnRoute);
            this.body.Controls.Clear();
            frmRoute frm = new frmRoute();
            frm.TopLevel = false;
            frm.Dock = DockStyle.Fill;
            frm.Show();
            this.body.Controls.Add(frm);
        }


    }
}
