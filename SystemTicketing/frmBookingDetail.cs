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
    public partial class frmBookingDetail : Form
    {
        TicketingDBDataContext db = new TicketingDBDataContext();
        frmMain frm;
        tbl_BookingMaster bok;
        tbl_BookingDetail bokd;
        validasi va;
        String idSchedule , idCustomer, fullname, address, town, country, postcode, email;
        int klik = 0, cek, total, id;
        public frmBookingDetail(String idSchedule, int total)
        {
            InitializeComponent();
            va = new validasi(this.Controls, this.eValidasi);
            this.idSchedule = idSchedule;
            this.total = total;
        }

        public frmBookingDetail(frmMain frm)
        {
            InitializeComponent();
            va = new validasi(this.Controls, this.eValidasi);
            this.frm = frm;
        }

        private void loadGrid()
        {
            dgvCustomer.DataSource = db.tbl_Customers.ToList();
            tbIdSchedule.Text = idSchedule;
            tbTotal.Text = total.ToString();
        }


        public void checkdata()
        {
            var query = from sc in db.tbl_Schedules 
                      join b in db.tbl_BookingMasters
                      on sc.id_schedule equals b.id_schedule

                      join bd in db.tbl_BookingDetails
                      on b.id_booking equals bd.id_booking
                      where sc.id_schedule == int.Parse(idSchedule) && bd.id_customer == int.Parse(idCustomer)
                      select bd.id_customer;
            foreach (var qw in query)
            {
                id = qw;
                if (id != int.Parse(idCustomer))
                {
                    foreach (ListViewItem lv in this.listView1.Items)
                    {
                        if (idCustomer == lv.SubItems[0].Text)
                        {
                            cek = 1;
                        }

                        else if (total <= listView1.Items.Count)
                        {
                            cek = 2;
                        }

                        else
                        {
                            cek = 0;
                        }
                    }
                }

                else
                {
                    cek = 1;
                }
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult fk = MessageBox.Show("Are you sure want to Cancel this Booking?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (fk == DialogResult.No) return;
            this.Close();
        }

        private void frmBookingDetail_Load(object sender, EventArgs e)
        {
            loadGrid();
        }

        private void dgvCustomer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            klik = 1;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (klik == 1)
            {
                foreach (DataGridViewRow rw in this.dgvCustomer.SelectedRows)
                {
                    idCustomer = rw.Cells[0].Value.ToString();
                    fullname = rw.Cells[1].Value.ToString() + " " + rw.Cells[2].Value.ToString();
                    address = rw.Cells[3].Value.ToString();
                    town = rw.Cells[4].Value.ToString();
                    country = rw.Cells[5].Value.ToString();
                    postcode = rw.Cells[6].Value.ToString();
                    email = rw.Cells[7].Value.ToString();
                }
                checkdata();
                if (cek == 0)
                {
                    ListViewItem lv = new ListViewItem(idCustomer);
                    lv.SubItems.Add(fullname);
                    lv.SubItems.Add(address);
                    lv.SubItems.Add(town);
                    lv.SubItems.Add(country);
                    lv.SubItems.Add(postcode);
                    lv.SubItems.Add(email);
                    listView1.Items.Add(lv);
                }

                else if (cek == 2)
                {
                    MessageBox.Show("Stock Full", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                else
                {
                    MessageBox.Show("Has been Registered", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count < 1)
            {
                listView1.Items.Clear();
            }

            else
            {
                listView1.FocusedItem.Remove();
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count < 1)
            {
                MessageBox.Show("Please registered a Customer", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            else
            {

                DialogResult fc = MessageBox.Show("Are you sure want to add this data?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (fc == DialogResult.No) return;
                if (total < listView1.Items.Count)
                {
                    MessageBox.Show("Stock Full", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    bok = new tbl_BookingMaster
                    {
                        id_schedule = int.Parse(idSchedule),
                        total_ticket = listView1.Items.Count,
                        booking_date = DateTime.Now,
                        status = "NOT PAID"
                    };
                    db.tbl_BookingMasters.InsertOnSubmit(bok);
                    db.SubmitChanges();

                    int id = bok.id_booking;
                    for (int i = 0; i < listView1.Items.Count; i++)
                    {
                        bokd = new tbl_BookingDetail
                        {
                            id_booking = id,
                            id_customer = int.Parse(idCustomer)
                        };
                        db.tbl_BookingDetails.InsertOnSubmit(bokd);
                        db.SubmitChanges();
                    }
                    MessageBox.Show("Booking data success!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
