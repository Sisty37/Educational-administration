using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AIUB
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            panel3.Width += 6;
            if (panel3.Width > 680)
            {
                timer1.Stop();

                LoginForm lForm = new LoginForm();
                lForm.Show();
                //this.Close();
                this.Hide();
            }

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
