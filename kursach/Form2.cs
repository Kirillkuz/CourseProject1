using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace kursach
{
    public partial class Form2 : Form
    {
        public string name4, sort4;
        public int spirt4, price4;

        public Form2()
        {
            InitializeComponent();
            
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            name4 = textBox1.Text;
            spirt4 =Convert.ToInt32( textBox2.Text);
            sort4 = textBox3.Text;
            price4 =Convert.ToInt32( textBox4.Text);
            if ((spirt4 > 100) | (spirt4 < 0))
            {

                MessageBox.Show("Вы неправильно ввели содержание спирта");
            }
            else
            {
                Form1 main = this.Owner as Form1;
                main.vvod(name4, spirt4, sort4, price4);
                Hide();
            }

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && e.KeyChar != 8)
                e.Handled = true;
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && e.KeyChar != 8)
                e.Handled = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if ((textBox1.Text.Length > 0)&(textBox2.Text.Length > 0)&(textBox3.Text.Length > 0)&(textBox4.Text.Length > 0))
            { button1.Enabled = true; }
        }
    }
}
