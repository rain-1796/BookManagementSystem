namespace BOOKMS
{
    partial class admin1
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.系统ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除本地密码ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.图书管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.办理借书卡ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.查看借书卡信息ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.查看借书记录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.系统ToolStripMenuItem,
            this.图书管理ToolStripMenuItem,
            this.办理借书卡ToolStripMenuItem,
            this.查看借书卡信息ToolStripMenuItem,
            this.查看借书记录ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 32);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 系统ToolStripMenuItem
            // 
            this.系统ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.删除本地密码ToolStripMenuItem,
            this.退出ToolStripMenuItem});
            this.系统ToolStripMenuItem.Name = "系统ToolStripMenuItem";
            this.系统ToolStripMenuItem.Size = new System.Drawing.Size(62, 28);
            this.系统ToolStripMenuItem.Text = "系统";
            // 
            // 删除本地密码ToolStripMenuItem
            // 
            this.删除本地密码ToolStripMenuItem.Name = "删除本地密码ToolStripMenuItem";
            this.删除本地密码ToolStripMenuItem.Size = new System.Drawing.Size(218, 34);
            this.删除本地密码ToolStripMenuItem.Text = "删除本地密码";
            this.删除本地密码ToolStripMenuItem.Click += new System.EventHandler(this.删除本地密码ToolStripMenuItem_Click);
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(218, 34);
            this.退出ToolStripMenuItem.Text = "退出";
            this.退出ToolStripMenuItem.Click += new System.EventHandler(this.退出ToolStripMenuItem_Click);
            // 
            // 图书管理ToolStripMenuItem
            // 
            this.图书管理ToolStripMenuItem.Name = "图书管理ToolStripMenuItem";
            this.图书管理ToolStripMenuItem.Size = new System.Drawing.Size(98, 28);
            this.图书管理ToolStripMenuItem.Text = "图书管理";
            this.图书管理ToolStripMenuItem.Click += new System.EventHandler(this.图书管理ToolStripMenuItem_Click);
            // 
            // 办理借书卡ToolStripMenuItem
            // 
            this.办理借书卡ToolStripMenuItem.Name = "办理借书卡ToolStripMenuItem";
            this.办理借书卡ToolStripMenuItem.Size = new System.Drawing.Size(98, 28);
            this.办理借书卡ToolStripMenuItem.Text = "注册账户";
            this.办理借书卡ToolStripMenuItem.Click += new System.EventHandler(this.办理借书卡ToolStripMenuItem_Click);
            // 
            // 查看借书卡信息ToolStripMenuItem
            // 
            this.查看借书卡信息ToolStripMenuItem.Name = "查看借书卡信息ToolStripMenuItem";
            this.查看借书卡信息ToolStripMenuItem.Size = new System.Drawing.Size(134, 28);
            this.查看借书卡信息ToolStripMenuItem.Text = "查看用户信息";
            this.查看借书卡信息ToolStripMenuItem.Click += new System.EventHandler(this.查看借书卡信息ToolStripMenuItem_Click);
            // 
            // 查看借书记录ToolStripMenuItem
            // 
            this.查看借书记录ToolStripMenuItem.Name = "查看借书记录ToolStripMenuItem";
            this.查看借书记录ToolStripMenuItem.Size = new System.Drawing.Size(134, 28);
            this.查看借书记录ToolStripMenuItem.Text = "查看借阅记录";
            this.查看借书记录ToolStripMenuItem.Click += new System.EventHandler(this.查看借书记录ToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("华文行楷", 26F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(174, 174);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(388, 55);
            this.label1.TabIndex = 1;
            this.label1.Text = "欢迎管理员登录";
            // 
            // admin1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "admin1";
            this.Text = "管理员操作界面";
            this.Load += new System.EventHandler(this.admin1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem 系统ToolStripMenuItem;
        private ToolStripMenuItem 图书管理ToolStripMenuItem;
        private Label label1;
        private ToolStripMenuItem 退出ToolStripMenuItem;
        private ToolStripMenuItem 办理借书卡ToolStripMenuItem;
        private ToolStripMenuItem 查看借书卡信息ToolStripMenuItem;
        private ToolStripMenuItem 查看借书记录ToolStripMenuItem;
        private ToolStripMenuItem 删除本地密码ToolStripMenuItem;
    }
}