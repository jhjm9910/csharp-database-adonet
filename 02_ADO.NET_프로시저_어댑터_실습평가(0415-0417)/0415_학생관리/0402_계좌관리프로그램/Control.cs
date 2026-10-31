//Control.cs
using System;
using System.Collections.Generic;
using System.Security.Policy;
using WoosongBit.Data;
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

        #region Students
        public void StudentInsert(string name)
        {
            try
            {
                if (db.Insert_Student(name) == false)
                    throw new Exception("학생 등록 실패");
            }
            catch (Exception ex) { throw ex; }
        }

        public Student[] Students
        {
            get
            {
                List<Student> list = new List<Student>();
                string str = db.SelectStudents();
                if (string.IsNullOrEmpty(str)) return list.ToArray();

                string[] rows = str.Split('@');
                foreach (string row in rows)
                {
                    if (string.IsNullOrEmpty(row)) continue; 
                    string[] col = row.Split('#');
                    if (col.Length < 2) continue;            
                    int id = int.Parse(col[0]);
                    string name = col[1];
                    list.Add(new Student(id, name));
                }
                return list.ToArray();
            }
        }
        #endregion

        #region Grades
        public void GradeInsert(int studentID, string subject, int score)
        {
            try
            {
                if (db.Insert_Grade(studentID, subject, score) == false)
                    throw new Exception("성적 입력 실패");
            }
            catch (Exception ex) { throw ex; }
        }

        public void GradeUpdate(int studentID, string subject, int score)
        {
            try
            {
                if (db.Update_Grade(studentID, subject, score) == false)
                    throw new Exception("성적 수정 실패");
            }
            catch (Exception ex) { throw ex; }
        }

        public Grade[] GetGradesBySubject(string subject)
        {
            try
            {
                List<Grade> list = new List<Grade>();
                string str = db.SelectGradesBySubject(subject);
                if (string.IsNullOrEmpty(str)) return list.ToArray();

                string[] rows = str.Split('@');
                foreach (string row in rows)
                {
                    string[] col = row.Split('#');
                    int gid = int.Parse(col[0]);
                    int sid = int.Parse(col[1]);
                    string subj = col[2];
                    int score = int.Parse(col[3]);
                    list.Add(new Grade(gid, sid, subj, score));
                }
                return list.ToArray();
            }
            catch (Exception ex)
            { 
                throw ex; 
            }
        }

        public string[] GetGradesByStudentID(int studentID)
        {
            try
            {
                string str = db.SelectGradesByStudentID(studentID);
                if (string.IsNullOrEmpty(str)) return new string[0];
                return str.Split('@');
            }
            catch (Exception ex) { throw ex; }
        }
        #endregion
    }
}
