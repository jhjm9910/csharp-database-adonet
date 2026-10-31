//ui.cs
using System;
using System.Collections.Generic;
using WoosonbBit.Lib;
using WoosongBit.Data;

namespace WoosongBit.UI
{
    internal class MainUi
    {
        private Control con = Control.Instance;

        #region 메서드
        public void AccountInsert()
        {
            try
            {
                Console.WriteLine("\n[계좌 개설]\n");

                int number  = Input.InputInteger("계좌번호");
                string name = Input.InputString("이름");
                int money   = Input.InputInteger("입금액");

                con.AccountInsert(number, name, money);

                Console.WriteLine("계좌 개설 성공");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void AccountSelect()
        {
            try
            {
                Console.WriteLine("\n[계좌 검색]\n");

                int number = Input.InputInteger("계좌번호");
                Account acc = con.NumberToAccount(number);
                acc.Println();                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void AccountInputMoney()
        {
            try
            {
                Console.WriteLine("\n[계좌 입금]\n");

                int number = Input.InputInteger("계좌번호");
                int money  = Input.InputInteger("입금액");

                con.AccountInputMoney(number, money);

                Console.WriteLine("계좌 입금 성공");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void AccountOutputMoney()
        {
            try
            {
                Console.WriteLine("\n[계좌 출금]\n");

                int number = Input.InputInteger("계좌번호");
                int money  = Input.InputInteger("출금액");

                con.AccountOutputMoney(number, money);

                Console.WriteLine("계좌 출금 성공");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void AccountDelete()
        {
            try
            {
                Console.WriteLine("\n[계좌 삭제]\n");

                int number = Input.InputInteger("계좌번호");

                con.AccountDelete(number);

                Console.WriteLine("계좌 삭제 성공");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void AccountPrintAll()
        {
            try
            {
                Console.WriteLine("\n[계좌 전체 리스트]\n");

                Account[] accouns = con.Accounts;
                for(int i=0; i< accouns.Length; i++)
                {
                    Account acc = accouns[i];
                    acc.Print();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void AccountNumberSort()
        {
            con.AccountNumberSort();
        }
        public void AccountNameSort()
        {
            con.AccountNameSort();
        }

        public void AccountTransMoney()
        {
            try
            {
                Console.WriteLine("\n[계좌 이체]\n");

                int input_number = Input.InputInteger("입금계좌번호");
                int output_number = Input.InputInteger("출금계좌번호");
                int money       = Input.InputInteger("이체금액");

                con.AccountTransMoney(input_number, output_number, money);

                Console.WriteLine("계좌 이체 성공");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion
    }
}
