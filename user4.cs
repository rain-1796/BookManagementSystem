using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;

namespace BOOKMS
{
    public partial class user4 : Form
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
        public user4(string id)
        {
            InitializeComponent();
            textBox1.Text = id;

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

        private void button1_Click(object sender, EventArgs e)//修改密码按钮
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "")
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
                        textBox3.Clear();
                        textBox2.Focus();
                        return; // 提示用户重新输入后退出方法
                    }
                }

                if (textBox2.Text == textBox3.Text)
                {
                    Dao dao = new Dao();

                    //密码加密

                    // 读取明文并转为字节数组（默认使用UTF-8编码格式）
                    var bytes = textBox2.Text.GetBytes();

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

                    string sql = $"update t_user set psw = '{password}' where id = '{textBox1.Text}';";
                    IDataReader dc = dao.Read(sql);



                    if (dao.Execute(sql) > 0)
                    {
                        MessageBox.Show("修改成功！");
                        DialogResult result = MessageBox.Show("是否要将新密码保存？", "保存本地", MessageBoxButtons.YesNo);

                        if (result == DialogResult.Yes)
                        {
                            string newPassword = password;

                            ChangePassword(textBox1.Text, newPassword);
                            MessageBox.Show("新密码保存成功！");
                        }

                    }
                    else
                    {
                        MessageBox.Show("修改失败！");
                    }
                    textBox2.Text = "";
                    textBox3.Text = "";
                }
                else
                {
                    MessageBox.Show("两次输入的新密码不一致，请重新输入！");
                }

            }
            else
            {
                MessageBox.Show("输入有空项，请重新输入！");
            }
        }

        private void button2_Click(object sender, EventArgs e)//取消按钮
        {
            textBox2.Text = "";
            textBox3.Text = "";
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

        private void button3_Click(object sender, EventArgs e)
        {
            // 生成一个长度为 12 的随机密码（可以根据需求调整长度）
            string randomPassword = GenerateRandomPassword(6);

            // 将生成的密码填充到密码输入框中
            textBox2.Text = randomPassword;
        }

        // 生成随机密码的方法
        private string GenerateRandomPassword(int length)
        {
            const string upperChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";   // 大写字母
            const string lowerChars = "abcdefghijklmnopqrstuvwxyz";   // 小写字母
            const string digits = "0123456789";                       // 数字
            const string specialChars = "!@#$%^&*()-_=+[]{}|;:,.<>?/"; // 特殊字符

            // 合并所有可用的字符
            string allChars = upperChars + lowerChars + digits + specialChars;

            // 创建一个随机数生成器
            Random random = new Random();

            // 确保密码包含至少一个大写字母、一个小写字母、一个数字和一个特殊字符
            char[] password = new char[length];

            // 确保每种类型的字符都至少出现一次
            password[0] = upperChars[random.Next(upperChars.Length)];
            password[1] = lowerChars[random.Next(lowerChars.Length)];
            password[2] = digits[random.Next(digits.Length)];
            password[3] = specialChars[random.Next(specialChars.Length)];

            // 填充剩余的密码字符
            for (int i = 4; i < length; i++)
            {
                password[i] = allChars[random.Next(allChars.Length)];
            }

            // 打乱密码数组以确保顺序随机
            password = password.OrderBy(c => random.Next()).ToArray();

            // 返回密码作为字符串
            return new string(password);
        }

        private void ChangePassword(string id, string newPassword)//修改密码函数
        {
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

            // 保存修改后的内容回文件
            File.WriteAllLines(filePath, lines);
        }
    }
}
