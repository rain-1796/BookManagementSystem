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
    public partial class admin1 : Form
    {
        public admin1()
        {
            InitializeComponent();
        }

        private void admin1_Load(object sender, EventArgs e)
        {

        }

        private void 图书管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            admin2 admin = new admin2();//实例化图书管理界面的对象
            admin.ShowDialog();//打开图书管理界面的窗口
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)//系统-退出
        {
            this.Close();//关闭当前窗口
            login login1 = new login();
            login1.ShowDialog();
        }

        private void 办理借书卡ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            admin3 admin = new admin3();
            admin.ShowDialog();//打开图书管理界面的窗口
        }

        private void 查看借书卡信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            admin4 admin = new admin4();
            admin.ShowDialog();//打开借书卡信息窗口
        }

        private void 查看借书记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            admin5 admin = new admin5();
            admin.ShowDialog();
        }
    }
}
