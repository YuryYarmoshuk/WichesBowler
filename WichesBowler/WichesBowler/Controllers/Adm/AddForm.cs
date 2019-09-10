using System;
using System.Windows.Forms;

namespace WichesBowler
{
    public partial class AddForm : Form
    {
        CorrectInputCheck correctInput;
        BD bd;
        ErrorList error = new ErrorList();
        SeccessList secces = new SeccessList();

        public AddForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
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

            if (correctInput == null)
            {
                correctInput = new CorrectInputCheck();
            }

            if (bd == null)
            {
                bd = new BD();
            }
            
            if (correctInput.CheckAddEdit(title, category, cost, discription, quantity))
            {
                categoryMess = comboBox1.SelectedItem.ToString();
                costInt = Int32.Parse(cost);
                quantityInt = Int32.Parse(quantity);

                bd.WorkWithBD(String.Format("INSERT INTO Products VALUES ('{0}','{1}',{2},'{3}',{4})",title, categoryMess, cost, discription, quantity));

                secces.SeccessMessage(1);

                textBox1.Text = "";
                comboBox1.SelectedIndex = -1;
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
            }
        }
    }
}
