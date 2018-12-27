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
    public partial class frmPayment : Form
    {
        TicketingDBDataContext db = new TicketingDBDataContext();
        frmMain frm;
        tbl_BookingMaster bok;
        validasi va;
        int klik = 0;
        String idBooking;
        public frmPayment()
        {
            InitializeComponent();
            va = new validasi(this.Controls, this.eValidasi);
        }

        public frmPayment(frmMain frm)
        {
            InitializeComponent();
            va = new validasi(this.Controls, this.eValidasi);
            this.frm = frm;
        }

        public void loadGrid()
        {
            var query = from s in db.tbl_Schedules
                        join b in db.tbl_BookingMasters
                        on s.id_schedule equals b.id_schedule

                        join bd in db.tbl_BookingDetails
                        on b.id_booking equals bd.id_booking

                        join c in db.tbl_Customers
                        on bd.id_customer equals c.id_customer

                        join y in db.tbl_Routes
                        on s.id_route equals y.id_route

                        join st in db.tbl_Stations
                        on y.id_departure equals st.id_station

                        join st2 in db.tbl_Stations
                        on y.id_arrival equals st2.id_station

                        join t in db.tbl_Tickets
                        on s.id_ticket equals t.id_ticket

                        join se in db.tbl_Trains
                        on s.id_train equals se.id_train
                        where b.status == "NOT PAID"
                        group bd by bd.id_booking into x
                        select new
                        {
                            IDBooking = x.Key,
                            Schedule = x.First().tbl_BookingMaster.id_schedule,
                            Fullname = x.First().tbl_Customer.firstname + " " + x.First().tbl_Customer.lastname,
                            Date = x.First().tbl_BookingMaster.booking_date,
                            Totalticket = x.First().tbl_BookingMaster.total_ticket,
                            Status = x.First().tbl_BookingMaster.status

                                            
                        };
            dgvBooking.DataSource = query.ToList();
        }

        private void booking()
        {
            if (klik == 1)
            {
                DialogResult fk = MessageBox.Show("Are you sure want select this?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (fk == DialogResult.No) return;

                foreach (DataGridViewRow rw in this.dgvBooking.SelectedRows)
                {
                    idBooking = rw.Cells[0].Value.ToString();
                }
                frmPaymentDetail frm = new frmPaymentDetail(idBooking);
                frm.ShowDialog();
            }
        }

        public void btnSearch_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(tbIdBooking.Text))
            {
                var query = from s in db.tbl_Schedules
                        join b in db.tbl_BookingMasters
                        on s.id_schedule equals b.id_schedule

                        join bd in db.tbl_BookingDetails
                        on b.id_booking equals bd.id_booking

                        join c in db.tbl_Customers
                        on bd.id_customer equals c.id_customer

                        join y in db.tbl_Routes
                        on s.id_route equals y.id_route

                        join st in db.tbl_Stations
                        on y.id_departure equals st.id_station

                        join st2 in db.tbl_Stations
                        on y.id_arrival equals st2.id_station

                        join t in db.tbl_Tickets
                        on s.id_ticket equals t.id_ticket

                        join se in db.tbl_Trains
                        on s.id_train equals se.id_train
                        where b.id_booking == int.Parse(tbIdBooking.Text)
                        select new
                        {
                            IDBooking = b.id_booking,
                            IDCustomer = c.id_customer,
                            Fullname = c.firstname + " " + c.lastname,
                            Departure = st.station_name,
                            Arrival = st2.station_name,
                            Date = b.booking_date
                        };
                dgvBooking.DataSource = query.ToList();
            }

            else
            {
                loadGrid();
            }
        }

        private void dgvBooking_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            klik = 1;
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            booking();
        }

        private void dgvBooking_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            klik = 1;
            booking();
        }
    }
}
