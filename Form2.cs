using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                panel2.Width += 3;
                if (panel2.Width >= 900)
                {
                    timer1.Stop();
                  project.Forms.GENERALFORMS.Login f2 = new project.Forms.GENERALFORMS.Login();
                  f2.Show();
                    this.Hide();

                }
               
            }
            catch (Exception)
            {

            }
        }
    }
}
