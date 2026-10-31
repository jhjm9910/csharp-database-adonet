using System;
using System.Data.SqlClient;

namespace ConsoleApp1
{
    internal class Program
    {
        // ✔ 여기 OK (전역 변수)
        static string connStr = "Server=localhost\\SQLEXPRESS;Database=WB43;Trusted_Connection=True;";

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("1. 추가 2. 조회 0. 종료");
                int sel = int.Parse(Console.ReadLine());

                switch (sel)
                {
                    case 1: Insert(); break;
                    case 2: Select(); break;
                    case 0: return;
                }
            }
        }

        static void Insert()
        {
            // ✔ using은 함수 안에서만
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();

                string sql = "INSERT INTO Account VALUES (1, '홍길동', 1000)";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();

                Console.WriteLine("추가 완료");
            }
        }

        static void Select()
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();

                string sql = "SELECT * FROM Account";
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine(
                        $"{reader["accNumber"]} / {reader["name"]} / {reader["balance"]}");
                }
            }
        }
    }
}