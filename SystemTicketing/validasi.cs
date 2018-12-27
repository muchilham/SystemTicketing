using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SystemTicketing
{
    class validasi
    {
        ErrorProvider eValidasi;
        Control.ControlCollection collection;

        public validasi(Control.ControlCollection collection, ErrorProvider eValidasi)
        {
            this.collection = collection;
            this.eValidasi = eValidasi;
        }

        public void clear(String skip)
        {

            foreach (Control ctl in this.collection)
            {
                if (ctl.GetType() == typeof(TextBox) || ctl.GetType() == typeof(RichTextBox))
                {
                    if (ctl.Name == skip) continue;
                    ctl.Text = "";
                }
                else if (ctl.GetType() == typeof(ComboBox))
                    ((ComboBox)ctl).Text = "";
                else if (ctl.GetType() == typeof(NumericUpDown))
                    ((NumericUpDown)ctl).Value = 1;
                else if (ctl.GetType() == typeof(DateTimePicker))
                    ((DateTimePicker)ctl).Value = DateTime.Now;
            }
        }

        public bool doValidation()
        {
            foreach (Control ctl in this.collection)
            {
                if (ctl.GetType() == typeof(TextBox))
                {
                    if (String.IsNullOrEmpty(ctl.Text))
                    {
                        MessageBox.Show("All must be filled", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.eValidasi.SetError(ctl, "Cannot be empty!");
                        return false;
                    }

                    else
                    {
                        this.eValidasi.SetError(ctl, "");
                    }
                }

                else if (ctl.GetType() == typeof(ComboBox))
                {
                    if (((ComboBox)ctl).SelectedIndex < 0)
                    {
                        MessageBox.Show("All must be filled", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.eValidasi.SetError(ctl, "Must be selected!");
                        return false;
                    }

                    else
                    {
                        this.eValidasi.SetError(ctl, "");
                    }
                }

                else if (ctl.GetType() == typeof(NumericUpDown))
                {
                    if (String.IsNullOrEmpty(((NumericUpDown)ctl).Value.ToString()) || ((NumericUpDown)ctl).Value == 0)
                    {
                        MessageBox.Show("All must be filled", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.eValidasi.SetError(ctl, "Must be filled!");
                        return false;
                    }

                    else
                    {
                        this.eValidasi.SetError(ctl, "");
                    }
                }
            }
            return true;
        }

        public void numberFormat(KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true;
            }
        }
    }
}
