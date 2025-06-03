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
        /// 指定要用于加密的块密码模式
        /// </summary>
        public CipherMode CipherMode { get; set; } = CipherMode.CBC;

        /// <summary>
        /// 指定在消息数据块短于加密操作所需的完整字节数时要应用的填充类型
        /// </summary>
        public PaddingMode PaddingMode { get; set; } = PaddingMode.PKCS7;

        /// <summary>
        /// 获取或设置加密操作的块大小（以位为单位）
        /// </summary>
        public int BlockSize { get; set; } = 128;

        /// <summary>
        /// 获取或设置用于对称算法的密钥大小（以位为单位）
        /// </summary>
        public int KeySize { get; set; } = 256;

        /// <summary>
        /// 用于对称算法的密钥（长度为[KeySize/8]字节）
        /// </summary>
        public byte[] RgbKey { get; } = "12345678901234567890123456789012".GetBytes();

        /// <summary>
        /// 用于对称算法的初始化向量（长度为16字节）
        /// </summary>
        public byte[] RgbIV { get; } = "1234567890123456".GetBytes();
        public login()//登录界面的构造函数
        {
            InitializeComponent();//初始化登录界面的窗口
            textBox2.PasswordChar = '*'; // 将输入的字符显示为星号

            // 创建AES配置并生成加密器和解密器
#pragma warning disable SYSLIB0021 // 类型或成员已过时
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
#pragma warning restore SYSLIB0021 // 类型或成员已过时
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
                while (true)//检查输入的密码是否包含字母、数字、特殊字符
                {
                    if (IsValidInput(textBox2.Text))
                    {
                        break; // 如果输入有效，跳出循环
                    }
                    else
                    {
                        // 提示输入无效
                        MessageBox.Show("输入无效！请确保输入的密码包含字母、数字和特殊字符。");

                        // 清空输入框并重新聚焦
                        textBox2.Clear();
                        textBox2.Focus();
                        return; // 提示用户重新输入后退出方法
                    }
                }

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

                string ciphertext = EncryptPassword(textBox2.Text);//调用加密函数

                string sql = $"select * from t_user where id='{textBox1.Text}' and psw ='{ciphertext}'";

                IDataReader dc = dao.Read(sql);//执行sql查询语句，dc可以用来读取查询结果

                if (dc.Read()) //检查是否有任何记录匹配，如果有，表示登录成功
                {
                    //存放登录用户的id和姓名
#pragma warning disable CS8601 // 引用类型赋值可能为 null。
                    Data.UID = dc["id"].ToString();
#pragma warning disable CS8601 // 引用类型赋值可能为 null。
                    Data.UName = dc["name"].ToString();


                    //检查文件中的账号密码
                    bool IsValidUser(string id, string password)
                    {
                        if (File.Exists(filePath))
                        {
                            foreach (var line in File.ReadLines(filePath))
                            {
                                var parts = line.Split(' ');// 将每一行按空格分隔成账号和密码
                                if (parts.Length == 2 && parts[0] == id && parts[1] == password)
                                {
                                    return true;  // 找到匹配的账号和密码
                                }
                            }
                        }
                        return false;  // 未找到匹配的账号和密码
                    }

                    if (!IsValidUser(textBox1.Text,ciphertext))//若本地文件中没有此账号密码，则提示是否要保存
                    {

                        DialogResult result = MessageBox.Show("是否将账号和密码保存？", "保存账号", MessageBoxButtons.YesNo);

                        if (result == DialogResult.Yes)
                        {
                            // 格式化账号和密码
                            string data = textBox1.Text + " " + ciphertext;

                            try
                            {
                                // 如果文件存在，追加数据；如果文件不存在，创建新文件并写入数据
                                File.AppendAllText(filePath, data + Environment.NewLine);

                                MessageBox.Show("账号和密码已成功保存！");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"发生错误：{ex.Message}");
                            }
                        }
                    }
                   

                    MessageBox.Show("登录成功！");
                    user1 user = new user1();//实例化用户界面对象
                    this.Hide();//关闭当前窗口
                    user.ShowDialog();//打开用户界面窗口
                }
                else
                {
                    MessageBox.Show("登录失败! 账号或密码不正确，请重新输入！");

                    DeleteAccount(textBox1.Text);

                }
                dao.DaoClose();//关闭数据库连接，释放资源。
            }
            if (radioButtonAdmin.Checked == true) //连接数据库中管理员表
            {
                Dao dao = new Dao();

                string ciphertext = EncryptPassword(textBox2.Text);//调用加密函数

                string sql = $"select * from t_admin where id='{textBox1.Text}' and psw ='{ciphertext}'";
                IDataReader dc = dao.Read(sql);
                if (dc.Read())
                {
                    //检查文件中的账号密码
                    bool IsValidUser(string id, string password)
                    {
                        if (File.Exists(filePath))
                        {
                            foreach (var line in File.ReadLines(filePath))
                            {
                                var parts = line.Split(' ');// 将每一行按空格分隔成账号和密码
                                if (parts.Length == 2 && parts[0] == id && parts[1] == password)
                                {
                                    return true;  // 找到匹配的账号和密码
                                }
                            }
                        }
                        return false;  // 未找到匹配的账号和密码
                    }

                    if (!IsValidUser(textBox1.Text, ciphertext))//若本地文件中没有此账号密码，则提示是否要保存
                    {

                        DialogResult result = MessageBox.Show("是否将账号和密码保存？", "保存账号", MessageBoxButtons.YesNo);

                        if (result == DialogResult.Yes)
                        {
                            // 格式化账号和密码
                            string data = textBox1.Text + " " + ciphertext;

                            try
                            {
                                // 如果文件存在，追加数据；如果文件不存在，创建新文件并写入数据
                                File.AppendAllText(filePath, data + Environment.NewLine);

                                MessageBox.Show("账号和密码已成功保存！");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"发生错误：{ex.Message}");
                            }
                        }
                    }
                    MessageBox.Show("登录成功！");

                    admin1 admin = new admin1(textBox1.Text);
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

        private void button4_Click(object sender, EventArgs e)//隐藏密码按钮
        {
            // 切换密码显示状态
            if (textBox2.PasswordChar == '*')
            {
                textBox2.PasswordChar = '\0'; // 显示真实内容
            }
            else
            {
                textBox2.PasswordChar = '*'; // 隐藏为掩码字符
            }
        }

        private void button3_Click(object sender, EventArgs e)//原来的自动填充按钮，弃用。
        {
           
        }

        // 检查字符串是否包含字母
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

        // 检查字符串是否包含数字
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

        // 检查字符串是否包含特殊字符（非字母、非数字）
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

        // 检查输入是否符合要求
        static bool IsValidInput(string input)
        {
            return ContainsLetter(input) && ContainsDigit(input) && ContainsSpecialChar(input);
        }

        public string EncryptPassword(string plainText)//加密函数
        {
            //加密密码
            // 读取明文并转为字节数组（默认使用UTF-8编码格式）
            var bytes = plainText.GetBytes();

            // 将待加密字节数组转为内存流
            var stream = new MemoryStream(bytes);

            // 创建加密流（使用加密器）
            var cryptoStream = new CryptoStream(stream, encryptor, CryptoStreamMode.Read);

            // 将加密流的内容拷贝到空的内存流中
            var encryptedStream = new MemoryStream();
            cryptoStream.CopyTo(encryptedStream);

            // 将新的内存流内容转为字节数组
            var encryptedBytes = encryptedStream.ToArray();

            // 以十六进制形式显示
            var password = encryptedBytes.ToHexString();

            return password;
        }

        public string DecryptPassword(string cipherText)//解密函数
        {
            // 读取十六进制密文并转为字节数组
            var bytes = cipherText.HexStringToBytes();

            // 将加密字节数组转为内存流
            var stream = new MemoryStream(bytes);

            // 创建加密流（使用解密器）
            var cryptoStream = new CryptoStream(stream, decryptor, CryptoStreamMode.Read);

            // 将加密流的内容拷贝到空的内存流中
            var clearStream = new MemoryStream();
            cryptoStream.CopyTo(clearStream);

            // 将新的内存流内容转为字节数组
            var encryptedBytes = clearStream.ToArray();

            // 将明文字节数组编码为字符串（默认使用UTF-8编码格式）
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
                        return parts[1];  // 返回找到的密码
                    }
                }
            }
            return null;  // 如果未找到账号，返回 空
        }

        private void textBox1_TextChanged(object sender, EventArgs e)//自动填充功能
        {
            string id = textBox1.Text;  // 获取账号输入框的内容

            // 如果输入框为空，直接返回
            if (string.IsNullOrWhiteSpace(id))
            {
                return;
            }

            // 查询文件中是否有该账号和密码
#pragma warning disable CS8600 // 将 null 字面量或可能为 null 的值转换为非 null 类型。
            string ciphertext = GetPasswordById(id);
#pragma warning restore CS8600 // 将 null 字面量或可能为 null 的值转换为非 null 类型。

            // 如果找到了对应的密码
            if (ciphertext != null )  // 只有在密码框为空时才弹出提示
            {
                string plaintext = DecryptPassword(ciphertext);//对文件中的密码密文进行解密

                // 弹出对话框询问用户是否自动填充密码
                DialogResult result = MessageBox.Show($"找到了账号 {id} 的密码：{plaintext}。\n是否自动填充密码？", "自动填充密码", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    textBox2.Text = plaintext;  // 自动填充密码框
                }
                else
                {
                    textBox2.Clear();  // 清空密码框，等待用户手动输入
                }
            }
        }

        private void ChangePassword(string id, string newPassword)//修改密码函数
        {
            string filePath = "D:\\Password\\password.txt";
            var lines = File.ReadAllLines(filePath).ToList();  // 读取所有行

            // 查找并更新账号的密码
            for (int i = 0; i < lines.Count; i++)
            {
                var parts = lines[i].Split(' ');
                if (parts.Length == 2 && parts[0] == id)
                {
                    // 更新密码
                    lines[i] = $"{id} {newPassword}";
                    break;
                }
            }

        }

        private bool DeleteAccount(string id)//删除保存的旧密码函数
        {
            var lines = File.ReadAllLines(filePath).ToList();  // 读取所有行

            // 查找并删除匹配账号的行
            bool isDeleted = false;//默认为false

            // 使用 .RemoveAll() 方法从列表中删除匹配的账号
            lines.RemoveAll(line => line.StartsWith(id + " "));

            // 如果删除成功，保存文件并返回 true
            if (lines.Count != File.ReadAllLines(filePath).Length)  // 如果行数变化，表示删除成功
            {
                File.WriteAllLines(filePath, lines);  // 保存修改后的内容回文件
                isDeleted = true;
            }

            return isDeleted;
        }
    }
}