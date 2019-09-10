using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using WichesBowler.Controllers.Guest;
using System.Windows.Forms;

namespace WichesBowler
{
    public partial class GuestForm : Form
    {
        Form authForm = null;
        ViewGuest viewGuest;
        CorrectInputCheck inputCheck;
        BD bd;
        string login;
        string password;
        int id;

        public GuestForm()
        {
            InitializeComponent();
        }

        public GuestForm(Form form, string log, string pass)
        {
            InitializeComponent();

            authForm = form;
            login = log;
            password = pass;
        }

        public GuestForm(Form form, int idBucket)
        {
            InitializeComponent();

            authForm = form;
            id = idBucket;

            label1.Visible = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            authForm.Show();
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(login))
            {
                viewGuest = new ViewGuest(login);
            }
            else
            {
                viewGuest = new ViewGuest(id);
            }

            viewGuest.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            inputCheck = new CorrectInputCheck();
            bd = new BD();

            if (!String.IsNullOrEmpty(login))
            {
                if (inputCheck.BucketIsEmpty(bd.IdCheck(login)))
                {
                    ViewBucket viewBucket = new ViewBucket(bd.IdCheck(login));
                    viewBucket.Show();
                }
            }
            else
            {
                if (inputCheck.BucketIsEmpty(id))
                {
                    ViewBucket viewBucket = new ViewBucket(id);
                    viewBucket.Show();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            inputCheck = new CorrectInputCheck();
            if (inputCheck.BucketIsEmpty(id))
            {
                if (!String.IsNullOrEmpty(login))
                {
                    bd = new BD();
                    Order order = new Order(bd.GetBucketId(login));
                    order.Show();
                }
                else
                {
                    Order order = new Order(id);
                    order.Show();
                }
            }
        }

        private void GuestForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (label1.Visible)
            {
                bd = new BD();
                if (!bd.OrderExist(id))
                {
                    int idProd;
                    int quant;
                    string[] prod = bd.AllBucket(id);
                    bool flag = true;

                    if (prod.Length == 0)
                    {
                        flag = false;
                    }

                    while (flag)
                    {
                        idProd = bd.ProdIdFromBucket(id);
                        quant = bd.QuantityProduct(idProd);
                        bd.WorkWithBD(String.Format("UPDATE Products SET Quantity = {0} WHERE Id = {1}", quant + 1, idProd));
                        bd.WorkWithBD(String.Format("DELETE TOP(1) FROM Buckets WHERE idBucket = {0}", id));

                        prod = bd.AllBucket(id);

                        if (prod.Length == 0)
                        {
                            flag = false;
                        }
                    }
                }
            }

            authForm.Show();
        }
    }
}
