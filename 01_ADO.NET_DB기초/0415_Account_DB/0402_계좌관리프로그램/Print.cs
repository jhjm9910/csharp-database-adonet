//Print.cs
using System;

namespace WoosonbBit.Lib
{
    internal static class Print
    {
        public static void Pause()
        {
            Console.Write("\n\n엔터키를 누르세요..........");
            Console.ReadKey(true);
        }
        public static void Logo()
        {
            Console.Clear();
            Console.WriteLine("*************************************************************************");
            Console.WriteLine(" 2026학년도 1학기 비트 고급 과정(43기)");
            Console.WriteLine(" C#언어");
            Console.WriteLine(" 콘솔 기반 계좌 관리 프로그램");
            Console.WriteLine(" 2026-04-03");
            Console.WriteLine(" ccm");
            Console.WriteLine("*************************************************************************");
            Pause();
        }
        public static ConsoleKey MenuPrint()
        {
            Console.WriteLine("*************************************************************************");
            Console.WriteLine("[ESC] 프로그램 종료");
            Console.WriteLine("[F1] 계좌 저장");
            Console.WriteLine("[F2] 계좌 검색(계좌번호,Uniq)");
            Console.WriteLine("[F3] 계좌 수정(입금)");
            Console.WriteLine("[F4] 계좌 수정(출금)");
            Console.WriteLine("[F5] 계좌 삭제");
            Console.WriteLine("[F6] 계좌 정렬-계좌번호");
            Console.WriteLine("[F7] 계좌 정렬-이름");
            Console.WriteLine("[F8] 계좌 이체");
            Console.WriteLine("*************************************************************************");
            return Console.ReadKey(true).Key;
        }
        public static void Ending()
        {
            Console.Clear();
            Console.WriteLine("*************************************************************************");
            Console.WriteLine(" 프로그램을 종료합니다.");
            Console.WriteLine("*************************************************************************");
            Pause();
        }
    }
}
