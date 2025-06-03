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
    public partial class user3 : Form
    {
        public user3()
        {
            InitializeComponent();
            Table();

            //预约时间超过5天自动取消预约
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
#pragma warning disable CS8600 // 将 null 字面量或可能为 null 的值转换为非 null 类型。
                string no = row.Cells[0].Value.ToString();//获取借书编号
                // 获取借书状态
                string Status = row.Cells[7].Value.ToString();  // 获取第 8 列（索引 7）的状态

#pragma warning disable CS8604 // 引用类型参数可能为 null。
                DateTime ReverseTime = DateTime.Parse(row.Cells[3].Value.ToString());// 获取预约时间
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
                string no = row.Cells[0].Value.ToString();//获取借书编号
                // 获取借书状态
                string Status = row.Cells[7].Value.ToString();  // 获取第 8 列（索引 7）的状态
                if (row.Cells[5]?.Value == null || string.IsNullOrWhiteSpace(row.Cells[5].Value.ToString()))
                {
                    continue; // 跳过无效行
                }

#pragma warning disable CS8604 // 引用类型参数可能为 null。
                DateTime ShouldReturnTime = DateTime.Parse(row.Cells[5].Value.ToString());// 获取应还书时间
                

                // 判断是否超期
                string Overdue = "未超期";  // 默认是未超期

                if (Status == "已借书" && DateTime.Now > ShouldReturnTime)
                {
                    Overdue = "已超期";  // 当前时间大于应还书时间
                }
                else if(Status == "已借书" && DateTime.Now < ShouldReturnTime)
                {
                    Overdue = "未超期";  // 当前时间小于应还书时间
                }
                else { }
                string sql = $"update t_lend set overdue = '{Overdue}' where no = '{no}' ;";
                Dao dao = new Dao();
                dao.Execute(sql);

                Table();
            }
        }
        public void Table()
        {
            dataGridView1.Rows.Clear();//清空旧数据
            Dao dao = new Dao();
            string sql = $"select no,bid,bname,reserve_time,borrow_time,should_return_time,return_time,status,overdue from t_lend where uid = {Data.UID} order by no;";
            IDataReader dc = dao.Read(sql);
            while (dc.Read())
            {
                dataGridView1.Rows.Add(dc[0].ToString(), dc[1].ToString(), dc[2].ToString(), dc[3].ToString(), dc[4].ToString(), dc[5].ToString(), dc[6].ToString(), dc[7].ToString(), dc[8].ToString());
            }

            dc.Close();
            dao.DaoClose();
        }



        private void user3_Load(object sender, EventArgs e)
        {
            if(dataGridView1.Rows.Count == 0)
            {
                label2.Text = "";
            }
            else
            {
                label2.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();//显示选中行的书名
            }
            
        }

        private void button1_Click(object sender, EventArgs e)//归还图书的按钮，已弃用
        {
            
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            label2.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();//显示选中行的书名
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string cancel = "取消预约";
            string no = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            string bid = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            if (dataGridView1.SelectedRows[0].Cells[7].Value.ToString() == "已预约")
            {
                string sql = $"update t_lend set status = '{cancel}' where no = '{no}';update t_book set number = number + 1 where id = '{bid}';";
                Dao dao = new Dao();
                dao.Execute(sql);
                MessageBox.Show("取消预约成功！");

                Table();

            }
            else
            {
                MessageBox.Show("您未预约！");
            }
        }
    }
}
