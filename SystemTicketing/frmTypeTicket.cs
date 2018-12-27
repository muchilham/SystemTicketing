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
    public partial class frmTypeTicket : Form
    {
        TicketingDBDataContext db = new TicketingDBDataContext();
        frmMain frm;
        tbl_TicketType tType;
        validasi va;
        int klik = 0;
        public frmTypeTicket()
        {
            InitializeComponent();
            va = new validasi(this.Controls, this.eValidasi);
        }
        public frmTypeTicket(frmMain frm)
        {
            InitializeComponent();
            va = new validasi(this.Controls, this.eValidasi);
            this.frm = frm;
        }

        private void loadGrid()
        {

            var q = from w in db.tbl_TicketTypes
                    join r in db.tbl_Tickets
                    on w.id_ticket equals r.id_ticket

                    orderby r.class_ticket

                    select new
                    {
                        w.id_type,
                        r.class_ticket,
                        w.harga_ticket
                    };
            var y =from t in db.tbl_Tickets select new {id = t.id_ticket, name = t.class_ticket};
            dgvTypeTicket.DataSource = q.ToList();
            cmbIdClass.DataSource = y.ToList();
            cmbIdClass.ValueMember = "id";
            cmbIdClass.DisplayMember = "name";
        }

        private void action()
        {
            tType = new tbl_TicketType
            {
                id_ticket = int.Parse(cmbIdClass.SelectedValue.ToString()),
                harga_ticket = float.Parse(tbHargaClass.Text)
            };
            db.tbl_TicketTypes.InsertOnSubmit(tType);
            db.SubmitChanges();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                String message = "Insert";
                DialogResult fc = MessageBox.Show("Are you sure want to add this data?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (fc == DialogResult.No) return;
                if (va.doValidation() == false) return;
                action();
                MessageBox.Show(message + " data success!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadGrid();
                va.clear("");
            }

            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }
        }

        private void frmTypeTicket_Load(object sender, EventArgs e)
        {
            loadGrid();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (klik == 1)
            {
                String idTicketT = "";
                DialogResult fc = MessageBox.Show("Are you sure want to delete this data?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (fc == DialogResult.No) return;
                foreach (DataGridViewRow rw in this.dgvTypeTicket.SelectedRows)
                {
                    idTicketT = rw.Cells[0].Value.ToString();
                }

                var query = db.tbl_TicketTypes.Where(i => i.id_type == int.Parse(idTicketT)).Single();
                db.tbl_TicketTypes.DeleteOnSubmit(query);
                db.SubmitChanges();
                loadGrid();
            }
        }

        private void dgvTypeTicket_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            klik = 1;
        }
    }
}
