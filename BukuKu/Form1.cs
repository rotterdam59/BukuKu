using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BukuKu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBox1.Text = "admin";
            textBox2.Text = "password123";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
        }

        private void button1_Click(object sender, EventArgs e)
        {
         
            if(textBox1.Text != "admin" || string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Username atau Passowrd Salah");
                return;
            }
            if(textBox2.Text != "password123" || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Username atau Passowrd Salah");
                return;
            }

            new Daftar_Buku_Form().Show();
            this.Hide();

        }
    }
}
