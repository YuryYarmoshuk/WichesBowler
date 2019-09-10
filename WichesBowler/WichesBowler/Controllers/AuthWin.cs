using System;
using System.Windows.Forms;
using WichesBowler.Controllers.Cons;

namespace WichesBowler
{
    public partial class AuthWin : Form
    {
        AdmWin adm;
        GuestForm guest;
        ConsultForm consult;
        CorrectInputCheck correctInput;
        BD bd;
        SeccessList seccess;
        ErrorList errorList = new ErrorList();

        public AuthWin()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string login = textBox1.Text;
            string password = textBox2.Text;

            correctInput = new CorrectInputCheck();
            
            bd = new BD();

            if (correctInput.CheckLogPas(login, password))
            {
                if (correctInput.CorrectInputLogin(login))
                {
                    if (bd.AuthCheck(login, password))
                    {
                        switch (bd.AccessCheck(login))
                        {
                            case "Admin":
                                {
                                    adm = new AdmWin(this);
                                    adm.Show();
                                    clearFields();
                                    Hide();
                                    break;
                                }
                            case "Consultant":
                                {
                                    consult = new ConsultForm(this);
                                    consult.Show();
                                    clearFields();
                                    Hide();
                                    break;
                                }
                            case "Guest":
                                {
                                    guest = new GuestForm(this, login, password);
                                    guest.Show();
                                    clearFields();
                                    Hide();
                                    break;
                                }
                            default:
                                errorList.ErrorMessage("Unknow access!!!");
                                break;
                        }
                    }
                    else
                    {
                        errorList.ErrorMessage(13);
                    }
                }
            }
        }

        private void clearFields()
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string login = textBox1.Text;
            string password = textBox2.Text;

            
            correctInput = new CorrectInputCheck();

            bd = new BD();

            seccess = new SeccessList();

            if (correctInput.CheckLogPas(login, password))
            {
                if (correctInput.CorrectInputLogin(login))
                {
                    if (correctInput.LoginExist(login))
                    {
                        bd.WorkWithBD(String.Format("INSERT INTO Accounts VALUES('{0}','{1}','{2}',{3})",login, password, "Guest", correctInput.BucketIdGen()));
                        seccess.SeccessMessage(2);
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            correctInput = new CorrectInputCheck();
            int idBucket = correctInput.BucketIdGen();

            guest = new GuestForm(this, idBucket);
            guest.Show();
            clearFields();
            Hide();
        }
    }
}
