//MyDB.cs
using System;
using System.Data.SqlClient;  //MS SQL

namespace WoosongBit.DataBase
{
    //p102
    internal class MyDB
    {
        #region 서버 연결 정보
        private const string SERVER_NAME = "DESKTOP-0I86BTV\\SQLEXPRESS";
        private const string DB_NAME = "WB43";
        private const string USER_ID = "ccm";
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
        // @number int,  @name nvarchar(50), @money int
        public bool Insert_Account(int number, string name, int money)
        {
            try
            {
                string sql = "Insert_Account";

                SqlCommand command = new SqlCommand(sql, scon);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                #region 파라미터 등록
                SqlParameter param_number = new SqlParameter("@number", number);
                param_number.SqlDbType = System.Data.SqlDbType.Int;
                command.Parameters.Add(param_number);

                SqlParameter param_name = new SqlParameter("@name", name);
                command.Parameters.Add(param_name);

                SqlParameter param_money = new SqlParameter("@money", money);
                param_money.SqlDbType = System.Data.SqlDbType.Int;
                command.Parameters.Add(param_money);
                #endregion

                ExecCommand(command);
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
                string sql = "Select_Accounts";

                SqlCommand command = new SqlCommand(sql, scon);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                return ExecReaderCommand(command);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        //@number int
        public string SelectAccount(int number)
        {
            try
            {
                string sql = "Select_Account";

                SqlCommand command = new SqlCommand(sql, scon);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter param_number = new SqlParameter("@number", number);
                param_number.SqlDbType = System.Data.SqlDbType.Int;
                command.Parameters.Add(param_number);

                return ExecReaderCommand(command);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        // @number int,  @money int
        public bool Update_Account_Input(int number, int money)
        {
            try
            {
                string sql = "Update_Account_Input";

                SqlCommand command = new SqlCommand(sql, scon);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                #region 파라미터 등록
                SqlParameter param_number = new SqlParameter("@number", number);
                param_number.SqlDbType = System.Data.SqlDbType.Int;
                command.Parameters.Add(param_number);

                SqlParameter param_money = new SqlParameter("@money", money);
                param_money.SqlDbType = System.Data.SqlDbType.Int;
                command.Parameters.Add(param_money);
                #endregion

                ExecCommand(command);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        // @number int,  @money int;
        public bool Update_Account_Output(int number, int money)
        {
            try
            {
                string sql = "Update_Account_Output";

                SqlCommand command = new SqlCommand(sql, scon);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                #region 파라미터 등록
                SqlParameter param_number = new SqlParameter("@number", number);
                param_number.SqlDbType = System.Data.SqlDbType.Int;
                command.Parameters.Add(param_number);

                SqlParameter param_money = new SqlParameter("@money", money);
                param_money.SqlDbType = System.Data.SqlDbType.Int;
                command.Parameters.Add(param_money);
                #endregion

                ExecCommand(command);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        //@number int
        public bool Delete_Account(int number)
        {
            try
            {
                string sql = "Delete_Account";

                SqlCommand command = new SqlCommand(sql, scon);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter param_number = new SqlParameter("@number", number);
                param_number.SqlDbType = System.Data.SqlDbType.Int;
                command.Parameters.Add(param_number);

                ExecCommand(command);
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
        private void ExecCommand(SqlCommand cmd)
        {
            try
            {
                int ret = cmd.ExecuteNonQuery();
                if (ret == 0)
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
        private string ExecReaderCommand(SqlCommand cmd)
        {
            try
            {
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
