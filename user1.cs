using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BOOKMS
{
    public partial class user1 : Form
    {
        public user1()
        {
            InitializeComponent();
            label1.Text = $"欢迎用户{Data.UName}登录";
        }

        private void user1_Load(object sender, EventArgs e)
        {

        }

        private void 图书借阅ToolStripMenuItem_Click(object sender, EventArgs e)//图书查看和借阅按钮
        {
            user2 user = new user2();
            user.ShowDialog();//打开图书借阅界面的窗口 
        }

        private void 图书归还ToolStripMenuItem_Click(object sender, EventArgs e)//图书归还按钮
        {
            user3 user = new user3();
            user.ShowDialog();//打开图书归还界面的窗口
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)//系统-退出按钮
        {
            this.Close();//关闭当前窗口
            login login1 = new login();
            login1.ShowDialog();
        }
    }
}
