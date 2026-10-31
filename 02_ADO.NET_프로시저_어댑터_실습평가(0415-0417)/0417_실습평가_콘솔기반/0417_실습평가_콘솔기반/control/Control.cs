//Control.cs
using System;
using System.Collections.Generic;
using WoosonbBit.Data;
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

        #region 메서드
        public void Insert_Student(string name)
        {
            try
            {
                if (db.Insert_Student(name) == false)
                    throw new Exception("학생 등록 실패");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Student> Select_Students()
        {
            try
            {
                List<Student> students = new List<Student>();
                string str = db.Select_Students();
                string[] sp = str.Split('@');
                foreach (string s in sp)
                {
                    string[] sp1 = s.Split('#');
                    int id = int.Parse(sp1[0]);
                    string name = sp1[1];
                    students.Add(new Student(id, name));
                }
                return students;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Insert_Grade(int studentID, string subject, int score)
        {
            try
            {
                if (db.Insert_Grade(studentID, subject, score) == false)
                    throw new Exception("학생 성적 등록 실패");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Update_Grade(int studentID, string subject, int score)
        {
            try
            {
                if (db.Update_Grade(studentID, subject, score) == false)
                    throw new Exception("학생 성적 수정 실패");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Grade> Select_Grades(string subject)
        {
            try
            {
                List<Grade> grades = new List<Grade>();
                string str = db.Select_Grades(subject);
                string[] sp = str.Split('@');
                foreach (string s in sp)
                {
                    string[] sp1 = s.Split('#');
                    int id          = int.Parse(sp1[0]);
                    int stuid       = int.Parse(sp1[1]);
                    string stu_name = sp1[2];
                    int score       = int.Parse(sp1[3]);

                    grades.Add(new Grade(id, stuid, stu_name, score));
                }
                return grades;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<StudentTotalSubject> Select_Student_Scores(int studentID)
        {
            try
            {
                List<StudentTotalSubject> grades = new List<StudentTotalSubject>();
                string str = db.Select_Student_Scores(studentID);
                string[] sp = str.Split('@');
                foreach (string s in sp)
                {
                    string[] sp1 = s.Split('#');
                    string stu_name    = sp1[0];
                    string subj_name   = sp1[1];
                    int score          = int.Parse(sp1[2]);
                    grades.Add(new StudentTotalSubject(stu_name, subj_name, score));
                }
                return grades;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
