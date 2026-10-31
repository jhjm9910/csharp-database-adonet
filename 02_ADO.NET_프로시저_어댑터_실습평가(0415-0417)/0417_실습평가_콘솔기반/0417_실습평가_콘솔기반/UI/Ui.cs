//ui.cs
using System;
using System.Collections.Generic;
using WoosonbBit.Data;
using WoosonbBit.Lib;

namespace WoosongBit.UI
{
    internal class Ui
    {
        private Control con = Control.Instance;

        #region 메서드
        public void Insert_Student()
        {
            try
            {
                Console.WriteLine("\n[기능1. 학생 등록]\n");

                string name = Input.InputString("이름");

                con.Insert_Student(name);

                Console.WriteLine("학생 등록 성공");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void Select_Students()
        {
            try
            {
                Console.WriteLine("\n[기능2. 등록된 모든 학생의 정보 출력]\n");

                List<Student> students = con.Select_Students();
                Console.WriteLine("학생ID \t\t 학생이름");
                Console.WriteLine("---------------------------------");
                foreach(Student stu in students)
                {
                    Console.WriteLine(stu);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void Insert_Grade()
        {
            try
            {
                Console.WriteLine("\n[기능3. 성적 입력]\n");

                int stu_id          = Input.InputInteger("학생 ID");
                string subject_name = Input.InputString("과목명");
                int score           = Input.InputInteger("점수");

                con.Insert_Grade(stu_id, subject_name, score);

                Console.WriteLine("성적 입력 성공");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void Update_Grade()
        {
            try
            {
                Console.WriteLine("\n[기능4. 성적 수정]\n");

                int stu_id = Input.InputInteger("학생 ID");
                string subject_name = Input.InputString("과목명");
                int score = Input.InputInteger("점수");

                con.Update_Grade(stu_id, subject_name, score);

                Console.WriteLine("성적 수정 성공");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void Select_Grades()
        {
            try
            {
                Console.WriteLine("\n[기능5. 등록된 성적 중 특정 과목에 대한 점수 출력]\n");

                string  subject_name = Input.InputString("과목명");

                List<Grade> grades = con.Select_Grades(subject_name);
                Console.WriteLine("과목ID \t\t 학생ID \t 과목명 \t 점수");
                Console.WriteLine("---------------------------------------------------------------------------");
                foreach (Grade grade in grades)
                {
                    Console.WriteLine(grade);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void Select_Student_Scores()
        {
            try
            {
                Console.WriteLine("\n[기능6. 학생별 성적 조회]\n");

                int stu_id = Input.InputInteger("학생 ID");

                List<StudentTotalSubject> sts = con.Select_Student_Scores(stu_id);
                Console.WriteLine("학생명 \t\t 과목명 \t 점수");
                Console.WriteLine("-----------------------------------------------");
                foreach (StudentTotalSubject st in sts)
                {
                    Console.WriteLine(st);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion
    }
}
