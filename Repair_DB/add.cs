using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Repair_DB
{
    public partial class add : Form
    {
        public static bool isadmin = false;
        public add()
        {
            InitializeComponent();
            
    }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textBox2.UseSystemPasswordChar = false;




            }
            else
            {
                textBox2.UseSystemPasswordChar = true;

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
                if (textBox1.Text.ToLower() == "admin".ToLower() && textBox2.Text == "admin")
                {
                    isadmin = true;
                    new Main().Show();
                    this.Visible = false;

                }
                else
                {
                    MessageBox.Show("проверьте введеные данные или обратитесь к администратору");
                    return;
                }

          

        }

        private void Add_Load(object sender, EventArgs e)
        {
            
        }

        private void Add_Layout(object sender, LayoutEventArgs e)
        {
            
        }
    }
}
