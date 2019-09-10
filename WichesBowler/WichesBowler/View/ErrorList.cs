using System;
using System.Windows.Forms;

namespace WichesBowler
{
    public class ErrorList
    {
        public virtual string ErrorStr(int errorID)
        {
            String str;

            switch (errorID)
            {
                case 1:
                    str = "Login and Password fields must be filled!";
                    break;
                case 2:
                    str = "Login must contain from 3 to 20 characters!";
                    break;
                case 3:
                    str = "Password must contain from 3 to 20 characters!";
                    break;
                case 4:
                    str = "Username can only contain letters, numbers and underscores";
                    break;
                case 5:
                    str = "Login or password entered incorrectly! Try again!";
                    break;
                case 6:
                    str = "All fields must be filled!";
                    break;
                case 7:
                    str = "The cost must be integer!";
                    break;
                case 8:
                    str = "The cost must be positive!";
                    break;
                case 9:
                    str = "A product with that name already exists!";
                    break;
                case 10:
                    str = "Select any product!";
                    break;
                case 11:
                    str = "Account with that name already exists!";
                    break;
                case 12:
                    str = "Bucket is empty!";
                    break;
                case 13:
                    str = "Login or Password is wrong!";
                    break;
                case 14:
                    str = "All fields must be more than three characters!";
                    break;
                case 15:
                    str = "For email you need to choose a domain!";
                    break;
                case 16:
                    str = "The number can only contain numbers (0 - 9)!";
                    break;
                case 17:
                    str = "Uncorrect email name!";
                    break;
                default: 
                    str = "Unknown error!";
                    break;
            }

            return str;
        }

        public void ErrorMessage(int msg)
        {
            ErrorList err = new ErrorList();

            MessageBox.Show(err.ErrorStr(msg), "Error", MessageBoxButtons.OK);
        }

        public void ErrorMessage(string msg)
        {
            MessageBox.Show(msg, "Error", MessageBoxButtons.OK);
        }
    }
}
