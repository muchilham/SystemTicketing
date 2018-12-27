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
    public partial class frmStation : Form
    {
        TicketingDBDataContext db = new TicketingDBDataContext();
        frmMain frm;
        tbl_Station stat;
        validasi va;
        int klik = 0;
        public frmStation()
        {
            InitializeComponent();
            va = new validasi(this.Controls, this.eValidasi);
        }

        public frmStation(frmMain frm)
        {
            InitializeComponent();
            va = new validasi(this.Controls, this.eValidasi);
            this.frm = frm;
        }

        private void loadGrid()
        {
            dgvStation.DataSource = db.tbl_Stations.ToList();
        }

        private void setTextBox()
        {
            if(!String.IsNullOrEmpty(tbIdStation.Text))
            {
                stat = db.tbl_Stations.FirstOrDefault(i => i.id_station == int.Parse(tbIdStation.Text));
                if (stat != null)
                {
                    tbStationname.Text = stat.station_name;
                    tbStationlocation.Text = stat.station_location;
                    btnAdd.Text = "UPDATE";
                }

                else
                {
                    va.clear("tbIdStation");
                    btnAdd.Text = "ADD";
                }
            }
        }

        private void action(String actioning)
        {
            stat = actioning == "insert" ? new tbl_Station() : stat = db.tbl_Stations.FirstOrDefault(i => i.id_station == int.Parse(tbIdStation.Text));
            stat.id_station = int.Parse(tbIdStation.Text);
            stat.station_name = tbStationname.Text;
            stat.station_location = tbStationlocation.Text;
            if (actioning == "insert")
            {
                stat.id_station = int.Parse(tbIdStation.Text);
                db.tbl_Stations.InsertOnSubmit(stat);
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
                stat = db.tbl_Stations.FirstOrDefault(i => i.id_station == int.Parse(tbIdStation.Text));
                if (stat != null)
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

        private void tbIdVenue_KeyPress(object sender, KeyPressEventArgs e)
        {
            va.numberFormat(e);
        }

        private void frmVenue_Load(object sender, EventArgs e)
        {
            loadGrid();
        }

        private void tbIdVenue_TextChanged(object sender, EventArgs e)
        {
            setTextBox();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (klik == 1)
            {
                String idStation = "";
                DialogResult fc = MessageBox.Show("Are you sure want to delete this data?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (fc == DialogResult.No) return;
                foreach (DataGridViewRow rw in this.dgvStation.SelectedRows)
                {
                    idStation = rw.Cells[0].Value.ToString();
                }

                var query = db.tbl_Stations.Where(i => i.id_station == int.Parse(idStation)).Single();
                db.tbl_Stations.DeleteOnSubmit(query);
                db.SubmitChanges();
                loadGrid();
            }
        }

        private void dgvVenue_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            klik = 1;
        }
    }
}
