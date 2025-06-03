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
    public partial class admin4 : Form
    {
        public admin4()
        {
            InitializeComponent();
        }

        private void admin4_Load(object sender, EventArgs e)
        {
            Table();
            label2.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();//显示选中行的账号
            label3.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();//显示选中行的姓名
        }
        public void Table() //从数据库中导入用户表
        {
            dataGridView1.Rows.Clear();//先清空表格控件上的旧数据
            Dao dao = new Dao();
            string sql = $"select * from t_user;";
            IDataReader dc = dao.Read(sql);//执行sql查询语句，dc可以用来读取查询结果
            while (dc.Read())//逐行增加
            {
                dataGridView1.Rows.Add(dc[0].ToString(), dc[1].ToString(), dc[2].ToString(), dc[3].ToString());
            }
            dc.Close();
            dao.DaoClose();
        }

        private void button1_Click(object sender, EventArgs e)//修改信息按钮
        {
            try
            {
                //点击修改按钮后，默认将选中行的记录传入
#pragma warning disable CS8600 // 将 null 字面量或可能为 null 的值转换为非 null 类型。
                string id = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();//账号

#pragma warning disable CS8600 // 将 null 字面量或可能为 null 的值转换为非 null 类型。
                string name = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();//姓名
                string sex = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();//性别
                string psw = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();//密码

#pragma warning disable CS8604 // 引用类型参数可能为 null。
                admin41 admin = new admin41(id, name, sex,psw);//修改借书卡信息界面的有参构造函数，将选中行的数据传入

                admin.ShowDialog();//打开修改图书界面的窗口
            }
            catch
            {
                MessageBox.Show("出现错误！");
            }
        }

        private void button2_Click(object sender, EventArgs e)//刷新按钮
        {
            Table();
        }

        private void button3_Click(object sender, EventArgs e)//注销账户按钮
        {
            string id = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();//存入选中行的书号
            DialogResult dr = MessageBox.Show("确认注销该账户吗？", "信息提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);//询问消息框
            if (dr == DialogResult.OK)
            {
                try
                {
                    string sql = $"delete from t_user where id ='{id}'";
                    Dao dao = new Dao();
                    if (dao.Execute(sql) > 0)//如果受影响的行数大于0
                    {
                        MessageBox.Show("注销账户成功！");
                        Table();
                    }
                    else
                    {
                        MessageBox.Show("注销账户失败。");
                    }
                    dao.DaoClose();
                }
                catch
                {
                    MessageBox.Show("请先选中要注销的账户！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            label2.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();//显示选中行的账号
            label3.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();//显示选中行的姓名
        }
    
    }
}
