using MySql.Data.MySqlClient;

namespace BOOKMS
{
    internal class Dao //负责连接mysql数据库及数据库操作的类
    {
        MySqlConnection myConn = new MySqlConnection();//创建数据库连接对象myConn

        public MySqlConnection Connect() //连接数据库的函数
        {
            string connStr = "server=localhost;User Id=root;password=123456;Database=bookms"; //本地数据库的地址
            myConn = new MySqlConnection(connStr);//将本地数据库的地址传入
            myConn.Open();//打开数据库
            return myConn;//返回数据库连接对象myConn
        }

        public MySqlCommand Command(string sql)//将要执行的sql语句传入
        {
            // 创建命令
            MySqlCommand cmd = new MySqlCommand(sql, Connect());
            return cmd;
        }


        public int Execute(string sql)//更新（增、删、改），返回受影响的行数
        {
            return Command(sql).ExecuteNonQuery();
        }

        public MySqlDataReader Read(string sql)//读取数据库中的数据
        {
            return Command(sql).ExecuteReader();
        }

        public void DaoClose()
        {
            myConn.Close();//关闭数据库连接
        }

    }
}
