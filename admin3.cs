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
    public partial class admin3 : Form
    {
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

        /// <summary>同时，密码在存储前进行加密处理，确保数据的安全性。


        /// 用于对称算法的密钥（长度为[KeySize/8]字节）
        /// </summary>
        public byte[] RgbKey { get; } = "12345678901234567890123456789012".GetBytes();

        /// <summary>
        /// 用于对称算法的初始化向量（长度为16字节）
        /// </summary>
        public byte[] RgbIV { get; } = "1234567890123456".GetBytes();
        public admin3()
        {
            InitializeComponent();
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

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox4.Text != "" && textBox5.Text != "" && textBox6.Text != "")
            {
                while (true)//检查输入的密码是否包含字母、数字、特殊字符
                {
                    if (IsValidInput(textBox6.Text))
                    {
                        break; // 如果输入有效，跳出循环
                    }
                    else
                    {
                        // 提示输入无效
                        MessageBox.Show("输入无效！请确保输入的密码包含字母、数字和特殊字符。");

                        // 清空输入框并重新聚焦
                        textBox6.Clear();
                        textBox6.Focus();
                        return; // 提示用户重新输入后退出方法
                    }
                }

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

                    //加密密码
                    // 读取明文并转为字节数组（默认使用UTF-8编码格式）
                    var bytes = textBox6.Text.GetBytes();

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

                    string gender = radioButton2.Checked ? "男" : "女";//判断选中的是男还是女
                    string sql = $"insert into t_user(id,name,sex,psw) values('{textBox4.Text}','{textBox5.Text}','{gender}','{password}');";//将数据添加到用户表中
                    if (dao.Execute(sql) > 0)
                    {
                        MessageBox.Show("注册成功！");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("注册失败！");
                    }
                    textBox4.Text = "";
                    textBox5.Text = "";
                    textBox6.Text = "";
                }
            }
            else
            {
                MessageBox.Show("输入有空项，请重新输入！");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            radioButtonMale.Checked = true;
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

        private void admin3_Load(object sender, EventArgs e)
        {

        }
    }
   
}

