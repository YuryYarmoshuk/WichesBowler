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
    public partial class ConsultView : Form
    {
        int idForm;
        int idBucket;

        public ConsultView()
        {
            InitializeComponent();
        }

        public ConsultView(int id)
        {
            InitializeComponent();

            idForm = id;
        }

        public ConsultView(int id, int idB)
        {
            InitializeComponent();

            idForm = id;
            idBucket = idB;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ConsultView_Load(object sender, EventArgs e)
        {
            BD bd = new BD();

            switch (idForm)
            {
                case 1:
                    {
                        bd.ViewAll(listBox1, true);
                        break;
                    }
                case 2:
                    {
                        bd.ViewBucket(listBox1);
                        break;
                    }
                case 3:
                    {
                        bd.ViewBucket(listBox1,idBucket);
                        break;
                    }
            }
        }
    }
}
