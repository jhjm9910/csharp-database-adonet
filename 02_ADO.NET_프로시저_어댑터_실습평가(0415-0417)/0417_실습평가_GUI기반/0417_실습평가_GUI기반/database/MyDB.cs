//MyDB.cs
using System;
using System.Data;
using System.Data.SqlClient;  //MS SQL

namespace WoosongBit.DataBase
{
    //p102
    internal class MyDB
    {
        #region 서버 연결 정보
        private const string SERVER_NAME = "localhost\\SQLEXPRESS";
        private const string DB_NAME = "WB43";
        private const string USER_ID = "JJH";
        private const string USER_PW = "YOUR_PASSWORD";
        #endregion

        private SqlConnection scon = null;

        #region 1. SQlConnection (연결, 속성, 종료)
        public bool Open()
        {
            try
            {
                string constr = $"Data Source={SERVER_NAME};Initial Catalog={DB_NAME}; User ID={USER_ID};Password={USER_PW}";
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
        public bool Open(string server_name, string db_name, string user_id, string user_pw)
        {
            try
            {
                string constr = $"Data Source={server_name};Initial Catalog={db_name}; User ID={user_id};Password={user_pw}";
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
        public void ServerInfo()
        {
            if (scon.State == System.Data.ConnectionState.Open)
            {
                Console.WriteLine(scon.WorkstationId);
                Console.WriteLine(scon.ServerVersion);
                Console.WriteLine(scon.PacketSize);
                Console.WriteLine(scon.ConnectionTimeout);
                Console.WriteLine(scon.Database);
                Console.WriteLine(scon.DataSource);
                Console.WriteLine(scon.State);
            }
            else
            {
                Console.WriteLine("서버 상태 확인");
            }
        }

        public bool Close()
        {
            // if(scon.State == System.Data.ConnectionState.Open)
            //     scon.Close();
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

        #region 기능
        //기능1) insert into Students values('홍길동');
        public bool Insert_Student(string name)
        {
            try
            {
                string sql = $"insert into Students values('{name}');";
                ExecCommand(sql);
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        //기능2) 아답터 사용 기능으로 변경
        public DataTable Select_Students()
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dt = new DataTable("Students");
            adapter.SelectCommand = new SqlCommand("select * from students", scon);
            adapter.Fill(dt);
            return dt;
        }

        //기능3) insert into Grades values(1, '국어', 100);
        public bool Insert_Grade(int studentID, string subject, int score)
        {
            try
            {
                string sql = $"insert into Grades values({studentID}, '{subject}', {score});";
                ExecCommand(sql);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        //기능4) update Grades set Score = 90 where studentID = 1 and subject = '국어';
        public bool Update_Grade(int studentID, string subject, int score)
        {
            try
            {
                string sql = $"update Grades set Score = {score} where studentID = {studentID} and subject = '{subject}';";
                ExecCommand(sql);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        //기능5) 아답터 사용 기능으로 변경
        public DataTable Select_Grades(string sub_name = "")
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dt = new DataTable("Grades");
            if(sub_name == "")
                adapter.SelectCommand = new SqlCommand("select * from grades", scon);
            else
                adapter.SelectCommand = new SqlCommand($"select * from grades where subject = '{sub_name}'", scon);
            adapter.Fill(dt);
            return dt;
        }

        //기능6) 아답터 사용 기능으로 변경
        public DataTable Select_Student_Scores(int studentID)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dt = new DataTable("Grades");
            string sql = $"select name, subject, score from Students, Grades where Students.studentID = Grades.studentID and Students.studentID = {studentID};";
            adapter.SelectCommand = new SqlCommand(sql, scon);
            adapter.Fill(dt);
            return dt;
        }
        #endregion

        #region 3. 반환 정보가 하나인 상황
        //select phone from Custom where cname = '홍길동';



        private string Name_to_Phone(string name)
        {
            try
            {
                string sql = $"select phone from Custom where cname = '{name}';";
                return (string)ExecScalarCommand(sql);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return string.Empty;
            }
        }

        //select price from Product where pname = 'C#';
        public int? BookName_to_Price(string name)
        {
            try
            {
                string sql = $"select price from Product where pname = '{name}';";
                return (int?)ExecScalarCommand(sql);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        //select sum(price) from Product;
        public int? GetProduct_PriceSum()
        {
            try
            {
                string sql = $"select sum(price) from Product;";
                return (int?)ExecScalarCommand(sql);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        #endregion
              
        #region 2.1(Insert, Update, Delete 용 명령 함수)
        private void ExecCommand(string sql)
        {
            SqlCommand cmd = null;
            try
            {
                cmd = new SqlCommand(sql, scon);
                if (cmd.ExecuteNonQuery() == 0)
                {
                    throw new Exception("row데이터 변경 없다");
                }               
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
            }
        }
        #endregion 

        #region 3.1(하나의 값을 획득하기 위한 명령 함수)
        private object ExecScalarCommand(string sql)
        {
            SqlCommand cmd = null;
            try
            {
                cmd = new SqlCommand(sql, scon);
                return cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
            }
        }
        #endregion 

        #region 4.1(select 용 명령 함수)
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
                     {
                            str += reader[i] + "#";
                     }
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
                cmd.Dispose();
            }
        }
        #endregion 
    }
}
