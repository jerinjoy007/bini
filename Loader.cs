using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;


namespace project
{
    public partial class Loader : Form
    {

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]

        private static extern IntPtr CreateRoundRectRgn
            (
            int nLeftRect,
            int nTopRect,
            int RightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse

            );

        
        public Loader()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
            prograssbar1.Value = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            prograssbar1.Value += 5;
            prograssbar1.Text = prograssbar1.Value.ToString() + "%";

            if (prograssbar1.Value == 100)
            {
                timer1.Enabled = false;
                Dashboardlib f2 = new Dashboardlib();
                //project.Forms.GENERALFORMS.Login f2 = new project.Forms.GENERALFORMS.Login();
                f2.Show();
               
                this.Hide();
            }
        }
    }
}
  