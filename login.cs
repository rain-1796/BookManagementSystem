using System.Data;
using System.IO;
using System.Security.Cryptography;


namespace BOOKMS
{
    public partial class login : Form
    {
        
        private string filePath = "D:\\Password\\password.txt";
         

        private ICryptoTransform encryptor;
        private ICryptoTransform decryptor;

        /// <summary>
        /// ָ��Ҫ���ڼ��ܵĿ�����ģʽ
        /// </summary>
        public CipherMode CipherMode { get; set; } = CipherMode.CBC;

        /// <summary>
        /// ָ������Ϣ���ݿ���ڼ��ܲ�������������ֽ���ʱҪӦ�õ��������
        /// </summary>
        public PaddingMode PaddingMode { get; set; } = PaddingMode.PKCS7;

        /// <summary>
        /// ��ȡ�����ü��ܲ����Ŀ��С����λΪ��λ��
        /// </summary>
        public int BlockSize { get; set; } = 128;

        /// <summary>
        /// ��ȡ���������ڶԳ��㷨����Կ��С����λΪ��λ��
        /// </summary>
        public int KeySize { get; set; } = 256;

        /// <summary>
        /// ���ڶԳ��㷨����Կ������Ϊ[KeySize/8]�ֽڣ�
        /// </summary>
        public byte[] RgbKey { get; } = "12345678901234567890123456789012".GetBytes();

        /// <summary>
        /// ���ڶԳ��㷨�ĳ�ʼ������������Ϊ16�ֽڣ�
        /// </summary>
        public byte[] RgbIV { get; } = "1234567890123456".GetBytes();
        public login()//��¼����Ĺ��캯��
        {
            InitializeComponent();//��ʼ����¼����Ĵ���
            textBox2.PasswordChar = '*'; // ��������ַ���ʾΪ�Ǻ�

            // ����AES���ò����ɼ������ͽ�����
#pragma warning disable SYSLIB0021 // ���ͻ��Ա�ѹ�ʱ
            using (var managed = new AesManaged()
            {
                Mode = CipherMode,
                KeySize = KeySize,
                Padding = PaddingMode,
                BlockSize = BlockSize,
                Key = RgbKey,
                IV = RgbIV,
            })
            {
                encryptor = managed.CreateEncryptor();
                decryptor = managed.CreateDecryptor();
            }
#pragma warning restore SYSLIB0021 // ���ͻ��Ա�ѹ�ʱ
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
                while (true)//�������������Ƿ������ĸ�����֡������ַ�
                {
                    if (IsValidInput(textBox2.Text))
                    {
                        break; // ���������Ч������ѭ��
                    }
                    else
                    {
                        // ��ʾ������Ч
                        MessageBox.Show("������Ч����ȷ����������������ĸ�����ֺ������ַ���");

                        // �����������¾۽�
                        textBox2.Clear();
                        textBox2.Focus();
                        return; // ��ʾ�û�����������˳�����
                    }
                }

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

                string ciphertext = EncryptPassword(textBox2.Text);//���ü��ܺ���

                string sql = $"select * from t_user where id='{textBox1.Text}' and psw ='{ciphertext}'";

                IDataReader dc = dao.Read(sql);//ִ��sql��ѯ��䣬dc����������ȡ��ѯ���

                if (dc.Read()) //����Ƿ����κμ�¼ƥ�䣬����У���ʾ��¼�ɹ�
                {
                    //��ŵ�¼�û���id������
#pragma warning disable CS8601 // �������͸�ֵ����Ϊ null��
                    Data.UID = dc["id"].ToString();
#pragma warning disable CS8601 // �������͸�ֵ����Ϊ null��
                    Data.UName = dc["name"].ToString();


                    //����ļ��е��˺�����
                    bool IsValidUser(string id, string password)
                    {
                        if (File.Exists(filePath))
                        {
                            foreach (var line in File.ReadLines(filePath))
                            {
                                var parts = line.Split(' ');// ��ÿһ�а��ո�ָ����˺ź�����
                                if (parts.Length == 2 && parts[0] == id && parts[1] == password)
                                {
                                    return true;  // �ҵ�ƥ����˺ź�����
                                }
                            }
                        }
                        return false;  // δ�ҵ�ƥ����˺ź�����
                    }

                    if (!IsValidUser(textBox1.Text,ciphertext))//�������ļ���û�д��˺����룬����ʾ�Ƿ�Ҫ����
                    {

                        DialogResult result = MessageBox.Show("�Ƿ��˺ź����뱣�棿", "�����˺�", MessageBoxButtons.YesNo);

                        if (result == DialogResult.Yes)
                        {
                            // ��ʽ���˺ź�����
                            string data = textBox1.Text + " " + ciphertext;

                            try
                            {
                                // ����ļ����ڣ�׷�����ݣ�����ļ������ڣ��������ļ���д������
                                File.AppendAllText(filePath, data + Environment.NewLine);

                                MessageBox.Show("�˺ź������ѳɹ����棡");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"��������{ex.Message}");
                            }
                        }
                    }
                   

                    MessageBox.Show("��¼�ɹ���");
                    user1 user = new user1();//ʵ�����û��������
                    this.Hide();//�رյ�ǰ����
                    user.ShowDialog();//���û����洰��
                }
                else
                {
                    MessageBox.Show("��¼ʧ��! �˺Ż����벻��ȷ�����������룡");

                    DeleteAccount(textBox1.Text);

                }
                dao.DaoClose();//�ر����ݿ����ӣ��ͷ���Դ��
            }
            if (radioButtonAdmin.Checked == true) //�������ݿ��й���Ա��
            {
                Dao dao = new Dao();

                string ciphertext = EncryptPassword(textBox2.Text);//���ü��ܺ���

                string sql = $"select * from t_admin where id='{textBox1.Text}' and psw ='{ciphertext}'";
                IDataReader dc = dao.Read(sql);
                if (dc.Read())
                {
                    //����ļ��е��˺�����
                    bool IsValidUser(string id, string password)
                    {
                        if (File.Exists(filePath))
                        {
                            foreach (var line in File.ReadLines(filePath))
                            {
                                var parts = line.Split(' ');// ��ÿһ�а��ո�ָ����˺ź�����
                                if (parts.Length == 2 && parts[0] == id && parts[1] == password)
                                {
                                    return true;  // �ҵ�ƥ����˺ź�����
                                }
                            }
                        }
                        return false;  // δ�ҵ�ƥ����˺ź�����
                    }

                    if (!IsValidUser(textBox1.Text, ciphertext))//�������ļ���û�д��˺����룬����ʾ�Ƿ�Ҫ����
                    {

                        DialogResult result = MessageBox.Show("�Ƿ��˺ź����뱣�棿", "�����˺�", MessageBoxButtons.YesNo);

                        if (result == DialogResult.Yes)
                        {
                            // ��ʽ���˺ź�����
                            string data = textBox1.Text + " " + ciphertext;

                            try
                            {
                                // ����ļ����ڣ�׷�����ݣ�����ļ������ڣ��������ļ���д������
                                File.AppendAllText(filePath, data + Environment.NewLine);

                                MessageBox.Show("�˺ź������ѳɹ����棡");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"��������{ex.Message}");
                            }
                        }
                    }
                    MessageBox.Show("��¼�ɹ���");

                    admin1 admin = new admin1(textBox1.Text);
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

        private void button4_Click(object sender, EventArgs e)//�������밴ť
        {
            // �л�������ʾ״̬
            if (textBox2.PasswordChar == '*')
            {
                textBox2.PasswordChar = '\0'; // ��ʾ��ʵ����
            }
            else
            {
                textBox2.PasswordChar = '*'; // ����Ϊ�����ַ�
            }
        }

        private void button3_Click(object sender, EventArgs e)//ԭ�����Զ���䰴ť�����á�
        {
           
        }

        // ����ַ����Ƿ������ĸ
        static bool ContainsLetter(string input)
        {
            foreach (char c in input)
            {
                if (Char.IsLetter(c))
                {
                    return true;
                }
            }
            return false;
        }

        // ����ַ����Ƿ��������
        static bool ContainsDigit(string input)
        {
            foreach (char c in input)
            {
                if (Char.IsDigit(c))
                {
                    return true;
                }
            }
            return false;
        }

        // ����ַ����Ƿ���������ַ�������ĸ�������֣�
        static bool ContainsSpecialChar(string input)
        {
            foreach (char c in input)
            {
                if (!Char.IsLetterOrDigit(c))
                {
                    return true;
                }
            }
            return false;
        }

        // ��������Ƿ����Ҫ��
        static bool IsValidInput(string input)
        {
            return ContainsLetter(input) && ContainsDigit(input) && ContainsSpecialChar(input);
        }

        public string EncryptPassword(string plainText)//���ܺ���
        {
            //��������
            // ��ȡ���Ĳ�תΪ�ֽ����飨Ĭ��ʹ��UTF-8�����ʽ��
            var bytes = plainText.GetBytes();

            // ���������ֽ�����תΪ�ڴ���
            var stream = new MemoryStream(bytes);

            // ������������ʹ�ü�������
            var cryptoStream = new CryptoStream(stream, encryptor, CryptoStreamMode.Read);

            // �������������ݿ������յ��ڴ�����
            var encryptedStream = new MemoryStream();
            cryptoStream.CopyTo(encryptedStream);

            // ���µ��ڴ�������תΪ�ֽ�����
            var encryptedBytes = encryptedStream.ToArray();

            // ��ʮ��������ʽ��ʾ
            var password = encryptedBytes.ToHexString();

            return password;
        }

        public string DecryptPassword(string cipherText)//���ܺ���
        {
            // ��ȡʮ���������Ĳ�תΪ�ֽ�����
            var bytes = cipherText.HexStringToBytes();

            // �������ֽ�����תΪ�ڴ���
            var stream = new MemoryStream(bytes);

            // ������������ʹ�ý�������
            var cryptoStream = new CryptoStream(stream, decryptor, CryptoStreamMode.Read);

            // �������������ݿ������յ��ڴ�����
            var clearStream = new MemoryStream();
            cryptoStream.CopyTo(clearStream);

            // ���µ��ڴ�������תΪ�ֽ�����
            var encryptedBytes = clearStream.ToArray();

            // �������ֽ��������Ϊ�ַ�����Ĭ��ʹ��UTF-8�����ʽ��
            var plainText = encryptedBytes.EncodeToString();

            return plainText;
        }


        private string? GetPasswordById(string id)
        {
            if (File.Exists(filePath))
            {
                foreach (var line in File.ReadLines(filePath))
                {
                    var parts = line.Split(' ');
                    if (parts.Length == 2 && parts[0] == id)
                    {
                        return parts[1];  // �����ҵ�������
                    }
                }
            }
            return null;  // ���δ�ҵ��˺ţ����� ��
        }

        private void textBox1_TextChanged(object sender, EventArgs e)//�Զ���书��
        {
            string id = textBox1.Text;  // ��ȡ�˺�����������

            // ��������Ϊ�գ�ֱ�ӷ���
            if (string.IsNullOrWhiteSpace(id))
            {
                return;
            }

            // ��ѯ�ļ����Ƿ��и��˺ź�����
#pragma warning disable CS8600 // �� null �����������Ϊ null ��ֵת��Ϊ�� null ���͡�
            string ciphertext = GetPasswordById(id);
#pragma warning restore CS8600 // �� null �����������Ϊ null ��ֵת��Ϊ�� null ���͡�

            // ����ҵ��˶�Ӧ������
            if (ciphertext != null )  // ֻ���������Ϊ��ʱ�ŵ�����ʾ
            {
                string plaintext = DecryptPassword(ciphertext);//���ļ��е��������Ľ��н���

                // �����Ի���ѯ���û��Ƿ��Զ��������
                DialogResult result = MessageBox.Show($"�ҵ����˺� {id} �����룺{plaintext}��\n�Ƿ��Զ�������룿", "�Զ��������", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    textBox2.Text = plaintext;  // �Զ���������
                }
                else
                {
                    textBox2.Clear();  // �������򣬵ȴ��û��ֶ�����
                }
            }
        }

        private void ChangePassword(string id, string newPassword)//�޸����뺯��
        {
            string filePath = "D:\\Password\\password.txt";
            var lines = File.ReadAllLines(filePath).ToList();  // ��ȡ������

            // ���Ҳ������˺ŵ�����
            for (int i = 0; i < lines.Count; i++)
            {
                var parts = lines[i].Split(' ');
                if (parts.Length == 2 && parts[0] == id)
                {
                    // ��������
                    lines[i] = $"{id} {newPassword}";
                    break;
                }
            }

        }

        private bool DeleteAccount(string id)//ɾ������ľ����뺯��
        {
            var lines = File.ReadAllLines(filePath).ToList();  // ��ȡ������

            // ���Ҳ�ɾ��ƥ���˺ŵ���
            bool isDeleted = false;//Ĭ��Ϊfalse

            // ʹ�� .RemoveAll() �������б���ɾ��ƥ����˺�
            lines.RemoveAll(line => line.StartsWith(id + " "));

            // ���ɾ���ɹ��������ļ������� true
            if (lines.Count != File.ReadAllLines(filePath).Length)  // ��������仯����ʾɾ���ɹ�
            {
                File.WriteAllLines(filePath, lines);  // �����޸ĺ�����ݻ��ļ�
                isDeleted = true;
            }

            return isDeleted;
        }
    }
}