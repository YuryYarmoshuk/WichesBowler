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
    public partial class Order : Form
    {
        int idBucket;
        BD bd = new BD();
        CorrectInputCheck inputCheck = new CorrectInputCheck();
        SeccessList seccessList = new SeccessList();

        public Order()
        {
            InitializeComponent();
        }

        public Order(int id)
        {
            InitializeComponent();

            idBucket = id;
        }

        private void Order_Load(object sender, EventArgs e)
        {
            ChangeOrderForm();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int id = bd.OrderId(idBucket);

            if (inputCheck.AccessMessage("Do you want delete this order?"))
            {
                bd.WorkWithBD(String.Format("DELETE Orders WHERE Id = {0}", id));
            }

            seccessList.SeccessMessage(6);
            ChangeOrderForm();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string status = "not sent";
            DateTime time = DateTime.Now;
            string data = time.ToShortDateString();
            string name = textBox2.Text.Replace(" ","");
            string mname = textBox3.Text.Replace(" ", "");
            string sname = textBox1.Text.Replace(" ", "");
            string address = textBox4.Text;
            string hemail = textBox5.Text;
            string email = textBox5.Text + comboBox1.SelectedItem;
            string hnumber = textBox6.Text;
            string number = comboBox2.SelectedItem + textBox6.Text;

            if (inputCheck.OrderCheck(name, mname, sname, address, hemail, email, hnumber))
            {
                name = NormalStr(textBox2.Text);
                mname = NormalStr(textBox3.Text);
                sname = NormalStr(textBox1.Text);
                address = NormalAddress(textBox4.Text);

                bd.WorkWithBD(String.Format("INSERT INTO Orders VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}',{8})",
                    name, mname, sname, address,email,data, number, status, idBucket));
                seccessList.SeccessMessage(4);
            }

            ChangeOrderForm();
        }

        public void ChangeOrderForm()
        {
            if (bd.OrderExist(idBucket))
            {
                button1.Visible = false;
                button2.Visible = true;
                button3.Visible = true;
            }
            else
            {
                button1.Visible = true;
                button2.Visible = false;
                button3.Visible = false;
            }
        }

        public string NormalStr(string str)
        {
            return str.Substring(0, 1).ToUpper() + str.Substring(1).ToLower();
        }

        public string NormalAddress(string str)
        {
            bool split = false;
            if (str.IndexOf(",") != -1)
            {
                str = str.Replace(" ", "");
                split = true;
            }
            
            string[] adr = str.Split(',');

            if (!split)
            {
                adr = str.Split(' ');
            }

            string address = "";
            
            for (int i = 0; i < adr.Length; i++)
            {
                address += NormalStr(adr[i]);

                if (i+1 != adr.Length)
                {
                    address += ", ";
                }
            }

            return address;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DateTime time = DateTime.Now;
            string data = time.ToShortDateString();
            string name = textBox2.Text;
            string mname = textBox3.Text;
            string sname = textBox1.Text;
            string address = textBox4.Text;
            string hemail = textBox5.Text;
            string email = textBox5.Text + comboBox1.SelectedItem;
            string hnumber = textBox6.Text;
            string number = comboBox2.SelectedItem + " " + textBox6.Text;

            if (inputCheck.OrderCheck(name, mname, sname, address, hemail, email, hnumber))
            {
                name = NormalStr(textBox2.Text);
                mname = NormalStr(textBox3.Text);
                sname = NormalStr(textBox1.Text);
                address = NormalAddress(textBox4.Text);

                bd.WorkWithBD(String.Format("UPDATE Orders " +
                    "SET fName = '{0}'," +
                    "mName = '{1}'," +
                    "sName = '{2}'," +
                    "address = '{3}'," +
                    "email = '{4}'," +
                    "date = '{5}'," +
                    "number = '{6}' WHERE idBucket = {7}",
                    name, mname, sname, address, email, data, number, idBucket));
                seccessList.SeccessMessage(5);
            }
        }
    }
}
