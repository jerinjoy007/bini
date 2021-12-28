using Gramboo.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace project
{
    public partial class FavoriteMenu : Gramboo.Controls.GrbForm
    {



        private static FavoriteMenu instance;
        private bool updatingTreeView;
        DataTable dt;
        public static FavoriteMenu Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FavoriteMenu();
                }
                else if (instance.IsDisposed)
                {
                    instance = new FavoriteMenu();
                }
                return instance;
            }
        }
        public FavoriteMenu()
        {
            InitializeComponent();
        }


        public override void RefreshData()
        {
            base.RefreshData();
             //Gramboo.General.Setupcombo(Cmb_ParentName, "SYST.Menumaster", "MenuName", "Parentid", "IsActive='True' AND  Parentid=0");
            //PopulateFavouriteMenus(); 
        }

        public override void Init()
        {
            base.Init();
        }



       

        private void Cmb_ParentName_SelectedValueChanged(object sender, EventArgs e)
        {
            //if (Cmb_ParentName.SelectedValue != null)
            //    PopulateFavouriteMenus(Cmb_ParentName.Text);
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

               if (e.Node == null)
                return;
               Classes.MenuControl.LoadForm(treeview1.SelectedNode.Text, GetTopNode(e.Node, treeview1).Text, treeview1, this.ParentForm);
               treeview1.SelectedNode = treeview1.Nodes[0];
        }








        private TreeNode GetTopNode(TreeNode node, TreeView treeview)
        {


            if (node.Parent != null)
                return GetTopNode(node.Parent, treeview);
            else
                return node;

        }


    }
}
