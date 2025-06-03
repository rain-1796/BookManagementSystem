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
        string admin_id = "";
        public admin1(string id)
        {
            admin_id = id;
            InitializeComponent();
            label1.Text = $"欢迎管理员{admin_id}登录";
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
            
            login login1 = new login();
            this.Hide();
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

        private void 删除本地密码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("是否要将本地密码删除？", "删除本地密码", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                bool isDeleted = DeleteAccount(admin_id);

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
