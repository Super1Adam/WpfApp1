using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace WpfApp1
{
    class Dao
    {
        private string connectionString = "Data Source=Adam\\SQLEXPRESS;Initial Catalog=library;Integrated Security=True;Encrypt=False";

        // 使用 using 语句来管理数据库连接
        public SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        // 执行查询并返回 SqlCommand
        public SqlCommand GetCommand(string sql)
        {
            SqlConnection sc = GetConnection();
            SqlCommand command = new SqlCommand(sql, sc);
            return command;
        }

        // 执行不返回结果的 SQL 语句（如 INSERT、UPDATE、DELETE）
        public int Execute(string sql)
        {
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand command = GetCommand(sql);
                command.Connection = connection;
                connection.Open();
                return command.ExecuteNonQuery(); // 返回受影响的行数
            }
        }

        // 执行查询并返回 SqlDataReader（适用于 SELECT 查询）
        public SqlDataReader Read(string sql)
        {
            SqlConnection connection = GetConnection();
            SqlCommand command = GetCommand(sql);
            command.Connection = connection;
            connection.Open();
            return command.ExecuteReader(CommandBehavior.CloseConnection); // 保证连接在读取完成后关闭
        }
    }
}

