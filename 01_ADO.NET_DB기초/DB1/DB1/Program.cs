using System;
using System.Data.SqlClient;

class Program
{
    // 연결 문자열 - 본인 서버 정보로 수정!
    static string conStr = "Server=localhost\\sqlexpress; Database=WB43; User Id=YOUR_USER; Password=YOUR_PASSWORD; TrustServerCertificate=True;";
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("\n======= 상품 관리 프로그램 =======");
            Console.WriteLine("1. 상품 전체 조회");
            Console.WriteLine("2. 상품 추가");
            Console.WriteLine("3. 상품 수정");
            Console.WriteLine("4. 상품 삭제");
            Console.WriteLine("0. 종료");
            Console.Write("선택: ");

            string input = Console.ReadLine();

            switch (input)
            {
                case "1": SelectAll(); break;
                case "2": Insert(); break;
                case "3": Update(); break;
                case "4": Delete(); break;
                case "0": return;
                default: Console.WriteLine("잘못된 입력입니다!"); break;
            }
        }
    }

    // ✅ 전체 조회
    static void SelectAll()
    {
        using (SqlConnection conn = new SqlConnection(conStr))
        {
            conn.Open();
            string query = "SELECT * FROM JJH.Table_1";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            Console.WriteLine("\n--- 상품 목록 ---");
            while (reader.Read())
            {
                Console.WriteLine($"PID: {reader["PID"]} | 이름: {reader["PNAME"]} | 가격: {reader["Price"]} | 설명: {reader["Description"]}");
            }
        }
    }

    // ✅ 추가
    static void Insert()
    {
        Console.Write("상품명: ");
        string pname = Console.ReadLine();
        Console.Write("가격: ");
        int price = int.Parse(Console.ReadLine());
        Console.Write("설명: ");
        string desc = Console.ReadLine();

        using (SqlConnection conn = new SqlConnection(conStr))
        {
            conn.Open();
            string query = "INSERT INTO JJH.Table_1 (PNAME, Price, Description) VALUES (@pname, @price, @desc)";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@pname", pname);
            cmd.Parameters.AddWithValue("@price", price);
            cmd.Parameters.AddWithValue("@desc", desc);
            cmd.ExecuteNonQuery();
            Console.WriteLine("✅ 상품이 추가되었습니다!");
        }
    }

    // ✅ 수정
    static void Update()
    {
        SelectAll();
        Console.Write("수정할 상품 PID: ");
        int pid = int.Parse(Console.ReadLine());
        Console.Write("새 상품명: ");
        string pname = Console.ReadLine();
        Console.Write("새 가격: ");
        int price = int.Parse(Console.ReadLine());

        using (SqlConnection conn = new SqlConnection(conStr))
        {
            conn.Open();
            string query = "UPDATE JJH.Table_1 SET PNAME=@pname, Price=@price WHERE PID=@pid";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@pname", pname);
            cmd.Parameters.AddWithValue("@price", price);
            cmd.Parameters.AddWithValue("@pid", pid);
            cmd.ExecuteNonQuery();
            Console.WriteLine("✅ 수정되었습니다!");
        }
    }

    // ✅ 삭제
    static void Delete()
    {
        SelectAll();
        Console.Write("삭제할 상품 PID: ");
        int pid = int.Parse(Console.ReadLine());

        using (SqlConnection conn = new SqlConnection(conStr))
        {
            conn.Open();
            string query = "DELETE FROM JJH.Table_1 WHERE PID=@pid";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@pid", pid);
            cmd.ExecuteNonQuery();
            Console.WriteLine("✅ 삭제되었습니다!");
        }
    }
}