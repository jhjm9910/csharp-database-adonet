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
        //insert into Account  values(1000, '허길동', 50000, GetDate());
        public bool Insert_Account(int number, string name, int money)
        {
            try
            {
                string sql = $"insert into Account  values({number},'{name}', {money}, GetDate())";
                ExecCommand(sql);
                return true;
            }
            catch(Exception ex)
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
                string sql = $"update Account set balance = balance + {money} where number = {number};";
                ExecCommand(sql);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        // update Account set balance = balance - 5000 where number = 222 and balance >5000;
        public bool Update_Account_Output(int number, int money)
        {
            try
            {
                string sql = $"update Account set balance = balance - {money} where number = {number} and balance >{money};";
                ExecCommand(sql);
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
                string sql = $"delete Account where number = {number};";
                ExecCommand(sql);
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
        //트랜젝션
        //update Account set balance = balance - 1000 where number = 111 and balance >1000;
        //update Account set balance = balance + 1000 where number = 222;
        public bool AccountTransMoney(int input_number, int output_number, int money)
        {
            SqlTransaction transaction = scon.BeginTransaction();
            try
            { 
                string sql = $"update Account set balance = balance - {money} where number = {output_number} and balance >{money};";
                SqlCommand cmd1 = new SqlCommand(sql, scon, transaction);

                sql = $"update Account set balance = balance + {money} where number = {input_number};";
                SqlCommand cmd2 = new SqlCommand(sql, scon, transaction);

                if (cmd1.ExecuteNonQuery() != 1)
                    throw new Exception("오류");

                if (cmd2.ExecuteNonQuery() != 1)
                    throw new Exception("오류");

                transaction.Commit(); // 성공

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                transaction.Rollback(); // 실패 시 롤백

                return false;
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
