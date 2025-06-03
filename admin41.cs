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
    public partial class admin41 : Form
    {
        string ID = "";

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

      /*  public admin41()
        {
            InitializeComponent();
        }*/

        public admin41(string id, string name, string sex, string psw)//有参构造函数
        {
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

            //密码解密
            // 读取十六进制密文并转为字节数组
            var bytes = psw.HexStringToBytes();

            // 将加密字节数组转为内存流
            var stream = new MemoryStream(bytes);

            // 创建加密流（使用解密器）
#pragma warning disable CS8604 // 引用类型参数可能为 null。
            var cryptoStream = new CryptoStream(stream, decryptor, CryptoStreamMode.Read);
#pragma warning restore CS8604 // 引用类型参数可能为 null。

            // 将加密流的内容拷贝到空的内存流中
            var clearStream = new MemoryStream();
            cryptoStream.CopyTo(clearStream);

            // 将新的内存流内容转为字节数组
            var encryptedBytes = clearStream.ToArray();

            // 将明文字节数组编码为字符串（默认使用UTF-8编码格式）
            textBox3.Text = encryptedBytes.EncodeToString();




        }
        private void admin41_Load(object sender, EventArgs e)
        {

        }


        private void button1_Click(object sender, EventArgs e)//修改按钮
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "")
            {
                while (true)//检查输入的密码是否包含字母、数字、特殊字符
                {
                    if (IsValidInput(textBox3.Text))
                    {
                        break; // 如果输入有效，跳出循环
                    }
                    else
                    {
                        // 提示输入无效
                        MessageBox.Show("输入无效！请确保输入的密码包含字母、数字和特殊字符。");

                        // 清空输入框并重新聚焦
                        textBox3.Clear();
                        textBox3.Focus();
                        return; // 提示用户重新输入后退出方法
                    }
                }

                Dao dao = new Dao();

                // 读取明文并转为字节数组（默认使用UTF-8编码格式）
                var bytes = textBox3.Text.GetBytes();

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

                string gender = radioButtonMale.Checked ? "男" : "女";//判断选中的是男还是女

                string sql = $"update t_user set psw='{password}',name='{textBox2.Text}',sex='{gender}' where id = '{textBox1.Text}';";//修改数据库中的数据

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
            else
            {
                MessageBox.Show("输入有空项，请重新输入！");
            }
        }

        private void button2_Click(object sender, EventArgs e)//清空按钮
        {
            textBox2.Text = "";
            textBox3.Text = "";
            radioButtonMale.Checked = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
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
    }
}
