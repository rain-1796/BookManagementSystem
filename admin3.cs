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
    public partial class admin3 : Form
    {
        public admin3()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void admin3_Load(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)//办理按钮
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "")
            {
                //账号id作为主键，不可重复，所以要在办理借书卡之前进行检查
                string sql1 = $"select * from t_user where id = '{textBox1.Text}';";
                Dao dao1 = new Dao();
                IDataReader dc = dao1.Read(sql1);

                if (dc.Read())
                {
                    MessageBox.Show("账号已存在，请重新输入！");
                }
                else
                {
                    Dao dao = new Dao();
                    string gender = radioButtonMale.Checked ? "男" : "女";//判断选中的是男还是女
                    string sql = $"insert into t_user(id,name,sex,psw) values('{textBox1.Text}','{textBox2.Text}','{gender}','{textBox3.Text}');";//将数据添加到用户表中
                    if (dao.Execute(sql) > 0)
                    {
                        MessageBox.Show("办理成功！");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("办理失败！");
                    }
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                }
            }
            else
            {
                MessageBox.Show("输入有空项，请重新输入！");
            }


        }

        private void button2_Click(object sender, EventArgs e)//清空按钮
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            radioButtonMale.Checked = true;
        }
    }
}
