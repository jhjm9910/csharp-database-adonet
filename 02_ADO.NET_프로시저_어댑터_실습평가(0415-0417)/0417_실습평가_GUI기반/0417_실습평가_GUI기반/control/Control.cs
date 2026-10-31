//Control.cs
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
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

        #region 서버 연결 및 종료
        public bool Server_Join(string server_name, string db_name, string user_id, string user_pw)
        {
            return db.Open(server_name, db_name, user_id, user_pw);
        }
        public bool Server_Drop()
        {
            return db.Close();
        }
        #endregion 

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
        
        //DataTable 반환 형태로 수정
        public DataTable Select_Students()
        {
            try
            {
                DataTable dt =  db.Select_Students();
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
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
       
        //DataTable 반환 형태로 수정
        public DataTable Select_Grades(string sub_name = "")
        {
            try
            {
                DataTable dt = db.Select_Grades(sub_name);
                if (dt.Rows.Count <= 0)
                    throw new Exception("없는 과목명입니다.");
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        //DataTable 반환 형태로 수정
        public DataTable Select_Student_Scores(int studentID)
        {
            try
            {
                DataTable dt = db.Select_Student_Scores(studentID);
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        #endregion
    }
}
