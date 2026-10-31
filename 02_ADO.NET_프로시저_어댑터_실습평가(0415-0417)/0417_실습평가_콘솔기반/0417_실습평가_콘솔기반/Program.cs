//Program.cs
using System;
using WoosonbBit.Lib;
using WoosongBit.UI;

namespace WoosongBit
{
    internal class Program
    {
        private Ui ui = new Ui();

        public void InitInstance()
        {
            Print.Logo();
        }
        public void Run()
        {
            while (true)
            {
                Console.Clear();
                switch (Print.MenuPrint())
                {
                    case ConsoleKey.Escape:                         return;
                    case ConsoleKey.F1: ui.Insert_Student();        break;
                    case ConsoleKey.F2: ui.Select_Students();       break;
                    case ConsoleKey.F3: ui.Insert_Grade();          break;
                    case ConsoleKey.F4: ui.Update_Grade();          break;
                    case ConsoleKey.F5: ui.Select_Grades();         break;
                    case ConsoleKey.F6: ui.Select_Student_Scores(); break;
                   
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
