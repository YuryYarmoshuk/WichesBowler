using System.Linq;
using System;
using System.Windows.Forms;

namespace WichesBowler
{
    public class CorrectInputCheck
    {
        ErrorList errorList = new ErrorList();

        public bool CheckLogPas(string log, string pass)
        {
            if (log == "" || pass == "")
            {
                errorList.ErrorMessage(1);
                return false;
            }
            else if (log.Length < 3 || log.Length > 20)
            {
                errorList.ErrorMessage(2);
                return false;
            }
            else if (pass.Length < 3 || pass.Length > 20)
            {
                errorList.ErrorMessage(3);
                return false;
            }

            return true;
        }

        public bool CheckAddEdit(string title, int category, string cost, string discription, string quantity)
        {
            int costInt;
            BD bd = new BD();

            if (title == "" || category == -1 || cost == "" || discription == "" || quantity == "")
            {
                errorList.ErrorMessage(6);
                return false;
            }
            else if (!Int32.TryParse(cost, out costInt))
            {
                errorList.ErrorMessage(7);
                return false;
            }
            else if (Int32.Parse(cost) < 0)
            {
                errorList.ErrorMessage(8);
                return false;
            }
            else if (bd.ExistTitleCheck(title))
            {
                errorList.ErrorMessage(9);
                return false;
            }

            return true;
        }

        public bool CorrectInputLogin(string log)
        {
            string mass = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890_";

            if (log == "")
            {
                return false;
            }

            foreach (char ch in log)
            {
                if (!mass.Contains(ch))
                {
                    errorList.ErrorMessage(4);
                    return false;
                }
            }
            
            return true;
        } 

        public bool BucketIsEmpty(int id)
        {
            BD bd = new BD();
            ErrorList errorList = new ErrorList();
            bool ans = true;
            if (!bd.EmptyBucketCheck(id))
            {
                ans = false;
                errorList.ErrorMessage(12);
            }

            return ans;
        }

        public bool LoginExist(string login)
        {
            BD bd = new BD();

            bool ans = true;

            if (bd.ExistAccCheck(login))
            {
                errorList.ErrorMessage(11);
                ans = false;
            }

            return ans;
        }

        public bool SelectedItemCheck (int ind)
        {
            if (ind == -1)
            {
                errorList.ErrorMessage(10);
                return false;
            }

            return true;
        }

        public bool AccessMessage(string str)
        {
            bool ans;

            DialogResult result = MessageBox.Show(str, "Access", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                ans = true;
            }
            else
            {
                ans = false;
            }

            return ans;
        }

        public bool OrderCheck(string name, string mname, string surname, string address, string email, string fullEmail, string number)
        {
            string[] emailPref = new string[4] { "@mail.ru", "@gmail.com", "@tut.by", "@yandex.ru" };
            string numbers = "1234567890";

            if (name.Length < 3 || mname.Length < 3 || surname.Length < 3 || address.Length < 3 || number.Length < 3 ||
                name.Length > 20 || mname.Length > 20 || surname.Length > 20 || address.Length > 50 || number.Length > 20 ||
                email.Length < 3 || email.Length > 20)
            {
                errorList.ErrorMessage(14);
                return false;
            }

            if (email.IndexOf("@") != -1)
            {
                errorList.ErrorMessage(17);
                return false;
            }

            if (fullEmail.IndexOf("@") != -1)
            {
                fullEmail = fullEmail.Remove(0, fullEmail.IndexOf("@"));
            }
            else
            {
                errorList.ErrorMessage(15);
                return false;
            }

            bool ans = false;

            for(int i = 0; i < emailPref.Length; i++)
            {
                if (emailPref[i].Equals(fullEmail))
                {
                    ans = true;
                }
            }

            if (!ans)
            {
                errorList.ErrorMessage(15);
                return false;
            }

            foreach (char numb in number)
            {
                if (!numbers.Contains(numb))
                {
                    errorList.ErrorMessage(16);
                    return false;
                }
            }

            return true;
        }

        public bool ChangeCheck(string[] oldRec, string[] newRec)
        {
            bool ans = false;

            for (int i = 0; i < oldRec.Length; i++)
            {
                if (!newRec[i].Equals(oldRec[i]))
                {
                    ans = true;
                    break;
                }
            }

            return ans;
        }

        public int BucketIdGen()
        {
            int id;
            BD bd = new BD();
            Random random = new Random();

            id = random.Next(1000);
            while (bd.ExistIdCheck(id))
            {
                id = random.Next(1000);
            }
            
            return id;
        }
    }
}
