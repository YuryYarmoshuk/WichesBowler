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
    public partial class ConsultForm : Form
    {
        Form auth;

        public ConsultForm()
        {
            InitializeComponent();
        }

        public ConsultForm(Form authForm)
        {
            InitializeComponent();

            auth = authForm;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
            auth.Show();
        }

        private void ConsultForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            auth.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ConsultView view = new ConsultView(1);

            view.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ConsultView view = new ConsultView(2);

            view.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ConsultOrders consultOrders = new ConsultOrders();

            consultOrders.Show();
        }
    }
}
