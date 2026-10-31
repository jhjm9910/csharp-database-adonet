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

        
        public void StudentInsert()
        {
            try
            {
                Console.WriteLine("\n[학생 등록]\n");
                string name = Input.InputString("이름");
                con.StudentInsert(name);
                Console.WriteLine("등록 성공");
            }
            catch (Exception ex)
            {
                Console.WriteLine("등록 실패 : " + ex.Message);
            }
        }

        
        public void StudentPrintAll()
        {
            try
            {
                Console.WriteLine("\n[학생 목록]\n");
                Console.WriteLine("학생ID\t\t학생이름");
                Console.WriteLine("------------------------");
                Student[] students = con.Students;
                foreach (Student s in students)
                    s.Print();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        
        public void GradeInsert()
        {
            try
            {
                Console.WriteLine("\n[성적 입력]\n");
                int studentID = Input.InputInteger("학생ID");
                string subject = Input.InputString("과목명");
                int score = Input.InputInteger("점수");
                con.GradeInsert(studentID, subject, score);
                Console.WriteLine("성적 입력 성공");
            }
            catch (Exception ex)
            {
                Console.WriteLine("성적 입력 실패 : " + ex.Message);
            }
        }

       
        public void GradeUpdate()
        {
            try
            {
                Console.WriteLine("\n[성적 수정]\n");
                int studentID = Input.InputInteger("학생ID");
                string subject = Input.InputString("과목명");
                int score = Input.InputInteger("점수");
                con.GradeUpdate(studentID, subject, score);
                Console.WriteLine("성적 수정 성공");
            }
            catch (Exception ex)
            {
                Console.WriteLine("성적 수정 실패 : " + ex.Message);
            }
        }

        public void GradePrintBySubject()
        {
            try
            {
                Console.WriteLine("\n[과목별 성적 조회]\n");
                string subject = Input.InputString("과목명");  // ← 과목ID 대신 과목명!
                Grade[] grades = con.GetGradesBySubject(subject);

                if (grades.Length == 0)
                {
                    Console.WriteLine("해당 과목의 성적이 없습니다.");
                    return;
                }

                Console.WriteLine("\n과목ID\t학생ID\t과목명\t\t점수");
                Console.WriteLine("----------------------------------------");
                foreach (Grade g in grades)
                    g.Print();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        public void GradePrintByStudentID()
        {
            try
            {
                Console.WriteLine("\n[학생별 성적 조회]\n");
                int studentID = Input.InputInteger("학생ID");
                string[] rows = con.GetGradesByStudentID(studentID);

                Console.WriteLine("\n학생명\t\t과목명\t\t점수");
                Console.WriteLine("----------------------------------------");
                foreach (string row in rows)
                {
                    string[] col = row.Split('#');
                    Console.WriteLine("{0}\t\t{1}\t\t{2}", col[0], col[1], col[2]);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
