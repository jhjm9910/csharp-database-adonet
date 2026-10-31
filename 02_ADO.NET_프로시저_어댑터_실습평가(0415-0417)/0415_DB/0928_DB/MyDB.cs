//MyDB.cs
using System;
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

        #region Account관련 기능
        //insert into Account  values('허길동', 50000, GetDate());
        public bool Insert_Account(int number, string name, int money)
        {
            try
            {
                string sql1 = $"insert into Account values({number}, '{name}', {money}, GetDate())";
                string sql2 = $"insert into [Transaction] values({number}, {money}, 0, {money}, GetDate())";
                ExecCommand(sql1, sql2);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        //select* from Account;
        public string SelectAccounts()
        {
            try
            {
                string sql = $"select* from Account;";
                return ExecReaderCommand(sql);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        //select* from Account where number = 1000;
        public string SelectAccount(int number)
        {
            try
            {
                string sql = $"select* from Account where number = {number};";
                return ExecReaderCommand(sql);

                //거래 내역 테이블 select
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        //update Account set balance = balance + 5000 where number = 1000;
        public bool Update_Account_Input(int number, int money)
        {
            try
            {
                int balance = (int)ExecScalarCommand($"select balance from Account where number = {number}") + money;
                string sql1 = $"update Account set balance = balance + {money} where number = {number}";
                string sql2 = $"insert into [Transaction] values({number}, {money}, 0, {balance}, GetDate())";
                ExecCommand(sql1, sql2);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        //update Account set balance = balance - 5000 where number = 1000;
        public bool Update_Account_Output(int number, int money)
        {
            try
            {
                int balance = (int)ExecScalarCommand($"select balance from Account where number = {number}") - money;
                string sql1 = $"update Account set balance = balance - {money} where number = {number}";
                string sql2 = $"insert into [Transaction] values({number}, 0, {money}, {balance}, GetDate())";
                ExecCommand(sql1, sql2);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        //delete Account where number = 1000;
        public bool Delete_Account(int number)
        {
            try
            {
                string sql1 = $"delete [Transaction] where number = {number}";
                string sql2 = $"delete Account where number = {number}";
                ExecCommand(sql1, sql2);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        //select * from Account order by number desc; --asc
        public string SelectAccounts_Order_Number()
        {
            try
            {
                string sql = $"select * from Account order by number desc;";
                return ExecReaderCommand(sql);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        //select* from Account order by name; 
        public string SelectAccounts_Order_Name()
        {
            try
            {
                string sql = $"select* from Account order by name;";
                return ExecReaderCommand(sql);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
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
        private void ExecCommand(params string[] sqls)
        {
            SqlTransaction tran = scon.BeginTransaction();
            try
            {
                foreach (string sql in sqls)
                {
                    SqlCommand cmd = new SqlCommand(sql, scon, tran);
                    if (cmd.ExecuteNonQuery() == 0) throw new Exception("row데이터 변경 없다");
                    cmd.Dispose();
                }
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                throw ex;
            }
        }
        #endregion

        #region 2.2 (프로시저 용 명령 함수)
        private void ExecProcCommand(string procName, params (string, object)[] parameters)
        {
            SqlCommand cmd = new SqlCommand(procName, scon);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                foreach (var (name, value) in parameters)
                    cmd.Parameters.AddWithValue(name, value);
                cmd.ExecuteNonQuery();
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

        #region Transaction 관련 기능
        // insert into Transaction values(number, input, output, balance, GetDate())
        
        // select * from Transaction where number = 111
        public string SelectTransactions(int number)
        {
            try
            {
                string sql = $"select * from [Transaction] where number = {number}";
                return ExecReaderCommand(sql);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        #endregion

        public bool Proc_Insert_Account(int number, string name, int money)
        {
            try
            {
                ExecProcCommand("proc_Insert_Account",
                    ("@number", number),
                    ("@name", name),
                    ("@money", money));
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool Proc_Input_Account(int number, int money)
        {
            try
            {
                ExecProcCommand("proc_Input_Account",
                    ("@number", number),
                    ("@money", money));
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool Proc_Output_Account(int number, int money)
        {
            try
            {
                ExecProcCommand("proc_Output_Account",
                    ("@number", number),
                    ("@money", money));
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool Proc_Delete_Account(int number)
        {
            try
            {
                ExecProcCommand("proc_Delete_Account",
                    ("@number", number));
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
