namespace Reddit_View
{
    partial class RedditView
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RedditView));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuStrip3 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.quitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.removeBordersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addBordersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.switchSubToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rfunnyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rpicsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.rawwToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.contextMenuStrip3.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(600, 418);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.quitToolStripMenuItem,
            this.removeBordersToolStripMenuItem,
            this.addBordersToolStripMenuItem,
            this.switchSubToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(161, 92);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(61, 4);
            // 
            // contextMenuStrip3
            // 
            this.contextMenuStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.quitToolStripMenuItem1});
            this.contextMenuStrip3.Name = "contextMenuStrip3";
            this.contextMenuStrip3.Size = new System.Drawing.Size(98, 26);
            // 
            // quitToolStripMenuItem1
            // 
            this.quitToolStripMenuItem1.Name = "quitToolStripMenuItem1";
            this.quitToolStripMenuItem1.Size = new System.Drawing.Size(97, 22);
            this.quitToolStripMenuItem1.Text = "Quit";
            this.quitToolStripMenuItem1.Click += new System.EventHandler(this.quitToolStripMenuItem1_Click);
            // 
            // removeBordersToolStripMenuItem
            // 
            this.removeBordersToolStripMenuItem.Name = "removeBordersToolStripMenuItem";
            this.removeBordersToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.removeBordersToolStripMenuItem.Text = "Remove Borders";
            this.removeBordersToolStripMenuItem.Click += new System.EventHandler(this.removeBordersToolStripMenuItem_Click);
            // 
            // addBordersToolStripMenuItem
            // 
            this.addBordersToolStripMenuItem.Name = "addBordersToolStripMenuItem";
            this.addBordersToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.addBordersToolStripMenuItem.Text = "Add Borders";
            this.addBordersToolStripMenuItem.Click += new System.EventHandler(this.addBordersToolStripMenuItem_Click);
            // 
            // switchSubToolStripMenuItem
            // 
            this.switchSubToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.rfunnyToolStripMenuItem,
            this.rpicsToolStripMenuItem,
            this.rToolStripMenuItem,
            this.rawwToolStripMenuItem});
            this.switchSubToolStripMenuItem.Name = "switchSubToolStripMenuItem";
            this.switchSubToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.switchSubToolStripMenuItem.Text = "Switch Sub";
            // 
            // rfunnyToolStripMenuItem
            // 
            this.rfunnyToolStripMenuItem.Name = "rfunnyToolStripMenuItem";
            this.rfunnyToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.rfunnyToolStripMenuItem.Text = "/r/funny";
            this.rfunnyToolStripMenuItem.Click += new System.EventHandler(this.rfunnyToolStripMenuItem_Click);
            // 
            // rpicsToolStripMenuItem
            // 
            this.rpicsToolStripMenuItem.Name = "rpicsToolStripMenuItem";
            this.rpicsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.rpicsToolStripMenuItem.Text = "/r/pics";
            this.rpicsToolStripMenuItem.Click += new System.EventHandler(this.rpicsToolStripMenuItem_Click);
            // 
            // rToolStripMenuItem
            // 
            this.rToolStripMenuItem.Name = "rToolStripMenuItem";
            this.rToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.rToolStripMenuItem.Text = "/r/WTF";
            this.rToolStripMenuItem.Click += new System.EventHandler(this.rToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem1.Text = "Frontpage";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // rawwToolStripMenuItem
            // 
            this.rawwToolStripMenuItem.Name = "rawwToolStripMenuItem";
            this.rawwToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.rawwToolStripMenuItem.Text = "/r/aww";
            this.rawwToolStripMenuItem.Click += new System.EventHandler(this.rawwToolStripMenuItem_Click);
            // 
            // RedditView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(624, 442);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RedditView";
            this.Text = "RedditView";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMenuStrip3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip3;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem removeBordersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addBordersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem switchSubToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem rfunnyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rpicsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rawwToolStripMenuItem;
    }
}

