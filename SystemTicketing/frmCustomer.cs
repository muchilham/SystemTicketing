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
    public partial class frmCustomer : Form
    {
        TicketingDBDataContext db = new TicketingDBDataContext();
        frmMain frm;
        tbl_Customer cust = new tbl_Customer();
        validasi va;
        int klik = 0;
        public frmCustomer()
        {
            InitializeComponent();
            va = new validasi(this.Controls, this.eValidasi);
        }

        public frmCustomer(frmMain frm)
        {
            InitializeComponent();
            va = new validasi(this.Controls, this.eValidasi);
            this.frm = frm;
        }

        private void loadGrid()
        {
            dgvCustomer.DataSource = db.tbl_Customers.ToList();
        }
        public void setTextbox()
        {
            if (!String.IsNullOrEmpty(tbIdCustomer.Text))
            {
                cust = db.tbl_Customers.FirstOrDefault(i => i.id_customer == int.Parse(tbIdCustomer.Text));
                if (cust != null)
                {
                    tbIdCustomer.Text = cust.id_customer.ToString();
                    tbFirstname.Text = cust.firstname;
                    tbLastname.Text = cust.lastname;
                    rtbAddress.Text = cust.address;
                    tbTown.Text = cust.town;
                    tbCountry.Text = cust.country;
                    tbPostCode.Text = cust.postcode;
                    tbEmail.Text = cust.email;
                    btnAdd.Text = "UPDATE";
                }

                else
                {
                    va.clear("tbIdCustomer");
                    btnAdd.Text = "ADD";
                }
            }
        }

        private void action(String actioning)
        {
            cust = actioning == "insert" ? new tbl_Customer() : cust = db.tbl_Customers.FirstOrDefault(i => i.id_customer == int.Parse(tbIdCustomer.Text));
            cust.id_customer = int.Parse(tbIdCustomer.Text);
            cust.firstname = tbFirstname.Text;
            cust.lastname = tbLastname.Text;
            cust.address = rtbAddress.Text;
            cust.town = tbTown.Text;
            cust.country = tbCountry.Text;
            cust.postcode = tbPostCode.Text;
            cust.email = tbEmail.Text;
            if (actioning == "insert")
            {
                cust.id_customer = int.Parse(tbIdCustomer.Text);
                db.tbl_Customers.InsertOnSubmit(cust);
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
                cust = db.tbl_Customers.FirstOrDefault(i => i.id_customer == int.Parse(tbIdCustomer.Text));
                if (cust != null)
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

        private void frmCustomer_Load(object sender, EventArgs e)
        {
            loadGrid();
        }

        private void tbIdCustomer_KeyPress(object sender, KeyPressEventArgs e)
        {
            va.numberFormat(e);
        }

        private void tbIdCustomer_TextChanged(object sender, EventArgs e)
        {
            setTextbox();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (klik == 1)
            {
                String idCustomer = "";
                DialogResult fc = MessageBox.Show("Are you sure want to delete this data?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (fc == DialogResult.No) return;
                foreach (DataGridViewRow rw in this.dgvCustomer.SelectedRows)
                {
                    idCustomer = rw.Cells[0].Value.ToString();
                }

                var query = db.tbl_Customers.Where(i => i.id_customer == int.Parse(idCustomer)).Single();
                db.tbl_Customers.DeleteOnSubmit(query);
                db.SubmitChanges();
                loadGrid();
            }
        }

        private void dgvCustomer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            klik = 1;
        }

        private void tbPostCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            va.numberFormat(e);
        }
    }
}
