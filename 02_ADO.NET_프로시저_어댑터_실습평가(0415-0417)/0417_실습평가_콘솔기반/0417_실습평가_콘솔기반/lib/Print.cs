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
            Console.WriteLine(" 콘솔 기반 성적 관리 프로그램(실기 평가)");
            Console.WriteLine(" 2026-04-17");
            Console.WriteLine(" ccm");
            Console.WriteLine("*************************************************************************");
            Pause();
        }
        public static ConsoleKey MenuPrint()
        {
            Console.WriteLine("*************************************************************************");
            Console.WriteLine("[ESC] 프로그램 종료");
            Console.WriteLine("[F1] 학생 등록");
            Console.WriteLine("[F2] 모든 학생의 정보 출력");
            Console.WriteLine("[F3] 성적 입력");
            Console.WriteLine("[F4] 성적 수정");
            Console.WriteLine("[F5] 특정 과목에 대한 점수 출력");
            Console.WriteLine("[F6] 학생별 성적 조회");
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
