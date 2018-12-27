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
    public partial class frmBooking : Form
    {
        TicketingDBDataContext db = new TicketingDBDataContext();
        frmMain frm;
        validasi va;
        int klik = 0, total;
        String idSchedule;
        public frmBooking()
        {
            InitializeComponent();
            va = new validasi(this.Controls, this.eValidasi);
        }

        public frmBooking(frmMain frm)
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
                            Date = d.departure_date,
                            Stock = w.train_seat - db.tbl_BookingMasters.Sum(i => i.total_ticket)
                        };
            dgvSchedule.DataSource = query.ToList();
        }

        private void booking()
        {
            if (klik == 1)
            {
                DialogResult fk = MessageBox.Show("Are you sure want select this?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (fk == DialogResult.No) return;

                foreach (DataGridViewRow rw in this.dgvSchedule.SelectedRows)
                {
                    idSchedule = rw.Cells[0].Value.ToString();
                    total = int.Parse(rw.Cells[7].Value.ToString());
                }

                var q = from s in db.tbl_Schedules
                        join t in db.tbl_Trains
                        on s.id_train equals t.id_train

                        where s.id_schedule == int.Parse(idSchedule)
                        select new { total = t.train_seat };

                var n = from b in db.tbl_BookingMasters
                        join s in db.tbl_Schedules
                        on b.id_schedule equals s.id_schedule

                        where s.id_schedule == int.Parse(idSchedule)
                        select b.total_ticket;

                if (total >= 1)
                {
                    frmBookingDetail frm = new frmBookingDetail(idSchedule, total);
                    frm.ShowDialog();
                }

                else
                {
                    DialogResult dr = MessageBox.Show("KERETA SUDAH PENUH", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Question);
                    if (dr == DialogResult.No) return;
                }
            }
        }

        private void btnBooking_Click(object sender, EventArgs e)
        {
            booking();
        }

        private void frmBooking_Load(object sender, EventArgs e)
        {
            loadGrid();
        }

        private void dgvSchedule_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            klik = 1;
        }

        private void dgvSchedule_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            klik = 1;
            booking();
        }
    }
}
