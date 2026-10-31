//Program.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WoosongBit.DataBase;

namespace _0928_DB
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MyDB db = new MyDB();
            db.Open();
            db.ServerInfo();

            // 1. 계좌 개설 (프로시저)
            Console.WriteLine("=== 계좌 개설 ===");
            //db.Proc_Insert_Account(111, "홍길동", 1000);

            // 2. 입금 (프로시저)
            Console.WriteLine("=== 입금 500 ===");
            db.Proc_Input_Account(111, 500);

            // 3. 출금 (프로시저)
            Console.WriteLine("=== 출금 300 ===");
            db.Proc_Output_Account(111, 300);

            // 4. 계좌 검색
            Console.WriteLine("=== 계좌 검색 ===");
            Console.WriteLine(db.SelectAccount(111));
            Console.WriteLine("--------------------------");
            Console.WriteLine(db.SelectTransactions(111));

            // 5. 계좌 삭제 (프로시저)
            // db.Proc_Delete_Account(111);

            db.Close();
            Console.ReadLine();
        }
    }
}
