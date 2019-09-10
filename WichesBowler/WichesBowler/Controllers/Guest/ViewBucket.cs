using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WichesBowler.Controllers.Guest
{
    public partial class ViewBucket : Form
    {
        BD bd = new BD();
        CorrectInputCheck inputCheck = new CorrectInputCheck();
        ErrorList error = new ErrorList();
        int idBucket;
        string[][] products;

        public ViewBucket()
        {
            InitializeComponent();
        }

        public ViewBucket(int id)
        {
            InitializeComponent();

            idBucket = id;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ViewBucket_Load(object sender, EventArgs e)
        {
            Bucket();
        }

        public void Bucket()
        {
            var prod = bd.ViewAllBucket(idBucket);

            products = prod;
            ViewAllBucket(prod);

            TotalPrice();
        }

        public void ViewAllBucket(string[][] prod)
        {
            listBox1.Items.Clear();

            for (int i = 0; i < prod.Length; i++)
            {
                ViewOneProd(prod[i]);
            }
        }

        public void ViewOneProd(string[] prod)
        {
            string ans = "";

            ans = "Title: " + prod[0] + "\t" +
                  "Category: " + prod[1] + "\t" +
                  "Cost: " + prod[2] + "\t" +
                  "Discription: " + prod[3] + "\t";

            listBox1.Items.Add(ans);
        }

        public string OneProd(string[] prod)
        {
            string ans = "";

            ans = "Title: " + prod[0] + " " +
                  "Category: " + prod[1] + " " +
                  "Cost: " + prod[2] + " " +
                  "Discription: " + prod[3];

            return ans;
        }

        public void TotalPrice()
        {
            int sum = 0;

            foreach(string[] prod in products)
            {
                sum += Int32.Parse(prod[2]);
            }

            textBox1.Text = sum.ToString(); ;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count != 0)
            {
                if (!checkBox1.Checked)
                {
                    if (inputCheck.SelectedItemCheck(listBox1.SelectedIndex))
                    {
                        int ind = listBox1.SelectedIndex;
                        if (inputCheck.AccessMessage(String.Format("Do you want delete this records? ({0})", OneProd(products[ind]))))
                        {
                            bd.WorkWithBD(String.Format("UPDATE Products SET Quantity = {0} WHERE Id = {1}", Int32.Parse(products[ind][4]) + 1, products[ind][5]));
                            bd.WorkWithBD(String.Format("DELETE TOP (1) FROM Buckets WHERE idProducts = {0}", products[ind][5]));
                            Bucket();
                        }
                    }
                }
                else
                {
                    if (inputCheck.AccessMessage("Do you want delete all records?"))
                    {
                        for (int i = 0; i < products.Length; i++)
                        {
                            bd.WorkWithBD(String.Format("UPDATE Products SET Quantity = {0} WHERE Id = {1}", Int32.Parse(products[i][4]) + 1, products[i][5]));
                            bd.WorkWithBD(String.Format("DELETE TOP (1) FROM Buckets WHERE idProducts = {0}", products[i][5]));
                        }
                        Bucket();
                    }
                }
            }
            else
            {
                error.ErrorMessage(12);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Order order = new Order(idBucket);
            order.Show();
        }
    }
}
