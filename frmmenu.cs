using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI;
using WeifenLuo.WinFormsUI.Docking;

namespace project
{
    public partial class frmmenu :DockContent
    {
        public frmmenu()
        {
            InitializeComponent();
        }

        //private Int32 m_ChildFormNumber = 0;
        //MOD.general g = new MOD.general();
      
       
        private void frmmenu_Load(object sender, EventArgs e)
        {

        }

        private void frmmenu_Load_1(object sender, EventArgs e)
        {
            TreeView_Transaction.ExpandAll();
            treeView_reports.ExpandAll();
            TreeView_Master.ExpandAll();
            TreeView_Add_new.ExpandAll();



        }
       private void TreeView_Master_AfterSelect_1(object sender, TreeViewEventArgs e)
        {
            if (e.Node == null)
                return;
            Classes.MenuControl.LoadForm(TreeView_Master.SelectedNode.Text, "Master Files", TreeView_Master, this.ParentForm);

            TreeView_Master.SelectedNode = TreeView_Master.Nodes[0];
        }
        private void TreeView_Transaction_AfterSelect_2(object sender, TreeViewEventArgs e)
        {
            Classes.MenuControl.LoadForm(TreeView_Transaction.SelectedNode.Text, "Transactions", TreeView_Transaction, this.ParentForm);

            TreeView_Transaction.SelectedNode = TreeView_Transaction.Nodes[0];   
        }
        
        private void treeView_reports_AfterSelect(object sender, TreeViewEventArgs e)
        {
            Classes.MenuControl.LoadForm(treeView_reports.SelectedNode.Text, "Reports", treeView_reports, this.ParentForm);

            treeView_reports.SelectedNode = treeView_reports.Nodes[0];

        }

        private void TreeView_Transaction_AfterSelect_1(object sender, TreeViewEventArgs e)
        {
            
        }

        private void TreeView_Master_AfterSelect(object sender, TreeViewEventArgs e)
        {

           
        }


        public void ShowForm(DockContent Form,string menuname)
        {
            Form.MdiParent = this.ParentForm;

            ((frmMain)this.ParentForm).ShowChild(Form);
            Form.Focus();
            Form.Tag = menuname;
        }

        private void ShowForm(Form Form, string menuname)
        {
            Form.MdiParent = this.ParentForm;
            Form.Show();
            Form.Focus();
            Form.Tag = menuname;

        }

        private void TreeView_Transaction_AfterSelect(object sender, TreeViewEventArgs e)
        {



            Classes.MenuControl.LoadForm(TreeView_Transaction.SelectedNode.Text, "Transactions", TreeView_Transaction,this);

        
            TreeView_Transaction.SelectedNode = TreeView_Transaction.Nodes[0];

        }

        private void treeview_company_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            Classes.MenuControl.LoadForm(TreeView_Transaction.SelectedNode.Text, "Transactions", TreeView_Transaction, this);

            treeView_reports.SelectedNode = treeView_reports.Nodes[0];
        }

        private void Band_Master_Click(object sender, EventArgs e)
        {

        }

        private void Band_Master_Click_1(object sender, EventArgs e)
        {

        }

        private void Band_Transaction_Click(object sender, EventArgs e)
        {

        }

        private void Band_Reports_Click(object sender, EventArgs e)
        {

        }

        private void TreeView_Master_AfterCheck(object sender, TreeViewEventArgs e)
        {

        }

        private void Band_Reports_Click_1(object sender, EventArgs e)
        {

        }

        private void Band_Master_Click_2(object sender, EventArgs e)
        {

        }

        private void Band_Transaction_Click_1(object sender, EventArgs e)
        {

        }

        private void Band_Master_Click_3(object sender, EventArgs e)
        {

        }

        private void TreeView_Add_new_AfterSelect(object sender, TreeViewEventArgs e)
        {
            Classes.MenuControl.LoadForm(TreeView_Add_new.SelectedNode.Text, "Add_New", TreeView_Add_new, this.ParentForm);

            TreeView_Add_new.SelectedNode = TreeView_Add_new.Nodes[0];
        }
    }
}

  