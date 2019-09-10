using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WichesBowler
{
    public partial class EditForm : Form
    {
        private int indEdit;
        private string[] ans;
        BD bd = new BD();
        CorrectInputCheck inputCheck = new CorrectInputCheck();
        ListBox listBox;
        SubString subString = new SubString();

        public EditForm(int indEdit, string[] ans, ListBox listBox)
        {
            InitializeComponent();
            this.indEdit = indEdit;
            this.ans = ans;
            this.listBox = listBox;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            

            string title = textBox1.Text;
            int category = comboBox1.SelectedIndex;
            string cost = textBox2.Text;
            string discription = textBox3.Text;
            string quantity = textBox4.Text;

            string categoryMess;
            int costInt;
            int quantityInt;
            
            if (inputCheck.CheckAddEdit(title, category, cost, discription, quantity))
            {
                categoryMess = comboBox1.SelectedItem.ToString();
                costInt = Int32.Parse(cost);
                quantityInt = Int32.Parse(quantity);
                string[] newRec = new string[6] { title, categoryMess, cost, discription, quantity, ans[5] };

                if (inputCheck.ChangeCheck(ans, newRec))
                {
                    if (inputCheck.AccessMessage(String.Format("Do you really want to change the record? " +
                        StrForAccess(AccessEditor(newRec)))))
                    {
                        string req = ReqEditor(title, categoryMess, costInt, discription, quantityInt);

                        bd.WorkWithBD(req);

                        bd.ViewAll(listBox, true);
                    }
                }
            }
        }

        private void EditForm_Load(object sender, EventArgs e)
        {
            textBox1.Text = ans[0];
            textBox2.Text = ans[2];
            textBox3.Text = ans[3];
            textBox4.Text = ans[4];
            comboBox1.SelectedItem = ans[1];
        }

        public string ReqEditor(string title, string categoryMess, int costInt, string discription, int quantityInt)
        {
            string req = "";
            int id = Int32.Parse(ans[5]);

            req = String.Format("UPDATE Products SET Title = '{0}', Category = '{1}'," +
                " Cost = {2}, Discription = '{3}'," +
                " Quantity = {4} WHERE Id = {5}",title, categoryMess, costInt, discription, quantityInt, id);

            return req;
        }

        public string[] AccessEditor(string[] newRec)
        {
            string[] access;
            int count = 0;
            int k = 0;

            for (int i = 0; i < ans.Length; i++)
            {
                if (!newRec[i].Equals(ans[i]))
                {
                    count++;
                }
            }
            access = new string[count*2];

            for (int i = 0; i < ans.Length; i++)
            {
                if (!newRec[i].Equals(ans[i]))
                {
                    access[k] = ans[i];
                    access[k + 1] = newRec[i];

                    k += 2;
                }
            }

            return access;
        }

        public string StrForAccess(string[] access)
        {
            string str = "(";

            for (int i = 0; i < access.Length; i = i+2)
            {
                str += String.Format("{0} -> {1}", access[i], access[i+1]);

                if (i + 2 <access.Length)
                {
                    str += ", ";
                }
            }

            str += ")";

            return str;
        }
    }
}
