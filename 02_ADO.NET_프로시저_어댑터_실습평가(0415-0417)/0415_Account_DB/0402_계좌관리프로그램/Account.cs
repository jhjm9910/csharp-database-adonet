//Account.cs
//맴버필드 + 프로퍼티 -> 생성자&소멸자 -> 메서드
using System;
using System.Collections.Generic;

namespace WoosongBit.Data
{
    internal class Account : IComparable<Account>
    {
        public int Number { get; private set; }
        public string Name { get; private set; }
        public int Balance { get; private set; }
        public DateTime NowDate { get; private set; }

        #region 생성자
        public Account(int _number, string _name, int _balance)
        {
            Number = _number;
            Name = _name;
            Balance = _balance;
            NowDate = DateTime.Now;
        }

        public Account(int _number, string _name, int _balance, DateTime _nowdate)
        {
            Number = _number;
            Name = _name;
            Balance = _balance;
            NowDate = _nowdate;
        }
        #endregion

        #region 메서드
        public void InputMoney(int money)
        {
            if (money <= 0)
                throw new Exception("잘못된 금액이 전달됨");

            Balance = Balance + money;
        }
        public void OutputMoney(int money)
        {
            if (money <= 0)
                throw new Exception("잘못된 금액이 전달됨");

            if (Balance < money)
                throw new Exception("잔액이 부족합니다.");

            Balance = Balance - money;
        }
        public void Print()
        {
            Console.Write(Number + "\t");
            Console.Write(Name + "\t");
            Console.Write(Balance + "원\t");
            Console.WriteLine(NowDate.ToString());
        }
        public void Println()
        {
            Console.WriteLine("[번    호] {0}", Number);
            Console.WriteLine("[이    름] {0}", Name);
            Console.WriteLine("[잔    액] {0}원", Balance);
            Console.WriteLine("[개설일자] {0}", NowDate.ToShortDateString());
            Console.WriteLine("[개설시간] {0}", NowDate.ToShortTimeString());
        }

        #endregion

        #region IComparable 구현상속
        public int CompareTo(Account other)
        {
            return Number - other.Number;
        }
        #endregion
    }

    /// <summary>
    /// 추가 정렬 기준 제시
    /// </summary>
    internal class AccountNameComparer : IComparer<Account>
    {
        public int Compare(Account x, Account y)
        {
            //오름차순
            //return x.Name.CompareTo(y.Name);
            //내림차순
            return y.Name.CompareTo(x.Name);
        }
    }
}
