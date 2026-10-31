// MyDB.cs
using System;
using System.Data.SqlClient;

namespace WoosongBit.DataBase
{
    internal class MyDB
    {
        #region 서버 연결 정보
        private const string SERVER_NAME = "localhost\\SQLEXPRESS";
        private const string DB_NAME = "WB43";
        private const string USER_ID = "JJH";
        private const string USER_PW = "YOUR_PASSWORD";
        #endregion

        private SqlConnection scon = null;

        #region 연결 / 종료
        public bool Open()
        {
            try
            {
                string constr = $"Data Source={SERVER_NAME};Initial Catalog={DB_NAME};User ID={USER_ID};Password={USER_PW}";
                scon = new SqlConnection(constr);
                scon.Open();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool Close()
        {
            try
            {
                scon.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        #endregion

        #region Students 관련
        
        public bool Insert_Student(string name)
        {
            try
            {
                string sql = $"insert into Students(name) values('{name}')";
                ExecCommand(sql);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        
        public string SelectStudents()
        {
            try
            {
                string sql = "select studentID, name from Students;";
                return ExecReaderCommand(sql);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        #endregion

        #region Grades 관련
        
        public bool Insert_Grade(int studentID, string subject, int score)
        {
            try
            {
                string sql = $"insert into Grades(studentID, subject, score) values({studentID}, '{subject}', {score})";
                ExecCommand(sql);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        
        public bool Update_Grade(int studentID, string subject, int score)
        {
            try
            {
                string sql = $"update Grades set score = {score} where studentID = {studentID} and subject = '{subject}'";
                ExecCommand(sql);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }


        public string SelectGradesBySubject(string subject)
        {
            try
            {
                string sql = $"select gradeID, studentID, subject, score from Grades where subject = '{subject}'";
                return ExecReaderCommand(sql);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public string SelectGradesByStudentID(int studentID)
        {
            try
            {
                string sql = $@"select s.name, g.subject, g.score from Students s join Grades g on s.studentID = g.studentID where s.studentID = {studentID}";
                return ExecReaderCommand(sql);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        #endregion

        #region 내부 공통 명령 함수
        private void ExecCommand(string sql)
        {
            SqlCommand cmd = null;
            try
            {
                cmd = new SqlCommand(sql, scon);
                if (cmd.ExecuteNonQuery() == 0)
                    throw new Exception("row 데이터 변경 없음");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd?.Dispose();
            }
        }

        private string ExecReaderCommand(string sql)
        {
            SqlCommand cmd = null;
            try
            {
                cmd = new SqlCommand(sql, scon);
                SqlDataReader reader = cmd.ExecuteReader();
                string str = string.Empty;
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                        str += reader[i] + "#";
                    str = str.TrimEnd('#') + '@';
                }
                str = str.TrimEnd('@');
                reader.Close();
                return str;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd?.Dispose();
            }
        }
        #endregion
    }
}