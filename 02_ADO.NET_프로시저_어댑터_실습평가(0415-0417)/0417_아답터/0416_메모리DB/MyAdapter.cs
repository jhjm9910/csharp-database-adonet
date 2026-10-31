//MyAdapter.cs
using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Policy;
using System.Windows.Forms;
using System.Xml.Linq;

namespace _0416_메모리DB
{
    internal class MyAdapter
    {
        #region 서버 연결 정보
        private const string SERVER_NAME = "localhost\\SQLEXPRESS";
        private const string DB_NAME = "WB43";
        private const string USER_ID = "JJH";
        private const string USER_PW = "YOUR_PASSWORD";
        #endregion

        private SqlConnection scon      = null;
        private SqlDataAdapter adapter  = new SqlDataAdapter();
        private DataSet wb43            = new DataSet("wb43");

        public DataTable this[string name]
        {
            get { return wb43.Tables[name]; }
        }

        public MyAdapter()
        {
            try
            {
                string constr = $"Data Source={SERVER_NAME};Initial Catalog={DB_NAME}; User ID={USER_ID};Password={USER_PW}";
                scon = new SqlConnection(constr);

                adapter.SelectCommand = MakeSelectCommand(scon);
                adapter.InsertCommand = MakeInsertCommand(scon);
                adapter.UpdateCommand = MakeUpdateCommand(scon);
                adapter.DeleteCommand = MakeDeleteCommand(scon);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "서버 연결");
                Environment.Exit(0); //종료!
            }
        }                

        #region SqlCommand 등록
        private static SqlCommand MakeSelectCommand(SqlConnection con)
        {
            string cmd_text = "select * from Account";
            SqlCommand cmd = new SqlCommand(cmd_text, con);            
            return cmd;
        }
        //insert into Account values(123, '공길동', 100, GetDate());
        private static SqlCommand MakeInsertCommand(SqlConnection con)
        {
            string cmd_text = "insert into Account values(@Number, @Name, @Balance, GetDate())";
            SqlCommand cmd = new SqlCommand(cmd_text, con);            
            cmd.Parameters.Add("@Number", SqlDbType.Int, 4, "number");
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 50, "Name");
            cmd.Parameters.Add("@Balance", SqlDbType.Int, 4, "balance"); 

            return cmd;
        }
        //update Account set balance = 5000 where number = 11;
        private static SqlCommand MakeUpdateCommand(SqlConnection con)
        {
            string cmd_text = "update Account set balance = @Balance where number = @Number;";
            SqlCommand cmd = new SqlCommand(cmd_text, con);
            cmd.Parameters.Add("@Balance", SqlDbType.Int, 4, "balance");
            cmd.Parameters.Add("@Number", SqlDbType.Int, 4, "number");
            return cmd;
        }
        //delete  Account where number = 111;
        private static SqlCommand MakeDeleteCommand(SqlConnection con)
        {
            string cmd_text = "delete  Account where number = @Number;";
            SqlCommand cmd = new SqlCommand(cmd_text, con);
            cmd.Parameters.Add("@Number", SqlDbType.Int, 4, "number");
            return cmd;
        }
        #endregion

        public bool Fill_Account()
        {
            try
            {
                //Open() -> "select쿼리문" -> Close()
                adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                adapter.Fill(wb43, "Account");               
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        public bool Update_Account()
        {
            try
            {
                //Open() -> "insert or update or delete 쿼리문" -> Close()
                adapter.Update(wb43, "Account");
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        public string Table_ViewAll(string table_name)
        {
            DataTable dt = wb43.Tables[table_name];

            string tmp = string.Empty;
            tmp += "테이블 명 : " + dt.TableName + "\r\n";
            tmp += "컬럼 개수 : " + dt.Columns.Count + "\r\n";
            tmp += "로우 데이터 개수 : " + dt.Rows.Count + "\r\n";
            tmp += "기본 키 : " + dt.PrimaryKey[0].ColumnName + "\r\n";
            tmp += "-------------------------" + "\r\n";

            foreach (DataColumn dc in dt.Columns)
            {
                tmp += dc.ColumnName + ", " + dc.DataType + ", " + dc.AllowDBNull + "\r\n";
            }
            tmp += "-------------------------";
            return tmp;
        }

        #region CRUD(insert, select, update, delete)
        public void Account_Add(int number, string name, int money)
        {
            try
            {
                DataRow dr = wb43.Tables["Account"].NewRow();
                dr["number"] = number;
                dr["name"] = name;
                dr["balance"] = money;
                dr["nowdate"] = DateTime.Now;
                wb43.Tables["Account"].Rows.Add(dr);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void Account_Delete(int number)
        {
            DataRow dr = wb43.Tables["Account"].Rows.Find(number);
            if (dr == null)
            {
                throw new Exception("없는 계좌번호");
            }
            else
            {
                wb43.Tables["Account"].Rows.Remove(dr);
            }
        }
        public Account Account_Select(int number)
        {
            DataRow dr = wb43.Tables["Account"].Rows.Find(number);
            if (dr == null)
            {
                return null;
            }
            else
            {
                //인자없는 생성자가 있을 경우, 속성의 이름으로 초기화 가능
                return new Account
                {
                    Number = int.Parse(dr[0].ToString()),
                    Name = dr[1].ToString(),
                    Balance = int.Parse(dr[2].ToString()),
                    NowDate = DateTime.Parse(dr[3].ToString())
                };
            }
        }
        public void Account_Update_Input(int number, int money)
        {
            DataRow dr = wb43.Tables["Account"].Rows.Find(number);
            if (dr == null)
            {
                throw new Exception("없는 계좌번호");
            }
            else
            {
                int balance = int.Parse(dr[2].ToString()) + money;
                dr[2] = balance;
            }
        }
        public void Account_Update_Output(int number, int money)
        {
            DataRow dr = wb43.Tables["Account"].Rows.Find(number);
            if (dr == null)
            {
                throw new Exception("없는 계좌번호");
            }
            else
            {
                int balance = int.Parse(dr["balance"].ToString());
                if (balance < money)
                    throw new Exception("잔액 부족");

                dr["balance"] = balance - money;
            }
        }

        #endregion
    }
}
