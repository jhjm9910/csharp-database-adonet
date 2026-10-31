//MyDB.cs
using System;
using System.CodeDom;
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
                string constr = $"Data Source={SERVER_NAME};Initial Catalog={DB_NAME}; User ID={USER_ID};Password={USER_PW};TrustServerCertificate=True;";
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

        #region 2. Insert , Update, Delete
        //insert into Custom  values('허길동', '010-7777-7777');
        public bool Insert_Custom(string name, string phone)
        {
            try
            {
                string sql = $"insert into Custom  values('{name}', '{phone}');";
                ExecCommand(sql);
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        //update Custom set phone='010-1234-1234' where cid = 4;
        public bool Update_Custom(int cid, string phone)
        {
            try
            {
                string sql = $"update Custom set phone='{phone}' where cid = {cid}";
                ExecCommand(sql);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        //delete Custom where cid=4;
        public bool Delete_Custom(int cid)
        {
            try
            {
                string sql = $"delete Custom where cid={cid};";
                ExecCommand(sql);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        #endregion

        #region 3. 반환 정보가 하나인 상황
        //select phone from Custom where cname = '홍길동';
        public string Name_to_Phone(string name)
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

        #region 4. select
        //select * from Custom;
        public string SelectCustomers(int colums)
        {
            try
            {
                string sql = $"select * from Custom;";
                return ExecReaderCommand(sql, colums);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        //select cname, phone from Custom where cid = 1;
        public string SelectCustomer_Info(int cid, int colums)
        {
            try
            {
                string sql = $"select cname, phone from Custom where cid = {cid}";
                return ExecReaderCommand(sql, colums);
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
        private string ExecReaderCommand(string sql, int colums)
        {
            SqlCommand cmd = null;
            try
            {
                cmd = new SqlCommand(sql, scon);
                SqlDataReader reader = cmd.ExecuteReader();
                string str = string.Empty;
                while (reader.Read())
                {
                     for (int i = 0; i < colums; i++)
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
