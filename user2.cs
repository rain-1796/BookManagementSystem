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
    public partial class user2 : Form
    {
        public user2()
        {
            InitializeComponent();
            Table();//导入图书表

        }

        private void user2_Load(object sender, EventArgs e)
        {
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

        private void button1_Click(object sender, EventArgs e)//预约图书按钮
        {
#pragma warning disable CS8600 // 将 null 字面量或可能为 null 的值转换为非 null 类型。
            string id = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();//获取书号
            string name = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();//获取书名
#pragma warning disable CS8604 // 引用类型参数可能为 null。
            int number=int.Parse(dataGridView1.SelectedRows[0].Cells[5].Value.ToString());//获取库存
            string reserve_book = "已预约";
            if (number < 1)
            {
                MessageBox.Show("库存不足！");
            }
            else
            {
                string sql = $"insert into t_lend(uid,bid,bname,reserve_time,status) values({Data.UID},'{id}','{name}',now(),'{reserve_book}');update t_book set number = number - 1 where id = '{id}';";
                Dao dao = new Dao();
                if (dao.Execute(sql) > 1)//两张表各执行一条sql语句，至少会有两行受到影响
                {
                    MessageBox.Show($"用户{Data.UName}预约了图书：《{name}》");
                    Table();
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_Click(object sender, EventArgs e)//表格控件的点击事件
        {
            label2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();//显示选中行的书名
        }

        private void button6_Click(object sender, EventArgs e)
        {
            TableName();
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
    }
}
