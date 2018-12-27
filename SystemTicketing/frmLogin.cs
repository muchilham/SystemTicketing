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
    public partial class frmLogin : Form
    {
        TicketingDBDataContext db = new TicketingDBDataContext();
        tbl_Admin adm;
        validasi va;
        public frmLogin()
        {
            InitializeComponent();
            va = new validasi(this.Controls, this.eValidasi);
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            adm = db.tbl_Admins.FirstOrDefault(i => i.username == tbUsername.Text && i.password == tbPassword.Text);
            if (va.doValidation() == false) return;
            if (adm != null)
            {
                tbl_HistoryLogin add = new tbl_HistoryLogin
                {
                    username = tbUsername.Text,
                    login_date = DateTime.Now

                };

                //db.tbl_HistoryLogins.InsertOnSubmit(add);

                try
                {
                    //db.SubmitChanges();
                    frmMain frm = new frmMain();
                    frm.Show();
                    this.Hide();
                }

                catch (Exception err)
                {
                    MessageBox.Show(err.ToString());
                }
                
            }
            else
            {
                MessageBox.Show("Data does not exist in the database", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                va.clear("");
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult fk = MessageBox.Show("Are you sure want to Exit this Program?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (fk == DialogResult.No) return;
            Application.ExitThread();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            tbUsername.Focus();
        }
    }
}
