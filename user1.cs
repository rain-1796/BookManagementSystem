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
        string user_id = Data.UID;
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
            login login1 = new login();
            this.Hide();
            login1.ShowDialog();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void 修改密码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string id = Data.UID.ToString(); 
            user4 user = new user4(id);
            user.ShowDialog();
        }

        private void 删除本地密码ToolStripMenuItem_Click(object sender, EventArgs e)//删除本地密码
        {
            DialogResult result = MessageBox.Show("是否要将本地密码删除？", "删除本地密码", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                bool isDeleted = DeleteAccount(user_id);

                if (isDeleted)
                {
                    MessageBox.Show("密码删除成功！");
                }
                else
                {
                    MessageBox.Show("未找到该账号，删除失败！");
                }
            }
        }

        private bool DeleteAccount(string id)//删除保存的旧密码函数
        {
            string filePath = "D:\\Password\\password.txt";  // 文件路径
            var lines = File.ReadAllLines(filePath).ToList();  // 读取所有行

            // 查找并删除匹配账号的行
            bool isDeleted = false;//默认为false

            // 使用 .RemoveAll() 方法从列表中删除匹配的账号
            lines.RemoveAll(line => line.StartsWith(id + " "));

            // 如果删除成功，保存文件并返回 true
            if (lines.Count != File.ReadAllLines(filePath).Length)  // 如果行数变化，表示删除成功
            {
                File.WriteAllLines(filePath, lines);  // 保存修改后的内容回文件
                isDeleted = true;
            }

            return isDeleted;
        }
    }
}
