using System.Data;

namespace BOOKMS
{
    public partial class login : Form
    {
        public login()//登录界面的构造函数
        {
            InitializeComponent();//初始化登录界面的窗口
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)//登录按钮的控件函数
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                Login();//调用登录函数
            }
            else
            {
                MessageBox.Show("输入有空项，请重新输入！");
            }
        }
        public void Login()//定义登录函数
        {
            if (radioButtonUser.Checked == true) //连接数据库中用户表
            {
                Dao dao = new Dao();
                string sql = $"select * from t_user where id='{textBox1.Text}' and psw ='{textBox2.Text}'";
                IDataReader dc = dao.Read(sql);//执行sql查询语句，dc可以用来读取查询结果
                if (dc.Read()) //检查是否有任何记录匹配，如果有，表示登录成功
                {
                    //存放登录用户的id和姓名
#pragma warning disable CS8601 // 引用类型赋值可能为 null。
                    Data.UID = dc["id"].ToString();
#pragma warning disable CS8601 // 引用类型赋值可能为 null。
                    Data.UName = dc["name"].ToString();

                    MessageBox.Show("登录成功！");

                    user1 user = new user1();//实例化用户界面对象
                    this.Hide();//关闭当前窗口
                    user.ShowDialog();//打开用户界面窗口
                }
                else
                {
                    MessageBox.Show("登录失败! 账号或密码不正确，请重新输入！");
                }
                dao.DaoClose();//关闭数据库连接，释放资源。
            } 
            if(radioButtonAdmin.Checked == true) //连接数据库中管理员表
            {
                Dao dao = new Dao();
                string sql = $"select * from t_admin where id='{textBox1.Text}' and psw ='{textBox2.Text}'";
                IDataReader dc = dao.Read(sql);
                if (dc.Read())
                {
                    MessageBox.Show("登录成功！");

                    admin1 admin = new admin1();
                    this.Hide();
                    admin.ShowDialog();
                }
                else
                {
                    MessageBox.Show("登录失败! 账号或密码不正确，请重新输入！");
                }
                dao.DaoClose();
            }
        }

        private void login_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)//取消按钮
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void radioButtonAdmin_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}