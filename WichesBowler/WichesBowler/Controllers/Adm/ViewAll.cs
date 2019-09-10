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
    public partial class ViewAll : Form
    {
        BD bd = new BD();
        CorrectInputCheck inputCheck = new CorrectInputCheck();
        SubString subString = new SubString();
        ErrorList errorList = new ErrorList();

        public ViewAll()
        {
            InitializeComponent();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ViewAll_Load(object sender, EventArgs e)
        {
            bd.ViewAll(listBox1, true);
        }

        public void ButtonVisible(string bName)
        {
            button2.Text = bName;
            button2.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int ind = listBox1.SelectedIndex;
            int id;
            string title;

            if (inputCheck.SelectedItemCheck(ind)) {
                id = subString.IndReturn(listBox1.Text, "Title");
                title = subString.TitleReturn(listBox1.Text, "Title", "Category");

                if (button2.Text == "Delete")
                {
                    if (inputCheck.AccessMessage(String.Format("Do you really want to delete the record? ({0} {1})",id, title)))
                    {
                        string req = String.Format("DELETE FROM Products WHERE Id = {0}", id);

                        bd.WorkWithBD(req);

                        bd.ViewAll(listBox1, true);
                    }
                }
                else
                {
                    
                    EditForm editForm = new EditForm(id, bd.WorkWithBD(id), listBox1);

                    editForm.Show();
                }
            }
            else
            {
                errorList.ErrorMessage(10);
            }
        }
    }
}
