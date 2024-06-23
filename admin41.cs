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
    public partial class admin41 : Form
    {
        string ID = "";
        public admin41()
        {
            InitializeComponent();
        }

        public admin41(string id,string name,string sex,string psw)//有参构造函数
        {
           
            InitializeComponent();
            ID = textBox1.Text = id;
            textBox2.Text= name;
            if (sex == "男")
            {
                radioButtonMale.Checked = true;

            }
            else
            {
                radioButtonFemale.Checked = true;
            }

            textBox3.Text= psw;

        }
        private void admin41_Load(object sender, EventArgs e)
        {

        }


        private void button1_Click(object sender, EventArgs e)//修改按钮
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "")
            {
                //点击修改按钮时，先把原记录删除
                Dao dao1 = new Dao();
                string sql2 = $"delete from t_user where id = '{ID}';";
                dao1.Execute(sql2);


                //账号id作为主键，不可重复，所以要在修改借书卡信息之前进行检查
                string sql1 = $"select * from t_user where id = '{textBox1.Text}';";
                IDataReader dc = dao1.Read(sql1);

                if (dc.Read())
                {
                    MessageBox.Show("账号已存在，请重新输入！");
                }
                else
                {
                    Dao dao = new Dao();
                    string gender = radioButtonMale.Checked ? "男" : "女";//判断选中的是男还是女
                    string sql = $"insert into t_user(id,name,sex,psw) values('{textBox1.Text}','{textBox2.Text}','{gender}',{textBox3.Text});";
                    if (dao.Execute(sql) > 0)
                    {
                        MessageBox.Show("修改成功！");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("修改失败！");
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
