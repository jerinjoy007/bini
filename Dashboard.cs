using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gramboo.Database;
using System.Data.SqlClient;
using Gramboo.Controls;
using Gramboo;

using System.Runtime.InteropServices;

namespace project
{
    public partial class Dashboard : Gramboo.Controls.GrbForm
    {
        private DataController dc = new DataController();

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        
        private static extern IntPtr CreateRoundRectRgn
        (
              int nLeftRect,
              int nTopRect,
              int nRightRect,
              int nBottomRect,
              int nWidthEllipse,
              int nHeightEllipse

        );
        public Dashboard()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
            pnlNav.Height = btnDashbord.Height;
            pnlNav.Top = btnDashbord.Top;
            pnlNav.Left = btnDashbord.Left;
            btnDashbord.BackColor = Color.FromArgb(46, 51, 73);
            this.WindowState = FormWindowState.Minimized;
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {

        }

        private void btnDashbord_Click(object sender, EventArgs e)
        {
            pnlNav.Height = btnDashbord.Height;
            pnlNav.Top = btnDashbord.Top;
            pnlNav.Left = btnDashbord.Left;
            btnDashbord.BackColor = Color.FromArgb(46, 51, 73);
              project.Forms.GENERALFORMS.Login f2 = new project.Forms.GENERALFORMS.Login();
              f2.Show();
           // project.Classes.Common.DbName = dc.ConnectionProperties.DatabseName;
            //FA.Common.DbName = dc.ConnectionProperties.DatabseName;
           // frmMain frm = new frmMain();
           // frm.lbluser.Text = txtUserName.Text;
           // frm.Show();
           // frm.lblBranch.Text = "lib";
           // frm.lblBranch.Tag = txtBranchID.Text;
           // frm.lblCompanyName.Text = "lib";
           // frm.lblCompanyName.Tag = txtcompId.Text;
           // frm.lblYear.Text = "2021";// GetFiscalYear();

           // frm.lblCounter.Text = "accont";
            // frm.dept = Convert.ToInt64(CmbDepartment.SelectedValue);
           // frm.dept = 1101; //Convert.ToInt64(CmbDepartment.SelectedValue);
            this.Hide();
        }

        private void btnAnalytics_Click(object sender, EventArgs e)
        {
            pnlNav.Height = btnAnalytics.Height;
            pnlNav.Top = btnAnalytics.Top;
            btnAnalytics.BackColor = Color.FromArgb(46, 51, 73);
        }

        private void btnCalendar_Click(object sender, EventArgs e)
        {

            pnlNav.Height = btnCalendar.Height;
            pnlNav.Top = btnCalendar.Top;
            btnCalendar.BackColor = Color.FromArgb(46, 51, 73);
        }

        private void btnContactUs_Click(object sender, EventArgs e)
        {
            pnlNav.Height = btnContactUs.Height;
            pnlNav.Top = btnContactUs.Top;
            btnContactUs.BackColor = Color.FromArgb(46, 51, 73);
            Classes.Common.SendSMS();
        }

        private void btnsettings_Click(object sender, EventArgs e)
        {

            pnlNav.Height = btnsettings.Height;
            pnlNav.Top = btnsettings.Top;
            btnsettings.BackColor = Color.FromArgb(46, 51, 73);
        }

        private void btnDashbord_Leave(object sender, EventArgs e)
        {
            btnDashbord.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void btnAnalytics_Leave(object sender, EventArgs e)
        {
            btnDashbord.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void btnCalendar_Leave(object sender, EventArgs e)
        {
            btnDashbord.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void btnContactUs_Leave(object sender, EventArgs e)
        {
            btnDashbord.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void btnsettings_Leave(object sender, EventArgs e)
        {
            btnDashbord.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void circularProgressBar1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
