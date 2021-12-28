namespace project
{
    partial class FavoriteMenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.treeview1 = new project.Classes.GrbTreeview();
            this.SuspendLayout();
            // 
            // txtModifiedDate
            // 
            this.txtModifiedDate.Location = new System.Drawing.Point(300, 338);
            // 
            // txtcreatedDate
            // 
            this.txtcreatedDate.Location = new System.Drawing.Point(323, 336);
            // 
            // txtcompId
            // 
            this.txtcompId.Location = new System.Drawing.Point(337, 338);
            // 
            // txtModuserID
            // 
            this.txtModuserID.Location = new System.Drawing.Point(337, 312);
            // 
            // txtCrUserId
            // 
            this.txtCrUserId.Location = new System.Drawing.Point(300, 312);
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(323, 312);
            // 
            // txtCounterId
            // 
            this.txtCounterId.Location = new System.Drawing.Point(323, 364);
            // 
            // txtBranchID
            // 
            this.txtBranchID.Location = new System.Drawing.Point(326, 364);
            // 
            // treeview1
            // 
            this.treeview1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeview1.Location = new System.Drawing.Point(0, 0);
            this.treeview1.Name = "treeview1";
            this.treeview1.Size = new System.Drawing.Size(293, 503);
            this.treeview1.TabIndex = 18;
            this.treeview1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // FavoriteMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(293, 503);
            this.CloseButton = false;
            this.CloseButtonVisible = false;
            this.Controls.Add(this.treeview1);
            this.Name = "FavoriteMenu";
            this.Text = "Favorite Menu ";
            this.Controls.SetChildIndex(this.treeview1, 0);
            this.Controls.SetChildIndex(this.txtUserName, 0);
            this.Controls.SetChildIndex(this.txtCrUserId, 0);
            this.Controls.SetChildIndex(this.txtModuserID, 0);
            this.Controls.SetChildIndex(this.txtcompId, 0);
            this.Controls.SetChildIndex(this.txtcreatedDate, 0);
            this.Controls.SetChildIndex(this.txtModifiedDate, 0);
            this.Controls.SetChildIndex(this.txtBranchID, 0);
            this.Controls.SetChildIndex(this.txtCounterId, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal Classes.GrbTreeview treeview1;


    }
}
