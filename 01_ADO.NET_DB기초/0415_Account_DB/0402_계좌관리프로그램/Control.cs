//Control.cs
using System;
using System.Collections.Generic;
using System.Security.Policy;
using WoosongBit.Data;
using WoosongBit.DataBase;

namespace WoosongBit
{
    internal class Control
    {
        #region 싱글톤
        private Control() 
        {
            if (db.Open() == false)
            {
                Console.WriteLine("DB 연결 오류....");
                Environment.Exit(0);
            }
        }

        public static Control Instance { get; private set; } = null;
        static Control()
        {
            Instance = new Control();
        }
        #endregion
        
        private MyDB db = new MyDB();

        public Account[] Accounts
        {
            get
            {
                List<Account> accounts = new List<Account>();
                string str = db.SelectAccounts();
                string[] sp = str.Split('@');
                foreach (string s in sp)
                {
                    string[] sp1 = s.Split('#');
                    int id              = int.Parse(sp1[0]);
                    string name         = sp1[1];
                    int balance         = int.Parse(sp1[2]);
                    DateTime nowdate    = DateTime.Parse(sp1[3]);
                    accounts.Add(new Account(id, name, balance, nowdate));
                }
                return accounts.ToArray();
            }
        }

        #region 메서드
        public void AccountInsert(int number, string name, int balance)
        {
            try
            {
                //계좌 저장
                if (db.Insert_Account(number, name, balance) == false)
                    throw new Exception("insert 실패");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Account NumberToAccount(int number)
        {
            try
            {
                string str = db.SelectAccount(number);
                string[] sp = str.Split('#');
                int id          = int.Parse(sp[0]);
                string name     = sp[1];
                int balance     = int.Parse(sp[2]);
                DateTime nowdate = DateTime.Parse(sp[3]);

                return new Account(id, name, balance, nowdate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void AccountInputMoney(int number, int money)
        {
            try
            {
                if (db.Update_Account_Input(number, money) == false)
                    throw new Exception("입금 실패");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void AccountOutputMoney(int number, int money)
        {
            try
            {
                if (db.Update_Account_Output(number, money) == false)
                    throw new Exception("출금 실패");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void AccountDelete(int number)
        {
            try
            {
                if (db.Delete_Account(number) == false)
                    throw new Exception("삭제 실패");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Account[] AccountNumberSort()
        {            
            List<Account> accounts = new List<Account>();
            string str = db.SelectAccounts_Order_Number();
            string[] sp = str.Split('@');
            foreach (string s in sp)
            {
                string[] sp1 = s.Split('#');
                int id = int.Parse(sp1[0]);
                string name = sp1[1];
                int balance = int.Parse(sp1[2]);
                DateTime nowdate = DateTime.Parse(sp1[3]);
                accounts.Add(new Account(id, name, balance, nowdate));
            }
            return accounts.ToArray();
        }
        public Account[] AccountNameSort()
        {            
            List<Account> accounts = new List<Account>();
            string str = db.SelectAccounts_Order_Name();
            string[] sp = str.Split('@');
            foreach (string s in sp)
            {
                string[] sp1 = s.Split('#');
                int id = int.Parse(sp1[0]);
                string name = sp1[1];
                int balance = int.Parse(sp1[2]);
                DateTime nowdate = DateTime.Parse(sp1[3]);
                accounts.Add(new Account(id, name, balance, nowdate));
            }
            return accounts.ToArray();
        }

        public void AccountTransMoney(int input_number, int output_number, int money)
        {
            try
            {
                if (db.AccountTransMoney(input_number, output_number, money) == false)
                    throw new Exception("이체 실패");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
