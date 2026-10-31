//Program.cs
using System;
using WoosonbBit.Lib;
using WoosongBit.UI;

namespace WoosongBit
{
    internal class Program
    {
        private MainUi ui = new MainUi();

        public void InitInstance()
        {
            Print.Logo();
        }
        public void Run()
        {
            while (true)
            {
                Console.Clear();
                ui.AccountPrintAll();
                switch (Print.MenuPrint())
                {
                    case ConsoleKey.Escape:                     return;
                    case ConsoleKey.F1: ui.AccountInsert();     break;
                    case ConsoleKey.F2: ui.AccountSelect();     break;
                    case ConsoleKey.F3: ui.AccountInputMoney(); break;
                    case ConsoleKey.F4: ui.AccountOutputMoney();break;
                    case ConsoleKey.F5: ui.AccountDelete();     break;
                    case ConsoleKey.F6: ui.AccountNumberSort(); break;
                    case ConsoleKey.F7: ui.AccountNameSort();   break;
                    case ConsoleKey.F8: ui.AccountTransMoney();    break;
                    default: Console.WriteLine("잘못 입력하셨습니다.");break;
                }
                Print.Pause();
            }
        }
        public void ExitInstance()
        {
            Print.Ending();
        }

        static void Main(string[] args)
        {
            Program p = new Program();
            p.InitInstance();
            p.Run();
            p.ExitInstance();
        }
    }
}
