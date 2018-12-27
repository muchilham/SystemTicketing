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
    public partial class frmTrain : Form
    {
        TicketingDBDataContext db = new TicketingDBDataContext();
        frmMain frm;
        tbl_Train train;
        validasi va;
        int klik = 0;

        public frmTrain()
        {
            InitializeComponent();
            va = new validasi(this.Controls, this.eValidasi);
        }

        public frmTrain(frmMain frm)
        {
            InitializeComponent();
            this.frm = frm;
            va = new validasi(this.Controls, this.eValidasi);
        }

        public void loadGrid()
        {
            dgvTrain.DataSource = db.tbl_Trains.ToList();
        }

        private void setTextbox()
        {
            if (!String.IsNullOrEmpty(tbIdTrain.Text))
            {
                train = db.tbl_Trains.FirstOrDefault(i => i.id_train == int.Parse(tbIdTrain.Text));
                if (train != null)
                {
                    tbTrainname.Text = train.train_name;
                    nudTotalSeat.Value = train.train_seat;
                    btnAdd.Text = "UPDATE";
                }

                else
                {
                    va.clear("tbIdTrain");
                    btnAdd.Text = "ADD";
                }
            }

        }

        private void action(String actioning)
        {
            train = actioning == "insert" ? new tbl_Train() : train = db.tbl_Trains.FirstOrDefault(i => i.id_train == int.Parse(tbIdTrain.Text));
            train.id_train = int.Parse(tbIdTrain.Text);
            train.train_name = tbTrainname.Text;
            train.train_seat = (int)nudTotalSeat.Value;
            if (actioning == "insert")
            {
                train.id_train = int.Parse(tbIdTrain.Text);
                db.tbl_Trains.InsertOnSubmit(train);
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
                train = db.tbl_Trains.FirstOrDefault(i => i.id_train == int.Parse(tbIdTrain.Text));
                if (train != null)
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

        private void frmTrain_Load(object sender, EventArgs e)
        {
            loadGrid();
        }

        private void tbIdTrain_KeyPress(object sender, KeyPressEventArgs e)
        {
            va.numberFormat(e);
        }

        private void tbIdTrain_TextChanged(object sender, EventArgs e)
        {
            setTextbox();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (klik == 1)
            {
                String idTrain = "";
                DialogResult fc = MessageBox.Show("Are you sure want to delete this data?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (fc == DialogResult.No) return;
                foreach (DataGridViewRow rw in this.dgvTrain.SelectedRows)
                {
                    idTrain = rw.Cells[0].Value.ToString();
                }

                var query = db.tbl_Trains.Where(i => i.id_train == int.Parse(idTrain)).Single();
                db.tbl_Trains.DeleteOnSubmit(query);
                db.SubmitChanges();
                loadGrid();
            }
        }

        private void dgvTrain_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            klik = 1;
        }
    }
}
