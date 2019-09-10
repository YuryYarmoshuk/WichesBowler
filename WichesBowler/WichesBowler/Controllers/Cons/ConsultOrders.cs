using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WichesBowler.Controllers.Cons
{
    public partial class ConsultOrders : Form
    {
        string[] order;
        BD bd = new BD();
        SubString sub = new SubString();


        public ConsultOrders()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ConsultOrders_Load(object sender, EventArgs e)
        {
            bd.OrderView(listBox1);
            order = bd.OrderView(sub.IndReturn(listBox1.Items[0].ToString(), "Full"));
            listBox1.SelectedIndex = 0;
            Filded();
        }

        public void Filded()
        {
            NameLabel.Text = "Name:" + order[3] +  " "+ order[1] + " " + order[2];
            textBox1.Text = order[4];
            Email.Text = "Email: " + order[5];
            Number.Text = "Number: " + order[7];
            Date.Text = "Date: " + order[6];
            Status.Text = "Status: " + order[8];
            id.Text = "Bucket Id: " + order[9];
        }

        public void ViewOrders()
        {
            order = bd.OrderView(sub.IndReturn(listBox1.Items[listBox1.SelectedIndex].ToString(), "Full"));
            Filded();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewOrders();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ConsultView view = new ConsultView(3, Int32.Parse(order[9]));

            view.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int id = listBox1.SelectedIndex;

            string status;

            if (order[8].Equals("not sent"))
            {
                status = "delivered";
            }
            else
            {
                status = "not sent";
            }

            bd.WorkWithBD(String.Format("UPDATE Orders SET status = '{0}' WHERE Id = {1}", status, order[0]));

            bd.OrderView(listBox1);

            listBox1.SelectedIndex = id;

            ViewOrders();
        }
    }
}
