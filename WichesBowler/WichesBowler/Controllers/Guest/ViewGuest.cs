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
    public partial class ViewGuest : Form
    {
        int wFull = 860;
        int wHide = 344;
        string login;
        BD bd = new BD();
        CorrectInputCheck inputCheck = new CorrectInputCheck();
        string[] products;
        SubString subString = new SubString();
        int idBucket;

        public ViewGuest()
        {
            InitializeComponent();
        }

        public ViewGuest(string log)
        {
            InitializeComponent();

            login = log;
            idBucket = bd.IdCheck(login);
        }

        public ViewGuest(int id)
        {
            InitializeComponent();
            
            idBucket = id;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (!listBox1.Visible)
            {
                button5.Text = "Hide list";
                Width = wFull;
            }

            if (listBox1.Visible)
            {
                button5.Text = "View list";
                Width = wHide;
            }
            
            listBox1.Visible = !listBox1.Visible;
        }

        private void ViewGuest_Load(object sender, EventArgs e)
        {
            bd.ViewAll(listBox1, false);
            

            listBox1.SelectedIndex = 0;
            string listMsg = listBox1.Text;

            products = bd.WorkWithBD(subString.IndReturn(listMsg, "Title"));
            Filded();
        }

        public void Filded()
        {
            textBox1.Text = products[0];
            textBox5.Text = products[1];
            textBox2.Text = products[2];
            textBox3.Text = products[3];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == listBox1.Items.Count-1)
            {
                listBox1.SelectedIndex = 0;
            }
            else
            {
                listBox1.SelectedIndex = listBox1.SelectedIndex + 1;
            }

            string listMsg = listBox1.Text;

            products = bd.WorkWithBD(subString.IndReturn(listMsg, "Title"));

            Filded();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
            {
                listBox1.SelectedIndex = 0;
            }

            if (listBox1.SelectedIndex == 0)
            {
                listBox1.SelectedIndex = listBox1.Items.Count - 1;
            }
            else
            {
                listBox1.SelectedIndex = listBox1.SelectedIndex - 1;
            }

            string listMsg = listBox1.Text;

            products = bd.WorkWithBD(subString.IndReturn(listMsg, "Title"));

            Filded();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string listMsg = listBox1.Text;

            products = bd.WorkWithBD(subString.IndReturn(listMsg, "Title"));

            Filded();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int countBe = listBox1.Items.Count;
            int countAf;

            SeccessList seccessList = new SeccessList();

            bd.WorkWithBD(String.Format("INSERT INTO Buckets VALUES({0}, {1})",idBucket, products[5]));
            bd.WorkWithBD(String.Format("UPDATE Products SET Quantity = {0} WHERE Id = {1}",Int32.Parse(products[4])-1,products[5]));
            bd.ViewAll(listBox1, false);

            countAf = listBox1.Items.Count;

            if (countBe != countAf)
            {
                listBox1.SelectedIndex = 0;
                Filded();
            }
            else
            {
                listBox1.SelectedIndex = subString.IdList(listBox1, products[0]);
                Filded();
            }

            seccessList.SeccessMessage(3);
        }
    }
}
