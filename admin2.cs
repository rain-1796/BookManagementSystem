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
    public partial class admin2 : Form
    {
        public admin2()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)//删除图书的按钮
        {
#pragma warning disable CS8600 // 将 null 字面量或可能为 null 的值转换为非 null 类型。
            string id = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();//存入选中行的书号
            string name = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();//存入选中行的书名
            label2.Text = name;//显示选中行的书名
            DialogResult dr = MessageBox.Show("确认删除吗？","信息提示",MessageBoxButtons.OKCancel,MessageBoxIcon.Question);//询问消息框
            if (dr == DialogResult.OK)
            {
                try
                {
                    string sql = $"delete from t_book where id ='{id}'";
                    Dao dao = new Dao();
                    if (dao.Execute(sql) > 0)//如果受影响的行数大于0
                    {
                        MessageBox.Show("删除成功！");
                        Table();
                        label2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();//重新显示选中行的书名
                    }
                    else
                    {
                        MessageBox.Show("删除失败。");
                    }
                    dao.DaoClose();
                }
                catch
                {
                    MessageBox.Show("请先选中要删除的图书！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void admin2_Load(object sender, EventArgs e)//当admin2窗体加载时，自动调用括号内的函数
        {
            Table();
            label2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();//显示选中行的书名
        }
        public void Table() //从数据库中导入图书表
        {
            dataGridView1.Rows.Clear();//先清空表格控件上的旧数据
            Dao dao = new Dao();
            string sql = $"select * from t_book a join t_kind b on a.kind_id = b.id;";
            IDataReader dc = dao.Read(sql);//执行sql查询语句，dc可以用来读取查询结果
            while (dc.Read())//逐行增加
            {
                dataGridView1.Rows.Add(dc[0].ToString(), dc[1].ToString(), dc[7].ToString(), dc[3].ToString(), dc[4].ToString(), dc[5].ToString());
            } 
            dc.Close();
            dao.DaoClose();
        }

        public void TableId() //书号查询的按钮
        {
            dataGridView1.Rows.Clear();//先清空表格控件上的旧数据
            Dao dao = new Dao();
            string sql = $"select * from t_book a join t_kind b on a.kind_id = b.id where a.id = '{textBox1.Text}';";
            IDataReader dc = dao.Read(sql);//执行sql查询语句，dc可以用来读取查询结果
            while (dc.Read())//逐行增加
            {
                dataGridView1.Rows.Add(dc[0].ToString(), dc[1].ToString(), dc[7].ToString(), dc[3].ToString(), dc[4].ToString(), dc[5].ToString());
            }
            dc.Close();
            dao.DaoClose();
        }

        public void TableName() //书名查询的按钮
        {
            dataGridView1.Rows.Clear();//先清空表格控件上的旧数据
            Dao dao = new Dao();
            string sql = $"select * from t_book a join t_kind b on a.kind_id = b.id where a.name like '%{textBox2.Text}%';";
            IDataReader dc = dao.Read(sql);//执行sql查询语句，dc可以用来读取查询结果
            while (dc.Read())//逐行增加
            {
                dataGridView1.Rows.Add(dc[0].ToString(), dc[1].ToString(), dc[7].ToString(), dc[3].ToString(), dc[4].ToString(), dc[5].ToString());
            }
            dc.Close();
            dao.DaoClose();
        }

        public void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)//添加图书的按钮
        {
            admin21 admin = new admin21();
            admin.ShowDialog();//打开添加图书界面的窗口
        }

        private void admin2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_Click(object sender, EventArgs e)//表格控件的点击事件
        {
            label2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();//显示选中行的书名
        }

       
        public void button5_Click(object sender, EventArgs e)//修改图书的按钮
        {
            
            try
            {
                //点击修改按钮后，默认将选中行的记录传入
                string beforeid = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();//书号
                

                string name = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();//书名

                string kind = "";//书名
                if (dataGridView1.SelectedRows[0].Cells[2].Value.ToString() == "文学")
                {
                    kind = "1.文学";
                }
                else if(dataGridView1.SelectedRows[0].Cells[2].Value.ToString() == "科幻")
                {
                    kind = "2.科幻";
                }
                else if (dataGridView1.SelectedRows[0].Cells[2].Value.ToString() == "历史")
                {
                    kind = "3.历史";
                }
                else if (dataGridView1.SelectedRows[0].Cells[2].Value.ToString() == "小说")
                {
                    kind = "4.小说";
                }
                else
                {
                    kind = "5.传记";
                }

                string author = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();//作者
                string press = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();//出版社
                string number = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();//库存
#pragma warning disable CS8604 // 引用类型参数可能为 null。
                admin22 admin = new admin22(beforeid, name,kind,author,press,number);//修改图书界面的有参构造函数，将选中行的数据传入
                admin.ShowDialog();//打开修改图书界面的窗口
            }
            catch
            {
                MessageBox.Show("出现错误！");
            }
        }


        private void button2_Click(object sender, EventArgs e)//书号查询
        {
            TableId();
        }

        private void button6_Click(object sender, EventArgs e)//书名模糊查询
        {
            TableName();
        }

        private void button3_Click(object sender, EventArgs e)//刷新按钮
        {
            Table();//重新导入图书表
            label2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();//显示选中行的书名
            textBox1.Text = "";//清空已经输入的查询数据
            textBox2.Text = "";
        }

        private void button7_Click(object sender, EventArgs e)
        {
           
        }

        private void button8_Click(object sender, EventArgs e)
        {
          
        }

        private void button9_Click(object sender, EventArgs e)
        {
           
        }

        private void button7_Click_1(object sender, EventArgs e)//数据备份按钮
        {
            //先检查数据库bookdb中有无t_backup表
            string sql1 = $" show tables like '%t_backup%';"; 
            Dao dao = new Dao();
            IDataReader dc = dao.Read(sql1);//执行sql查询语句，dc可以用来读取查询结果

            if (dc.Read())
            {
                MessageBox.Show("您已经存在一个数据备份");
            }
            else
            {
                string sql = $"create table t_backup as select * from t_book;";
                dao.Execute(sql);
                MessageBox.Show("当前数据已备份！");
            }
        }

        private void button8_Click_1(object sender, EventArgs e)//数据恢复按钮
        {
            //先检查数据库bookdb中有无t_backup表
            string sql1 = $" show tables like '%t_backup%';";
            Dao dao = new Dao();
            IDataReader dc = dao.Read(sql1);//执行sql查询语句，dc可以用来读取查询结果

            if (dc.Read())
            {
                string sql = $"drop table if exists t_book;create table t_book as select* from t_backup;drop table if exists t_backup;";
                dao.Execute(sql);
                MessageBox.Show("数据恢复成功");
                Table();
            }
            else
            {
                MessageBox.Show("您当前没有备份数据！");
            }
            
        }

        private void button9_Click_1(object sender, EventArgs e)//删除备份按钮
        {
            //先检查有无可删除的备份
            string sql1 = $" show tables like '%t_backup%';";
            Dao dao = new Dao();
            IDataReader dc = dao.Read(sql1);//执行sql查询语句，dc可以用来读取查询结果

            if (dc.Read())
            {
                string sql = $"drop table if exists t_backup;";
                dao.Execute(sql);
                MessageBox.Show("删除备份成功！");
            }
            else
            {
                MessageBox.Show("没有可删除的备份！");
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
