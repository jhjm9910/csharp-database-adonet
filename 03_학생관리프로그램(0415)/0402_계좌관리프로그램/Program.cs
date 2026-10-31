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
                ui.StudentPrintAll();
                switch (Print.MenuPrint())
                {
                    case ConsoleKey.Escape: return;
                    case ConsoleKey.F1: ui.StudentInsert(); break;
                    case ConsoleKey.F2: ui.StudentPrintAll(); break;
                    case ConsoleKey.F3: ui.GradeInsert(); break;
                    case ConsoleKey.F4: ui.GradeUpdate(); break;
                    case ConsoleKey.F5: ui.GradePrintBySubject(); break;
                    case ConsoleKey.F6: ui.GradePrintByStudentID(); break;
                    default: Console.WriteLine("잘못 입력하셨습니다."); break;
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
