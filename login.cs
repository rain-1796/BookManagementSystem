using System.Data;

namespace BOOKMS
{
    public partial class login : Form
    {
        public login()//��¼����Ĺ��캯��
        {
            InitializeComponent();//��ʼ����¼����Ĵ���
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)//��¼��ť�Ŀؼ�����
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                Login();//���õ�¼����
            }
            else
            {
                MessageBox.Show("�����п�����������룡");
            }
        }
        public void Login()//�����¼����
        {
            if (radioButtonUser.Checked == true) //�������ݿ����û���
            {
                Dao dao = new Dao();
                string sql = $"select * from t_user where id='{textBox1.Text}' and psw ='{textBox2.Text}'";
                IDataReader dc = dao.Read(sql);//ִ��sql��ѯ��䣬dc����������ȡ��ѯ���
                if (dc.Read()) //����Ƿ����κμ�¼ƥ�䣬����У���ʾ��¼�ɹ�
                {
                    //��ŵ�¼�û���id������
#pragma warning disable CS8601 // �������͸�ֵ����Ϊ null��
                    Data.UID = dc["id"].ToString();
#pragma warning disable CS8601 // �������͸�ֵ����Ϊ null��
                    Data.UName = dc["name"].ToString();

                    MessageBox.Show("��¼�ɹ���");

                    user1 user = new user1();//ʵ�����û��������
                    this.Hide();//�رյ�ǰ����
                    user.ShowDialog();//���û����洰��
                }
                else
                {
                    MessageBox.Show("��¼ʧ��! �˺Ż����벻��ȷ�����������룡");
                }
                dao.DaoClose();//�ر����ݿ����ӣ��ͷ���Դ��
            } 
            if(radioButtonAdmin.Checked == true) //�������ݿ��й���Ա��
            {
                Dao dao = new Dao();
                string sql = $"select * from t_admin where id='{textBox1.Text}' and psw ='{textBox2.Text}'";
                IDataReader dc = dao.Read(sql);
                if (dc.Read())
                {
                    MessageBox.Show("��¼�ɹ���");

                    admin1 admin = new admin1();
                    this.Hide();
                    admin.ShowDialog();
                }
                else
                {
                    MessageBox.Show("��¼ʧ��! �˺Ż����벻��ȷ�����������룡");
                }
                dao.DaoClose();
            }
        }

        private void login_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)//ȡ����ť
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void radioButtonAdmin_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}