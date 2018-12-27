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
    public partial class frmRoute : Form
    {
        TicketingDBDataContext db = new TicketingDBDataContext();
        frmMain frm;
        tbl_Route route;
        validasi va;
        public frmRoute()
        {
            InitializeComponent();
            va = new validasi(this.Controls, this.eValidasi);
        }

        public frmRoute(frmMain frm)
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

                        select new
                        {
                            IDRoute = p.id_route,
                            Departure = j.station_name,
                            Arrival = g.station_name,
                        };
            dgvRoute.DataSource = query.ToList();

            var combobox = from s in db.tbl_Stations select s.id_station;
            cmbIdDepart.DataSource = combobox.ToList();
            cmbIdArrival.DataSource = combobox.ToList();
        }
        public void action()
        {
            route = new tbl_Route
            {
                id_departure = int.Parse(cmbIdDepart.Text),
                id_arrival = int.Parse(cmbIdArrival.Text)
            };

            db.tbl_Routes.InsertOnSubmit(route);
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
                if (cmbIdDepart.Text == cmbIdArrival.Text)
                {
                    MessageBox.Show("You cant choose same station with departure station", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
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

        private void frmRoute_Load(object sender, EventArgs e)
        {
            loadGrid();
        }

        private void cmbIdDepart_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(cmbIdDepart.Text))
            {
                tbl_Station station = db.tbl_Stations.FirstOrDefault(i => i.id_station == int.Parse(cmbIdDepart.Text));
                tbDeparture.Text = station.station_name;
                tbDepartLocation.Text = station.station_location;
            }
        }

        private void cmbIdArrival_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(cmbIdArrival.Text))
            {
                tbl_Station station = db.tbl_Stations.FirstOrDefault(i => i.id_station == int.Parse(cmbIdArrival.Text));
                tbArrival.Text = station.station_name;
                tbArrivalLocation.Text = station.station_location;
            }
        }


    }
}
