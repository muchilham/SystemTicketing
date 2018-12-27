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
    public partial class frmClassTicket : Form
    {
        TicketingDBDataContext db = new TicketingDBDataContext();
        frmMain frm;
        tbl_Ticket tick;
        validasi va;
        int klik = 0;
        public frmClassTicket()
        {
            InitializeComponent();
            va = new validasi(this.Controls, this.eValidasi);
        }

        public frmClassTicket(frmMain frm)
        {
            InitializeComponent();
            va = new validasi(this.Controls, this.eValidasi);
            this.frm = frm;
        }

        private void loadGrid()
        {
            dgvClassTicket.DataSource = db.tbl_Tickets.ToList();
        }

        private void setTextBox()
        {
            if (!String.IsNullOrEmpty(tbIdClass.Text))
            {
                tick = db.tbl_Tickets.FirstOrDefault(i => i.id_ticket == int.Parse(tbIdClass.Text));
                if (tick != null)
                {
                    tbClassname.Text = tick.class_ticket;
                    btnAdd.Text = "UPDATE";
                }

                else
                {
                    va.clear("tbIdClass");
                    btnAdd.Text = "ADD";
                }
            }
        }

        private void action(String actioning)
        {
            tick = actioning == "insert" ? new tbl_Ticket() : tick = db.tbl_Tickets.FirstOrDefault(i => i.id_ticket == int.Parse(tbIdClass.Text));
            tick.id_ticket = int.Parse(tbIdClass.Text);
            tick.class_ticket = tbClassname.Text;
            if (actioning == "insert")
            {
                tick.id_ticket = int.Parse(tbIdClass.Text);
                db.tbl_Tickets.InsertOnSubmit(tick);
            }

            db.SubmitChanges();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                String message = "";
                DialogResult fc = MessageBox.Show("Are you sure want to add this data?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (fc == DialogResult.No) return;
                if (va.doValidation() == false) return;
                tick = db.tbl_Tickets.FirstOrDefault(i => i.id_ticket == int.Parse(tbIdClass.Text));
                if (tick != null)
                {
                    message = "Update";
                    action("update");
                }

                else
                {
                    message = "Insert";
                    action("insert");
                }

                MessageBox.Show(message + " data success!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadGrid();
                va.clear("");
            }

            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }
        }

        private void tbIdClass_KeyPress(object sender, KeyPressEventArgs e)
        {
            va.numberFormat(e);
        }

        private void frmClassTicket_Load(object sender, EventArgs e)
        {
            loadGrid();
        }

        private void tbIdClass_TextChanged(object sender, EventArgs e)
        {
            setTextBox();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (klik == 1)
            {
                String idTicket = "";
                DialogResult fc = MessageBox.Show("Are you sure want to delete this data?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (fc == DialogResult.No) return;
                foreach (DataGridViewRow rw in this.dgvClassTicket.SelectedRows)
                {
                    idTicket = rw.Cells[0].Value.ToString();
                }

                var query = db.tbl_Tickets.Where(i => i.id_ticket == int.Parse(idTicket)).Single();
                db.tbl_Tickets.DeleteOnSubmit(query);
                db.SubmitChanges();
                loadGrid();
            }
        }

        private void dgvClassTicket_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            klik = 1;
        }

    }
}
