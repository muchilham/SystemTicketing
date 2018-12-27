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
    public partial class frmPaymentDetail : Form
    {
        TicketingDBDataContext db = new TicketingDBDataContext();
        frmMain frm;
        tbl_Payment pay;
        validasi va;
        String idBooking;
        double harga;
        public frmPaymentDetail(String idBooking)
        {
            InitializeComponent();
            va = new validasi(this.Controls, this.eValidasi);
            this.idBooking = idBooking;
        }

        public frmPaymentDetail(frmMain frm)
        {
            InitializeComponent();
            va = new validasi(this.Controls, this.eValidasi);
            this.frm = frm;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult fk = MessageBox.Show("Are you sure want to Cancel this Payment?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (fk == DialogResult.No) return;
            this.Close();
        }

        public void load()
        {
            tbIdBooking.Text = idBooking;
            var query = (from bd in db.tbl_BookingDetails
                         join b in db.tbl_BookingMasters
                         on bd.id_booking equals b.id_booking

                         join s in db.tbl_Schedules
                         on b.id_schedule equals s.id_schedule

                         where b.id_booking == int.Parse(tbIdBooking.Text)
                         select new
                         {
                             total = s.harga * b.total_ticket
                         }).FirstOrDefault();
            tbTotalPayment.Text = query.total.ToString();
        }

        private void action()
        {
            pay = new tbl_Payment
            {
                id_booking = int.Parse(tbIdBooking.Text),
                total_payment = int.Parse(tbTotalPayment.Text),
                paid = int.Parse(tbPaid.Text)
            };

            var f = from b in db.tbl_BookingMasters
                    where b.id_booking == int.Parse(tbIdBooking.Text)
                    select b;

            foreach (var w in f)
            {
                w.status = "PAID";
            }
        }
        private void frmPaymentDetail_Load(object sender, EventArgs e)
        {
            load();
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
                if (pay.paid < pay.total_payment)
                {
                    MessageBox.Show("Your money is not sufficient", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbPaid.Text = "";
                    tbPaid.Focus();
                    return;
                }
                db.tbl_Payments.InsertOnSubmit(pay);
                db.SubmitChanges();
                MessageBox.Show(message + " data success!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmPayment fr = new frmPayment();
                fr.btnSearch_Click(sender, e);
                this.Close();
                va.clear("");
            }

            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }
        }
    }
}
