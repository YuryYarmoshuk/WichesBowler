using System;
using System.Windows.Forms;

namespace WichesBowler
{
    public class SeccessList
    {
        public string SeccessStr(int seccessID)
        {
            String str;

            switch (seccessID)
            {
                case 1:
                    str = "Product successfully added";
                    break;
                case 2:
                    str = "Account successfully added";
                    break;
                case 3:
                    str = "Product is added to bucket";
                    break;
                case 4:
                    str = "Order successfully added";
                    break;
                case 5:
                    str = "Order successfully edit";
                    break;
                case 6:
                    str = "Order successfully deleted";
                    break;
                default:
                    str = "Unknown error!";
                    break;
            }

            return str;
        }

        public void SeccessMessage(int msg)
        {
            MessageBox.Show(SeccessStr(msg), "Seccess", MessageBoxButtons.OK);
        }

        public void SeccessMessage(string msg)
        {
            MessageBox.Show(msg, "Seccess", MessageBoxButtons.OK);
        }
    }
}
