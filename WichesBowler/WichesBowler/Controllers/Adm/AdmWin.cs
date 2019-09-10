using System;
using System.Windows.Forms;

namespace WichesBowler
{
    public partial class AdmWin : Form
    {
        AuthWin authWin;
        AddForm addForm;
        
        public AdmWin()
        {
            InitializeComponent();
        }

        public AdmWin(AuthWin authForm)
        {
            InitializeComponent();
            authWin = authForm;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            addForm = new AddForm();

            addForm.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            authWin.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AccountForm accountForm = new AccountForm();
            accountForm.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ViewAll viewAll = new ViewAll();
            viewAll.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ViewAll viewAll = new ViewAll();
            viewAll.ButtonVisible("Delete");
            viewAll.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ViewAll viewAll = new ViewAll();
            viewAll.ButtonVisible("Edit");
            viewAll.Show();
        }

        private void AdmWin_FormClosed(object sender, FormClosedEventArgs e)
        {
            authWin.Show();
        }
    }
}
