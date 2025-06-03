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
    public partial class admin22 : Form
    {
        string ID = "";//用来存放要修改书籍的书号，便于在数据库中查找
        public admin22()
        {
            InitializeComponent();
        }
        public admin22(string id,string name,string kind,string author,string press,string number)//修改图书界面的有参构造函数
        {
            InitializeComponent();
            ID = textBox1.Text = id;
            textBox2.Text = name;
            comboBox1.Text = kind;
            textBox3.Text = author;
            textBox4.Text = press;
            textBox5.Text = number;
        }
        private void button1_Click(object sender, EventArgs e)//修改按钮
        {
            //健壮性检查，不允许有空项。
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "" && comboBox1.Text != "")
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

                //点击修改按钮时，先把原记录删除
                string sql2 = $"delete from t_book where id = '{ID}';"; //删掉之前选中行的记录
                Dao dao1 = new Dao();
                dao1.Execute(sql2);

                //书号作为主键，不可重复，所以要在修改图书之前进行检查
                string sql1 = $"select * from t_book where id = '{textBox1.Text}';";
                
                IDataReader dc = dao1.Read(sql1);

                if (dc.Read())
                {
                    MessageBox.Show("书号已存在，请重新输入！");
                }
                else
                {
                    string sql = $"insert into t_book(id,name,kind_id,author,press,number) values('{textBox1.Text}','{textBox2.Text}',{book_kind_id},'{textBox3.Text}','{textBox4.Text}',{textBox5.Text})";
                    Dao dao = new Dao();
                    if (dao.Execute(sql) > 0)
                    {
                        MessageBox.Show("修改成功");
                        this.Close();
                    }
                }
                
            }
            else
            {
                MessageBox.Show("输入有空项，请重新输入！");
            }
        }

        private void admin22_Load(object sender, EventArgs e)
        {

        }
    }
}
