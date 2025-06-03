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
    public partial class admin5 : Form
    {
        public admin5()
        {
            InitializeComponent();
            Table();

            //预约时间超过5天自动取消预约
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
#pragma warning disable CS8600 // 将 null 字面量或可能为 null 的值转换为非 null 类型。
                string no = row.Cells[0].Value.ToString();//获取借书编号
                // 获取借书状态
                string Status = row.Cells[8].Value.ToString();  // 获取第 8 列（索引 7）的状态

#pragma warning disable CS8604 // 引用类型参数可能为 null。
                DateTime ReverseTime = DateTime.Parse(row.Cells[4].Value.ToString());// 获取预约时间
                DateTime NewTime = ReverseTime.AddDays(5); // 加上 5 天
                if (Status == "已预约" && DateTime.Now > NewTime)
                {
                    Status = "取消预约";  // 当前时间大于预约时间+5天
                }
                else { }
                string sql = $"update t_lend set status = '{Status}' where no = '{no}' ;";
                Dao dao = new Dao();
                dao.Execute(sql);
            }



            //判断借书是否超期
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {

#pragma warning disable CS8600 // 将 null 字面量或可能为 null 的值转换为非 null 类型。
                string no = row.Cells[0].Value.ToString();//获取书号
                // 获取借书状态
                string Status = row.Cells[8].Value.ToString();  // 获取第 8 列（索引 7）的状态
                if (row.Cells[6]?.Value == null || string.IsNullOrWhiteSpace(row.Cells[5].Value.ToString()))
                {
                    continue; // 跳过无效行
                }

#pragma warning disable CS8604 // 引用类型参数可能为 null。
                DateTime ShouldReturnTime = DateTime.Parse(row.Cells[6].Value.ToString());// 获取应还书时间


                // 判断是否超期
                string Overdue = "未超期";  // 默认是未超期
                if (Status == "已借书" && DateTime.Now > ShouldReturnTime)
                {
                    Overdue = "已超期";  // 当前时间大于应还书时间
                }
                else if (Status == "已借书" && DateTime.Now < ShouldReturnTime)
                {
                    Overdue = "未超期";  // 当前时间小于应还书时间
                }
                else if (Status == "已还书" && DateTime.Now > ShouldReturnTime)
                {
                    Overdue = "已超期";  // 当前时间大于应还书时间
                }
                else { }
                string sql = $"update t_lend set overdue = '{Overdue}'where no = '{no}' ;";
                Dao dao = new Dao();
                dao.Execute(sql);

                Table();
            }
        }

        public void Table()
        {
            dataGridView1.Rows.Clear();//清空旧数据
            Dao dao = new Dao();
            string sql = $"select * from t_lend order by no;";
            IDataReader dc = dao.Read(sql);
            while (dc.Read())
            {
                dataGridView1.Rows.Add(dc[0].ToString(), dc[1].ToString(), dc[2].ToString(), dc[3].ToString(), dc[4].ToString(), dc[5].ToString(), dc[6].ToString(), dc[7].ToString(), dc[8].ToString(), dc[9].ToString());
            }
            dc.Close();
            dao.DaoClose();
        }

        private void admin5_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)//确认借书按钮
        {
            if (dataGridView1.SelectedRows[0].Cells[8].Value.ToString() == "已预约")
            {
#pragma warning disable CS8600 // 将 null 字面量或可能为 null 的值转换为非 null 类型。
                string no = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                string uid = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                string bname = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();

                DialogResult result = MessageBox.Show("是否将这本书借出？", "确认借书", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    string borrow_book = "已借书";
                    string overdue = "未超期";

                    string sql1 = $"update t_lend set borrow_time = now(),should_return_time = DATE_ADD(borrow_time, INTERVAL 30 DAY),status = '{borrow_book}',overdue = '{overdue}' where no = '{no}';";
                    Dao dao = new Dao();
                    if (dao.Execute(sql1) > 0)//至少会有一行受到影响
                    {
                        MessageBox.Show($"用户{uid}借出了图书：{bname}");
                        Table();
                    }
                }
            }
            else
            {
                MessageBox.Show("您未预约！");
            }
        }

        private void button2_Click(object sender, EventArgs e)//确认还书按钮
        {
            if (dataGridView1.SelectedRows[0].Cells[8].Value.ToString() == "已借书")
            {
#pragma warning disable CS8600 // 将 null 字面量或可能为 null 的值转换为非 null 类型。
                string no = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                string uid = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                string bid = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                string bname = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();

                DialogResult result = MessageBox.Show("是否将这本书归还？", "确认还书", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    string return_book = "已还书";

                    string sql1 = $"update t_lend set return_time = now(),status = '{return_book}' where no = '{no}';update t_book set number = number + 1 where id = '{bid}';";
                    Dao dao = new Dao();
                    if (dao.Execute(sql1) > 0)//至少会有一行受到影响
                    {
                        MessageBox.Show($"用户{uid}归还了图书：{bname}");
                        Table();
                    }

                }
            }
            else
            {
                MessageBox.Show("您未借书！");
            }


        }

        private void button3_Click(object sender, EventArgs e)//账号查询按钮
        {
            dataGridView1.Rows.Clear();//先清空表格控件上的旧数据
            string sql = $"select * from t_lend where uid = '{textBox1.Text}';";
           
            Dao dao = new Dao();
            IDataReader dc = dao.Read(sql);//执行sql查询语句，dc可以用来读取查询结果
            while (dc.Read())//逐行增加
            {
               dataGridView1.Rows.Add(dc[0].ToString(), dc[1].ToString(), dc[2].ToString(), dc[3].ToString(), dc[4].ToString(), dc[5].ToString(), dc[6].ToString(), dc[7].ToString(), dc[8].ToString(), dc[9].ToString());
            }
            dc.Close();
            dao.DaoClose();

        }

        private void button4_Click(object sender, EventArgs e)//已超期查询按钮
        {
            string Overdue = "已超期";
            dataGridView1.Rows.Clear();//先清空表格控件上的旧数据
            string sql = $"select * from t_lend where overdue = '{Overdue}';";

            Dao dao = new Dao();
            IDataReader dc = dao.Read(sql);//执行sql查询语句，dc可以用来读取查询结果
            while (dc.Read())//逐行增加
            {
                dataGridView1.Rows.Add(dc[0].ToString(), dc[1].ToString(), dc[2].ToString(), dc[3].ToString(), dc[4].ToString(), dc[5].ToString(), dc[6].ToString(), dc[7].ToString(), dc[8].ToString(), dc[9].ToString());
            }
            dc.Close();
            dao.DaoClose();
        }

        private void button5_Click(object sender, EventArgs e)//刷新按钮
        {
            Table();
        }
    }
}
