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
    public partial class AccountForm : Form
    {
        BD bd = new BD();
        CorrectInputCheck inputCheck = new CorrectInputCheck();
        SubString subString = new SubString();
        ErrorList errorList = new ErrorList();

        public AccountForm()
        {
            InitializeComponent();
        }

        private void AccountForm_Load(object sender, EventArgs e)
        {
            bd.ViewAllAcc(listBox1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string login = textBox1.Text;
            string pass = textBox2.Text;

            if (inputCheck.CheckLogPas(login, pass))
            {
                if (inputCheck.CorrectInputLogin(login) && inputCheck.LoginExist(login))
                {
                    string req = String.Format("INSERT INTO Accounts (Login, Password, Access) " +
                        "VALUES ('{0}', '{1}', 'Consultant')", login, pass);

                    bd.WorkWithBD(req);

                    bd.ViewAllAcc(listBox1);

                    textBox1.Text = "";
                    textBox2.Text = "";
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int ind = listBox1.SelectedIndex;
            int id;
            string login;

            if (inputCheck.SelectedItemCheck(ind))
            {
                id = subString.IndReturn(listBox1.Text, "Login");
                login = subString.TitleReturn(listBox1.Text, "Login", "Password");

                login.Replace(" ", "");
                if (inputCheck.AccessMessage(String.Format("Do you really want to delete the record? ({0} {1})", id, login)))
                {
                    string req = String.Format("DELETE FROM Accounts WHERE Id = {0}", id);

                    bd.WorkWithBD(req);

                    bd.ViewAllAcc(listBox1);
                }
            }
            else
            {
                errorList.ErrorMessage(10);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
