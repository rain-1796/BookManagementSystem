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
        }
        public void Table()
        {
            dataGridView1.Rows.Clear();//清空旧数据
            Dao dao = new Dao();
            string sql = $"select no,bid,bname,datetime,overtime from t_lend where uid = {Data.UID} order by no;";
            IDataReader dc = dao.Read(sql);
            while (dc.Read())
            {
                dataGridView1.Rows.Add(dc[0].ToString(), dc[1].ToString(), dc[2].ToString(), dc[3].ToString(), dc[4].ToString());
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

        private void button1_Click(object sender, EventArgs e)//归还图书的按钮
        {
            string sql1 = $"select * from t_lend where uid = {Data.UID};";//还书之前先判断t_lend表中有无数据
            Dao dao1 = new Dao();
            IDataReader dc = dao1.Read(sql1);//执行sql查询语句，dc可以用来读取查询结果
            if (dc.Read())//如果查询结果不为空，执行以下语句
            {
#pragma warning disable CS8600 // 将 null 字面量或可能为 null 的值转换为非 null 类型。
                string no = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();//获取借书编号
#pragma warning disable CS8600 // 将 null 字面量或可能为 null 的值转换为非 null 类型。
                string id = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();//获取书号
                string sql = $"delete from t_lend where no = {no};update t_book set number = number + 1 where id = '{id}' ; ";
                Dao dao = new Dao();
                if (dao.Execute(sql) > 1)
                {
                    MessageBox.Show("还书成功！");
                    Table();
                }
                else
                {
                    MessageBox.Show("还书失败！");
                }
            }
            else//如果t_lend表中无数据，说明没有要归还的书了
            {
                MessageBox.Show("您没有需要归还的书了！");
            }
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
    }
}
