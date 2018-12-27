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
    public partial class frmSchedule : Form
    {
        TicketingDBDataContext db = new TicketingDBDataContext();
        frmMain frm;
        tbl_Schedule sch;
        validasi va;
        int klik = 0;
        string depart, arriv;
        public frmSchedule()
        {
            InitializeComponent();
            va = new validasi(this.Controls, this.eValidasi);
        }

        public frmSchedule(frmMain frm)
        {
            InitializeComponent();
            va = new validasi(this.Controls, this.eValidasi);
            this.frm = frm;
        }

        public void loadGrid()
        {
            var query = from j in db.tbl_Stations
                        join p in db.tbl_Routes
                        on j.id_station equals p.id_departure

                        join g in db.tbl_Stations
                        on p.id_arrival equals g.id_station

                        join d in db.tbl_Schedules
                        on p.id_route equals d.id_route

                        join w in db.tbl_Trains
                        on d.id_train equals w.id_train

                        join c in db.tbl_Tickets
                        on d.id_ticket equals c.id_ticket
                        select new
                        {
                            IDSchedule = d.id_schedule,
                            Train = w.train_name,
                            Departure = j.station_name,
                            Arrival = g.station_name,
                            Class = c.class_ticket,
                            Harga = d.harga,
                            Date = d.departure_date
                        };
            dgvSchedule.DataSource = query.ToList();

            //dgvSchedule.DataSource = db.tbl_Schedules.ToList();
            cmbIdTrain.DataSource = db.tbl_Trains.Select(i => i.id_train).ToList();
            cmbIdRoute.DataSource = db.tbl_Routes.Select(i => i.id_route).ToList();

            var b = from q in db.tbl_Tickets select q;
            cmbIdTicket.DataSource = b.ToList();
            cmbIdTicket.ValueMember = "id_ticket";
            cmbIdTicket.DisplayMember = "class_ticket";
            dtpDate.MinDate = DateTime.Now;
        }

        private void action()
        {
            sch = new tbl_Schedule
            {
                id_train = int.Parse(cmbIdTrain.Text),
                id_route = int.Parse(cmbIdRoute.Text),
                id_ticket = int.Parse(cmbIdTicket.SelectedValue.ToString()),
                harga = float.Parse(cmbIdType.Text),
                departure_date = DateTime.Parse(dtpDate.Text)
            };
            db.tbl_Schedules.InsertOnSubmit(sch);
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

        private void frmSchedule_Load(object sender, EventArgs e)
        {
            loadGrid();
        }

        private void cmbIdTrain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(cmbIdTrain.Text))
            {
                tbl_Train train = db.tbl_Trains.FirstOrDefault(i => i.id_train == int.Parse(cmbIdTrain.Text));
                tbTrainname.Text = train.train_name;
            }
        }

        private void cmbIdTicket_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(cmbIdTicket.Text))
            {
                var q = from w in db.tbl_TicketTypes
                        join r in db.tbl_Tickets
                        on w.id_ticket equals r.id_ticket
                        where r.id_ticket == int.Parse(cmbIdTicket.SelectedValue.ToString())
                        select w.harga_ticket;
                cmbIdType.Enabled = true;
                cmbIdType.DataSource = q;
            }

            else
            {
                cmbIdType.Enabled = false;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (klik == 1)
            {
                String idSchedule = "";
                DialogResult fc = MessageBox.Show("Are you sure want to delete this data?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (fc == DialogResult.No) return;
                foreach (DataGridViewRow rw in this.dgvSchedule.SelectedRows)
                {
                    idSchedule = rw.Cells[0].Value.ToString();
                }

                var query = db.tbl_Schedules.Where(i => i.id_schedule == int.Parse(idSchedule)).Single();
                db.tbl_Schedules.DeleteOnSubmit(query);
                db.SubmitChanges();
                loadGrid();
            }
        }

        private void dgvSchedule_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            klik = 1;
        }

        private void cmbIdRoute_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(cmbIdRoute.Text))
            {
                var t = from j in db.tbl_Stations
                        join p in db.tbl_Routes
                        on j.id_station equals p.id_departure

                        join g in db.tbl_Stations
                        on p.id_arrival equals g.id_station
                        where p.id_route == int.Parse(cmbIdRoute.Text)
                        select new
                        {
                            departure = j.station_name,
                            arrival = g.station_name
                        };
                foreach (var o in t)
                {
                    depart = o.departure;
                    arriv = o.arrival;

                }
                tbDeparture.Text = depart;
                tbArrival.Text = arriv;
            }
        }
    }
}
