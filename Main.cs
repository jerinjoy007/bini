using Gramboo;
using project.Forms.GENERALFORMS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
//using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace project
{
    public partial class frmMain : Form
    {


        private frmmenu MenuWindow = new frmmenu();
       private FavoriteMenu MenuWindow1 = new FavoriteMenu();

        private DataController dc = new DataController();
        int Add, update, delete;
        public Int64 dept;
        private Int64 m_ChildFormNumber = 0;
        private ToolStripProfessionalRenderer oDefaultRenderer = new ToolStripProfessionalRenderer(new PropertyGridEx.CustomColorScheme());
        private bool IsTopExpanded = true;
        public delegate void AfterSaveEventHandler(object sender, EventArgs e);


        public event AfterSaveEventHandler AfterSave;

        public frmMain()
        {
            // This call is required by the Windows Form Designer.
            InitializeComponent();

            // Add any initialization after the InitializeComponent() call.

        }

         string nod;
        private IDockContent ReloadContent(string persistString)
        {
            switch (persistString)
            {

                case "frmProperty":

                    MenuWindow = new frmmenu();

                    return MenuWindow;

            }


            //switch (persistString)
            //{

            //    case "frmProperty":

            //        MenuWindow1 = new Favfrmmenu();

            //        return MenuWindow1;

            //}


            return null;

        }
      
      private void CreateBasicLayout()
        {
            MenuWindow.Show(DockPanel,  DockState.DockLeft);
            MenuWindow.Width = 200;

            MenuWindow1.Show(DockPanel,DockState.DockRightAutoHide);
            MenuWindow1.Width = 50;

          //  MenuWindow1.AutoHidePortion=
        }
        public void ShowChild( DockContent frmChild)
        {
            //' Make it a child of this MDI form before showing it.

            if (frmChild.IsDisposed == false)
            {
               
                m_ChildFormNumber++;
                frmChild.Show(DockPanel);
            }
           

        }



        private void ControlBoxMouseMove(PictureBox p)
        {
            p.BorderStyle = BorderStyle.FixedSingle;
        }
        private void ControlBoxMouseLeave(PictureBox p)
        {
            p.BorderStyle = BorderStyle.None;
        }
 
 
  
        private void frmMain_Load(object sender, EventArgs e)
        {
       




            // string configFile = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Application.ExecutablePath), "DockPanel.config");
            //    ' Apply a gray professional renderer as a default renderer
            ToolStripManager.Renderer = oDefaultRenderer;
            oDefaultRenderer.RoundedEdges = false;

            //    ' Set DockPanel properties
            DockPanel.ActiveAutoHideContent = null;
            DockPanel.Parent = this;
            Gramboo.Docking.Extender.SetSchema(DockPanel, Gramboo.Docking.Extender.Schema.VS2005);

            cmbCompany.Visible = false;

            DockPanel.SuspendLayout(true);
            //if (System.IO.File.Exists(configFile))
            //{
            //    DockPanel.LoadFromXml(configFile, ReloadContent);
            //}
            //else
            //{
            //' Load a basic layout
            CreateBasicLayout();
            //}
            MenuWindow1.Width = 50;
            DockPanel.ResumeLayout(true, true);


            if (System.AppDomain.CurrentDomain.FriendlyName == "MODTAB.exe")
            {
                MenuWindow1.Width = 0;

                //ShowChild(project.Forms.SALE.TabletEstimation.Instance);
                MenuWindow1.Hide();
            }
            if (lbluser.Text.ToUpper() != "ADMIN")
            {

                PopulateMenus(MenuWindow.TreeView_Master, "Master Files");
                PopulateMenus(MenuWindow.TreeView_Transaction, "Transactions");
                PopulateMenus(MenuWindow.treeView_reports, "Reports");

               


                //PopulateMenus(MenuWindow1.TreeView_Master, "Master Files");
                //PopulateMenus(MenuWindow1.TreeView_Transaction, "Transactions");
                //PopulateMenus(MenuWindow1.treeView_reports, "Reports");

            }
            MenuWindow1.treeview1.Nodes.Clear();
            PopulateFavouriteMenus(MenuWindow1.treeview1, "Master Files");
            PopulateFavouriteMenus(MenuWindow1.treeview1, "Transactions");
            PopulateFavouriteMenus(MenuWindow1.treeview1, "Reports");
        }

 
        private void PopulateFavouriteMenus(TreeView treeView1, string MenuName = "")
        {

            Gramboo.GRBConfig c = Gramboo.GRBConfig.Open();

            int i = c.Login.UserId;
            TreeNode parentNode;
            string str = "";
           
            if (MenuName.Length == 0)
            {
                str = "Select * FROM [SYST].[VFavMenu] WHERE ParentId=0 and user_id= " + i + " Order by MenuId";

            }
            else
            {
                str = "Select * FROM [SYST].[VFavMenu] where [Menu Name]='" + MenuName + "' and  ParentId =0  and user_id= " + i + " Order by FavMenuId";
            }
            // str = "Select * FROM [SYST].[VFavMenu] WHERE ParentId=0 Order by MenuId";
            DataTable dt = dc.GetData(new SqlCommand(str), "VFavMenu").Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                parentNode = treeView1.Nodes.Add(dr["Menu Name"].ToString());
                PopulateFavouriteTreeView(treeView1,Convert.ToInt32(dr["menuId"].ToString()), parentNode);
            }
            treeView1.ExpandAll();
        }

        private void PopulateFavouriteTreeView(TreeView treeView1, int parentId, TreeNode parentNode)
        {
            Gramboo.GRBConfig c = Gramboo.GRBConfig.Open();

            int i = c.Login.UserId;
            DataTable dtchildc = dc.GetData(new SqlCommand("Select * FROM [SYST].[VFavMenu] Where ParentId=" + parentId + " and user_id= " + i + "Order by MenuId"), "VFavMenu").Tables[0];
            TreeNode childNode;
            foreach (DataRow dr in dtchildc.Rows)
            {
                if (parentNode == null)
                    childNode = treeView1.Nodes.Add(dr["Menu Name"].ToString());
                else
                    childNode = parentNode.Nodes.Add(dr["Menu Name"].ToString());
                PopulateFavouriteTreeView(treeView1, Convert.ToInt32(dr["menuId"].ToString()), childNode);
            }
        }

        private void PicMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void picMaximize_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
                picMaximize.Image = project.Properties.Resources.Restore;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
                //picMaximize.Image = JMS_RET.Properties.Resources.Maximise;
            }
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to exit now?", "Exit application", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            else
            {

                Application.Exit();
            }
        }

        private void PicMinimize_MouseLeave(object sender, EventArgs e)
        {
            ControlBoxMouseLeave((PictureBox)sender);
        }

        private void picMaximize_MouseLeave(object sender, EventArgs e)
        {
            ControlBoxMouseLeave((PictureBox)sender);
        }

        private void picClose_MouseLeave(object sender, EventArgs e)
        {
            ControlBoxMouseLeave((PictureBox)sender);
        }

        private void PicMinimize_MouseHover(object sender, EventArgs e)
        {
            ControlBoxMouseMove((PictureBox)sender);
        }

        private void picMaximize_MouseHover(object sender, EventArgs e)
        {
            ControlBoxMouseMove((PictureBox)sender);
        }

        private void picClose_MouseHover(object sender, EventArgs e)
        {
            ControlBoxMouseMove((PictureBox)sender);
        }

     
        private void pnlControlBox_MouseHover(object sender, EventArgs e)
        {
            foreach (PictureBox p in pnlControlBox.Controls)
            {
                p.BorderStyle = BorderStyle.None;

            }
        }

        private void ToolStripNew_Click(object sender, EventArgs e)
        {
            if (DockPanel.ActiveDocument == null)
                return;

            if (DockPanel.ActiveDocument.GetType().BaseType == typeof(Gramboo.Controls.GrbForm))
            {
                ((Gramboo.Controls.GrbForm)DockPanel.ActiveDocument).Init();
            }
        }

        private void ToolStripSave_Click(object sender, EventArgs e)
        {
            if (DockPanel.ActiveDocument == null)
                return;


            if (DockPanel.ActiveDocument.GetType().BaseType == typeof(Gramboo.Controls.GrbForm))
            {
                Gramboo.Controls.GrbForm f = new Gramboo.Controls.GrbForm();
                f = ((Gramboo.Controls.GrbForm)DockPanel.ActiveDocument);


                Access((f.Tag==null?"":f.Tag) .ToString());

                if (f.IsEditMode)
                {
                    if (update == 0)
                    {
                        Gramboo.General.ShowMessage("You Don't Have Permission To Perform This Action", "Access Denied", MessageBoxIcon.Hand, MessageBoxButtons.OKCancel, MessageBoxDefaultButton.Button1);
                    }
                    else
                    {
                        f.Save();
                    }
                }
                else
                {
                    if (Add == 0)
                    {
                        Gramboo.General.ShowMessage("You Don't Have Permission To Perform This Action", "Access Denied", MessageBoxIcon.Hand, MessageBoxButtons.OKCancel, MessageBoxDefaultButton.Button1);
                    }
                    else
                    {
                        if (f.Save())
                        {
                            if (AfterSave != null)
                                AfterSave(this, new EventArgs());
                        }
                    }
                }
            }

            }
        
        private void Access(string menu)
        {

            if (lbluser.Text.ToUpper() == "ADMIN")
            {
                Add = 1;
                delete = 1;
                update = 1;

                return;

            } Add = 0;
            delete = 0;
            update = 0;


            Gramboo.GRBConfig c = Gramboo.GRBConfig.Open();
            frmmenu frm = new frmmenu();
            int i = c.Login.UserId;
            Gramboo.DataController dc = new Gramboo.DataController();
            using (DataTable dt = (dc.GetData(new SqlCommand("SELECT MenuAdd,MenuUpdate,MenuDelete FROM GEN.UserAccess (" + i + ",'" + menu + "')")).Tables[0]))
            {
                if (dt.Rows.Count > 0)
                {

                    Add = Convert.ToInt32(dt.Rows[0]["MenuAdd"]);
                    update = Convert.ToInt32(dt.Rows[0]["MenuUpdate"]);
                    delete = Convert.ToInt32(dt.Rows[0]["MenuDelete"]);
                }
                else
                {
                    Add = 0;
                    delete = 0;
                    update = 0;
                }
            }
        }
        private void ToolStripClose_Click(object sender, EventArgs e)
        {
            if (DockPanel.ActiveDocument == null)
            {
                picClose_Click(sender, e);
                return;
            }
            if (DockPanel.ActiveDocument.GetType().BaseType == typeof(Gramboo.Controls.GrbForm))
            {
                ((Gramboo.Controls.GrbForm)DockPanel.ActiveDocument).Close();
            }
            else if (DockPanel.ActiveDocument.GetType().BaseType == typeof(Gramboo.Controls.GrbGridReport))
            {
                ((Gramboo.Controls.GrbGridReport)DockPanel.ActiveDocument).Close();
            }
        }
        private void ToolStripDelete_Click(object sender, EventArgs e)
        {
            if (DockPanel.ActiveDocument == null)
                return;
            if (DockPanel.ActiveDocument.GetType().BaseType == typeof(Gramboo.Controls.GrbForm))
            {
                Gramboo.Controls.GrbForm f = new Gramboo.Controls.GrbForm();
                f = ((Gramboo.Controls.GrbForm)DockPanel.ActiveDocument);
                Access((f.Tag == null ? "" : f.Tag).ToString());
                if (delete == 0)
                {
                    Gramboo.General.ShowMessage("You Don't Have Permission To Perform This Action", "Access Denied", MessageBoxIcon.Hand, MessageBoxButtons.OKCancel, MessageBoxDefaultButton.Button1);
                }
                else
                {
                    f.Delete();
                }
            }
        }

        private void toolStripModify_Click(object sender, EventArgs e)
        {
            if (DockPanel.ActiveDocument == null)
                return;
            if (DockPanel.ActiveDocument.GetType().BaseType == typeof(Gramboo.Controls.GrbForm))
            {
                ((Gramboo.Controls.GrbForm)DockPanel.ActiveDocument).Edit();
            }
        }

        private void ToolStripFind_Click(object sender, EventArgs e)
        {

            if (DockPanel.ActiveDocument == null)
                return;
            if (DockPanel.ActiveDocument.GetType().BaseType == typeof(Gramboo.Controls.GrbForm))
            {
                ((Gramboo.Controls.GrbForm)DockPanel.ActiveDocument).List();
            }
        }

        private void toolstripRefresh_Click(object sender, EventArgs e)
        {
            if (DockPanel.ActiveDocument == null)
                return;
            if (DockPanel.ActiveDocument.GetType().BaseType == typeof(Gramboo.Controls.GrbForm))
            {
                ((Gramboo.Controls.GrbForm)DockPanel.ActiveDocument).RefreshData();
            }
            else  if (DockPanel.ActiveDocument.GetType().BaseType == typeof(Gramboo.Controls.GrbGridReport))
            {
                ((Gramboo.Controls.GrbGridReport)DockPanel.ActiveDocument).RefreshData();
            }
        }

        private void toolStripPrint_Click(object sender, EventArgs e)
        {
            if (DockPanel.ActiveDocument == null)
                return;
            if (DockPanel.ActiveDocument.GetType().BaseType == typeof(Gramboo.Controls.GrbForm))
            {
                ((Gramboo.Controls.GrbForm)DockPanel.ActiveDocument).Print();
            }
        }

        private void lblCompanyName_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Gramboo.General.Setupcombo(dc, cmbCompany, "SYST.CompanyMaster", "Comp_Name", "Comp_id", "IsActive=1 AND Comp_Name!= '"+ lblCompanyName.Text + "'");
            cmbCompany.Visible = true;
        }


        private void DockPanel_ActiveContentChanged(object sender, EventArgs e)
         {


        }

        private void cmbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCompany.SelectedIndex > -1)
            {
                int branchid;
                 string  branchName;
                using (DataTable dt = dc.GetData(new System.Data.SqlClient.SqlCommand("Select top 1  BranchName, Branchid FROM  SYST.BranchMaster  WHERE IsActive=1 AND Company_id=" + cmbCompany.SelectedValue + "")).Tables[0])
                {
                    branchid =Convert.ToInt16( dt.Rows[0]["Branchid"]);
                    branchName = Convert.ToString(dt.Rows[0]["BranchName"]);
                    lblBranch.Text = branchName;
                    lblBranch.Tag = branchid;
                }

                  Gramboo.GRBConfig config;

                if (Gramboo.GRBConfig.Open() != null)
                {
                    config = Gramboo.GRBConfig.Open();
                }
                else
                {
                    config = new Gramboo.GRBConfig();
                }
                config.Login.CompanyID = Convert.ToInt32(cmbCompany.SelectedValue);
                config.Login.BranchId = branchid;
                config.Save();
                project.Classes.Common.SwitchCompany(Convert.ToInt16(cmbCompany.SelectedValue), Convert.ToInt16(lblBranch.Tag));
                lblCompanyName.Text = cmbCompany.Text;
            }
            cmbCompany.Visible = false;
        }

        private void backUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string BackupPath="";
            using (DataTable dt = dc.GetData(new System.Data.SqlClient.SqlCommand("Select * FROM  SYST.Settings  ")).Tables[0])
            {

               // BackupPath = Convert.ToString(dt.Rows[0]["BackupPath"]);

            }
            if (frmRemoteFolderDialouge.Show(ref dc.ConnectionProperties.ServerName, ref dc.ConnectionProperties.DBUsername, ref dc.ConnectionProperties.DBPassword,false) == DialogResult.OK)
            {

              

                BackupPath = frmRemoteFolderDialouge.Path;


                project.Classes.Common.BackUpOrRestore(BackupPath, dc.ConnectionProperties.ServerName, dc.ConnectionProperties.DBUsername, dc.ConnectionProperties.DBPassword, dc.ConnectionProperties.DatabseName, "Backup");
                 
            }



        }

        private void restoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string BackupPath;
            using (DataTable dt = dc.GetData(new System.Data.SqlClient.SqlCommand("Select * FROM  SYST.Settings  ")).Tables[0])
            {

                //  BackupPath = Convert.ToString(dt.Rows[0]["BackupPath"]);

            }
            if (frmRemoteFolderDialouge.Show(ref dc.ConnectionProperties.ServerName, ref dc.ConnectionProperties.DBUsername, ref dc.ConnectionProperties.DBPassword, true) == DialogResult.OK)
            {



                BackupPath = frmRemoteFolderDialouge.Path;

                project.Classes.Common.BackUpOrRestore(BackupPath, dc.ConnectionProperties.ServerName, dc.ConnectionProperties.DBUsername, dc.ConnectionProperties.DBPassword, dc.ConnectionProperties.DatabseName, "Restore");
            }
        }

        private void picUser_Click(object sender, EventArgs e)
        {
        //    JMS_RET.Forms.GENERALFORMS.ChangeUser c = new Forms.GENERALFORMS.ChangeUser();
        //    //c.Parent = this;
        //    c.Show();
        }



        private void PopulateMenus(TreeView treeView1, string MenuName = "")
        {
            Gramboo.GRBConfig c = Gramboo.GRBConfig.Open();

            TreeNode parentNode;
            string str = "";
            treeView1.Nodes.Clear();

            str = "Select * FROM [SYST].[VuserMenuGroup] WHERE [MenuName]='" + MenuName + "' and  ParentId=0 AND User_id=" + c.Login.UserId + " Order by MenuId";


            DataTable dt = dc.GetData(new SqlCommand(str), "MenuMaster").Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                parentNode = treeView1.Nodes.Add(dr["MenuName"].ToString());
                PopulateTreeView(Convert.ToInt32(dr["menuId"].ToString()), parentNode, treeView1, c.Login.UserId);
            }
            treeView1.ExpandAll();
        }
        private void PopulateTreeView(int parentId, TreeNode parentNode, TreeView treeView1, int user)
        {

            DataTable dtchildc = dc.GetData(new SqlCommand("Select * FROM [SYST].[VuserMenuGroup] Where ParentId=" + parentId + " AND User_id=" + user + " Order by MenuId"), "MenuMaster").Tables[0];
            TreeNode childNode;
            foreach (DataRow dr in dtchildc.Rows)
            {


                if (parentNode == null)
                    childNode = treeView1.Nodes.Add(dr["MenuName"].ToString());
                else
                    childNode = parentNode.Nodes.Add(dr["MenuName"].ToString());

                PopulateTreeView(Convert.ToInt32(dr["menuId"].ToString()), childNode, treeView1, user);
            }
        }


        private void picHideOrExpand_Click(object sender, EventArgs e)
        {
            if (IsTopExpanded)
            {
                foreach (ToolStripItem c in ToolStripMain.Items)
                {
                    c.DisplayStyle = ToolStripItemDisplayStyle.Text;
                     
                }
                ToolStripMain.Height = 25;
                FormBorderTop.Height= 50;
                pnlLoginDetails.Visible = false;
                picHideOrExpand.Image = project.Properties.Resources._1416416978_plus;
                IsTopExpanded = false;
            }
            else
            {
                foreach (ToolStripItem c in ToolStripMain.Items)
                {
                    c.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;

                }
                ToolStripMain.Height = 55;
                FormBorderTop.Height= 128;
                pnlLoginDetails.Visible = true;
                picHideOrExpand.Image = project.Properties.Resources.Untitled_1_copy;
                IsTopExpanded = true;
            }
        }

        private void FormBorderTop_Paint(object sender, PaintEventArgs e)
        {
            // Getting the graphics object
            Graphics g = e.Graphics;

            // Creating the rectangle for the gradient
            Rectangle rBackground = new Rectangle(0, 0,
                                      this.Width, this.Height);
         
            string str = "", Color1 = "", Color2 = "";
            
                    Color1 = "#2A13F2";
                    Color2 = "#030208";
            if (cmbCompany.Text  != null)
            {
                str = "SELECT Color1,Color2 FROM SYST.CompanyMaster WHERE Comp_name='" +lblCompanyName.Text + "'";
                DataTable dt = dc.GetData(new SqlCommand(str), "CompanyMaster").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    Color1 = dt.Rows[0]["Color1"].ToString();
                    Color2 = dt.Rows[0]["Color2"].ToString();

                }
                 
            }

            Color1 = (Color1.IndexOf("#") == -1 ? "#" + Color1 : Color1);
            Color2 = (Color2.IndexOf("#") == -1 ? "#" + Color2 : Color2);
            ColorConverter ccon = new ColorConverter();
            try
            {
                // Creating the lineargradient
                System.Drawing.Drawing2D.LinearGradientBrush bBackground
                    = new System.Drawing.Drawing2D.LinearGradientBrush(rBackground,
                                                     (Color)ColorTranslator.FromHtml(Color1), (Color)ColorTranslator.FromHtml(Color2), LinearGradientMode.Horizontal);

                // Draw the gradient onto the form
                g.FillRectangle(bBackground, rBackground);
            }
            catch (Exception ex)
            {
            }
        }

        private void picServer_Click(object sender, EventArgs e)
        {

        }

        private void lblProductName_Click(object sender, EventArgs e)
        {

        }

        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.F2)
            {
               ToolStripSave_Click(new object(),new EventArgs());  
            }
            else if (e.KeyCode == Keys.F9)
            {
                ToolStripNew_Click(new object(), new EventArgs());
            }
            else if (e.KeyCode == Keys.F3)
            {
                toolStripModify_Click(new object(), new EventArgs());  
            }
            else if (e.KeyCode == Keys.F4)
            {
                ToolStripDelete_Click(new object(), new EventArgs());
            }
            else if (e.KeyCode == Keys.F5)
            {
                toolStripPrint_Click(new object(), new EventArgs()); 
            }
            else if (e.KeyCode == Keys.F6)
            {
                toolstripRefresh_Click(new object(), new EventArgs()); 
            }
            else if (e.KeyCode == Keys.F7)
            {
                ToolStripClose_Click(new object(), new EventArgs()); 
            }
            else if (e.KeyCode == Keys.Z && e.Modifiers==Keys.Control )
            {
                Gramboo.General.Setupcombo(dc, cmbCompany, "SYST.CompanyMaster", "Comp_Name", "Comp_id", "IsActive=1 AND Comp_Name!= '" + lblCompanyName.Text + "'");
                if (lblCompanyName.Tag == "2")
                    cmbCompany.SelectedValue = 1;
                else
                    cmbCompany.SelectedValue = 2;

                FormBorderTop.Refresh();
            }
            else if (e.KeyCode == Keys.T && e.Modifiers == Keys.Control)
            {
                frmTransfer frm = new frmTransfer();
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.ShowDialog();

            }
            else if  (e.KeyCode == Keys.Delete && e.Modifiers == Keys.Control)
            {
                //project.Forms.SALE.FrmSalesDeletion frm = new project.Forms.SALE.FrmSalesDeletion();
                //frm.StartPosition = FormStartPosition.CenterScreen;
                //frm.ShowDialog();

            }
        }

        private void ToolStripMain_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
         

    }

}

