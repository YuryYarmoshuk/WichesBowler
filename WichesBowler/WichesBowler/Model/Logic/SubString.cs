using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WichesBowler
{
    public class SubString
    {
        public int IndReturn(string str, string secSub)
        {
            int id = -1;

            str = str.Replace(" ", "");
            str = str.Replace("Id:", "");
            if (str.IndexOf(secSub) != -1)
            {
                str = str.Remove(str.IndexOf(secSub));
            }

            if (str != "")
            {
                id = Int32.Parse(str);
            }

            return id;
        }

        public string TitleReturn(string str, string firSub, string secSub)
        {
            str = str.Replace(" ", "");

            if (str.IndexOf(firSub) != -1 && str.IndexOf(secSub) != -1)
            {
                str = str.Remove(0, str.IndexOf(firSub) + 6);
                str = str.Remove(str.IndexOf(secSub));
            }
            
            return str;
        }

        public int IdList(ListBox list, string title)
        {
            int id = -1;
            string lineStr;
            
            for (int i =0; i < list.Items.Count; i++)
            {
                lineStr = list.Items[i].ToString();

                if (lineStr.IndexOf(title) != -1)
                {
                    return i;
                }
            }

            return id;
        }
    }
}
