namespace WichesBowler.Controllers.Cons
{
    partial class ConsultOrders
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.NameLabel = new System.Windows.Forms.Label();
            this.Email = new System.Windows.Forms.Label();
            this.Number = new System.Windows.Forms.Label();
            this.Date = new System.Windows.Forms.Label();
            this.Status = new System.Windows.Forms.Label();
            this.id = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(284, 11);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(621, 329);
            this.listBox1.TabIndex = 0;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // NameLabel
            // 
            this.NameLabel.AutoSize = true;
            this.NameLabel.Location = new System.Drawing.Point(17, 11);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(38, 13);
            this.NameLabel.TabIndex = 1;
            this.NameLabel.Text = "Name:";
            // 
            // Email
            // 
            this.Email.AutoSize = true;
            this.Email.Location = new System.Drawing.Point(17, 116);
            this.Email.Name = "Email";
            this.Email.Size = new System.Drawing.Size(35, 13);
            this.Email.TabIndex = 2;
            this.Email.Text = "Email:";
            // 
            // Number
            // 
            this.Number.AutoSize = true;
            this.Number.Location = new System.Drawing.Point(17, 142);
            this.Number.Name = "Number";
            this.Number.Size = new System.Drawing.Size(47, 13);
            this.Number.TabIndex = 3;
            this.Number.Text = "Number:";
            // 
            // Date
            // 
            this.Date.AutoSize = true;
            this.Date.Location = new System.Drawing.Point(17, 169);
            this.Date.Name = "Date";
            this.Date.Size = new System.Drawing.Size(33, 13);
            this.Date.TabIndex = 4;
            this.Date.Text = "Date:";
            // 
            // Status
            // 
            this.Status.AutoSize = true;
            this.Status.Location = new System.Drawing.Point(17, 195);
            this.Status.Name = "Status";
            this.Status.Size = new System.Drawing.Size(40, 13);
            this.Status.TabIndex = 5;
            this.Status.Text = "Status:";
            // 
            // id
            // 
            this.id.AutoSize = true;
            this.id.Location = new System.Drawing.Point(17, 219);
            this.id.Name = "id";
            this.id.Size = new System.Drawing.Size(56, 13);
            this.id.TabIndex = 6;
            this.id.Text = "Bucket Id:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(419, 360);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Back";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(17, 48);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(249, 60);
            this.textBox1.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Address";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(17, 247);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(104, 44);
            this.button2.TabIndex = 10;
            this.button2.Text = "View bucket";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(17, 297);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(104, 44);
            this.button3.TabIndex = 11;
            this.button3.Text = "Change status";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // ConsultOrders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(917, 395);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.id);
            this.Controls.Add(this.Status);
            this.Controls.Add(this.Date);
            this.Controls.Add(this.Number);
            this.Controls.Add(this.Email);
            this.Controls.Add(this.NameLabel);
            this.Controls.Add(this.listBox1);
            this.Name = "ConsultOrders";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Orders";
            this.Load += new System.EventHandler(this.ConsultOrders_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label NameLabel;
        private System.Windows.Forms.Label Email;
        private System.Windows.Forms.Label Number;
        private System.Windows.Forms.Label Date;
        private System.Windows.Forms.Label Status;
        private System.Windows.Forms.Label id;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}