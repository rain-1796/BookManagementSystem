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
    public partial class admin21 : Form
    {
        public admin21()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)//添加按钮
        {
            //健壮性检查，输入不能有空项 
            if (textBox1.Text!=""&& textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "" && comboBox1.Text!= "")
            {
                int book_kind_id = 0;
                if (comboBox1.Text == "1.文学")
                {
                    book_kind_id = 1;
                }
                else if (comboBox1.Text == "2.科幻")
                {
                    book_kind_id = 2;
                }
                else if (comboBox1.Text == "3.历史")
                {
                    book_kind_id = 3;
                }
                else if (comboBox1.Text == "4.小说")
                {
                    book_kind_id = 4;
                }
                else
                {
                    book_kind_id = 5;
                }

                //书号作为主键，不可重复，所以要在添加图书之前进行检查
                string sql1 = $"select * from t_book where id = '{textBox1.Text}';";
                Dao dao1= new Dao();
                IDataReader dc = dao1.Read(sql1);

                if (dc.Read())
                {
                    MessageBox.Show("书号已存在，请重新输入！");
                }
                else
                {
                    //这里要注意textbox.Text的值是否一一对应
                    string sql = $"insert into t_book(id,name,kind_id,author,press,number) values('{textBox1.Text}','{textBox2.Text}',{book_kind_id},'{textBox3.Text}','{textBox4.Text}',{textBox5.Text})";
                    Dao dao = new Dao();
                    if (dao.Execute(sql) > 0)//如果受影响的行数大于0
                    {
                        MessageBox.Show("添加成功");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("添加失败");
                    }
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox5.Text = "";
                    comboBox1.SelectedItem = null;//设置下拉框为空
                }
            }
            else
            {
                MessageBox.Show("输入有空项，请重新输入！");
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)//清空按钮
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            comboBox1.SelectedItem = null;//设置下拉框为空

        }

        private void admin21_Load(object sender, EventArgs e)
        {

        }
    }
}
