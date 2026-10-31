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

            /*
            //db.Insert_Custom("최길동", "010-4444-4444");
            //db.Update_Custom(2, "010-1234-1234");
            //db.Delete_Custom(6);
            string phone = db.Name_to_Phone("홍길동1111");
            if(phone != null)
                Console.WriteLine("검색결과 : {0}", phone);
            else
                Console.WriteLine("없는 이름입니다.");

            int? price = db.BookName_to_Price("C#111");
            if (price != null)
                Console.WriteLine("검색결과 : {0}원", price);
            else
                Console.WriteLine("없는 이름입니다.");

            string str = db.SelectCustomers(3);
            Console.WriteLine(str);

            str = db.SelectCustomer_Info(1, 2);
            Console.WriteLine(str);
            */
            db.Close();
        }
    }
}
