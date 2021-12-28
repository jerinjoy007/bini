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
using Gramboo.Controls;

namespace project
{
    public partial class Form1 : Gramboo.Controls.GrbForm
    {
        private static Form1 instance;

        public static Form1 Instance
        {

            get
            {
                if (instance == null)
                {
                    instance = new Form1();
                }
                else if (instance.IsDisposed)
                {
                    instance = new Form1();
                }
                return instance;
            }
        }
        public Form1()
        {
            InitializeComponent();
            Display_Report();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Classes.Common.SendSMS(DBConn, "Welcome", entryid, Convert.ToInt32(txtBranchID.Text));
        }
        private void Display_Report()
        {
            DBConn.ConnectionProperties.GenerateSQLConnectionString();
            CrystalDecisions.CrystalReports.Engine.ReportDocument cr = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            cr = new project.Report.CrystalReport1();
            project.Classes.Common.SetDatabaseLogon(cr, DBConn, false);
            cr.SetDatabaseLogon(DBConn.ConnectionProperties.DBUsername, DBConn.ConnectionProperties.DBPassword, DBConn.ConnectionProperties.DatabseName, DBConn.ConnectionProperties.DatabseName);
            crystalReportViewer1.ReportSource = cr;
        }
    }
}
