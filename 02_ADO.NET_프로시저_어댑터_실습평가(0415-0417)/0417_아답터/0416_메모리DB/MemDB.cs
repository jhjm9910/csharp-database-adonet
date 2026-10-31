//MemDB
using System;
using System.Data;
using System.Windows.Forms;
using System.Xml.Linq;


namespace _0416_메모리DB
{
    internal class MemDB
    {
        private DataTable account_table = null;
        public DataTable AccountTable { get { return account_table; } }

        #region 테이블 생성, 제거, 정보
        public void Account_DesingTable()
        {
            if (account_table != null)
            {
                throw new Exception("이미 존재합니다.");
            }

            account_table = new DataTable("Account");

            DataColumn dc_number = new DataColumn("number", typeof(int));            
            account_table.Columns.Add(dc_number);

            DataColumn dc_name = new DataColumn("name", typeof(string));
            dc_name.AllowDBNull = false;
            account_table.Columns.Add(dc_name);

            DataColumn dc_balance = new DataColumn("balance", typeof(int));
            dc_balance.AllowDBNull = false;
            account_table.Columns.Add(dc_balance);

            DataColumn dc_nowdate = new DataColumn("nowdate", typeof(DateTime));
            dc_nowdate.AllowDBNull = false;
            account_table.Columns.Add(dc_nowdate);

            DataColumn[] pkeys = new DataColumn[1];
            pkeys[0] = dc_number;
            account_table.PrimaryKey = pkeys;        
        }
        public void Account_DropTable()
        {
            account_table.Dispose();
            account_table = null;
        }
        public string Table_ViewAll(DataTable dt)
        {
            string tmp = string.Empty;
            tmp += "테이블 명 : " + dt.TableName + "\r\n";
            tmp += "컬럼 개수 : " + dt.Columns.Count + "\r\n";
            tmp += "로우 데이터 개수 : " + dt.Rows.Count + "\r\n";
            tmp += "기본 키 : " + dt.PrimaryKey[0].ColumnName + "\r\n";
            tmp += "-------------------------" + "\r\n";
            
            foreach (DataColumn dc in dt.Columns)
            {
                tmp += dc.ColumnName + ", " +  dc.DataType + ", " + dc.AllowDBNull + "\r\n";
            }
            tmp += "-------------------------";
            return  tmp;
        }
        #endregion

        #region CRUD(insert, select, update, delete)
        public void Account_Add(int number, string name, int money)
        {
            try
            {
                DataRow dr = account_table.NewRow();
                dr["number"]    = number;
                dr["name"]      = name;
                dr["balance"]   = money;
                dr["nowdate"]   = DateTime.Now;
                account_table.Rows.Add(dr);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void Account_Delete(int number)
        {
            DataRow dr = account_table.Rows.Find(number);
            if (dr == null)
            {
                throw new Exception("없는 계좌번호");
            }
            else
            {
                account_table.Rows.Remove(dr);                
            }
        }
        public Account Account_Select(int number)
        {
            DataRow dr = account_table.Rows.Find(number);
            if (dr == null)
            {
                return null;
            }
            else
            {
                //인자없는 생성자가 있을 경우, 속성의 이름으로 초기화 가능
                return new Account{
                    Number = int.Parse(dr[0].ToString()),
                    Name = dr[1].ToString(),
                    Balance = int.Parse(dr[2].ToString()),
                    NowDate = DateTime.Parse(dr[3].ToString())
                };
            }
        }
        public void Account_Update_Input(int number, int money)
        {
            DataRow dr = account_table.Rows.Find(number);
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
            DataRow dr = account_table.Rows.Find(number);
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

        #region XML 다루기
        const string XML_SCHEMA = "accounts.xsd";
        const string XML_DATA   = "accounts.xml";
        public void XMLSave()
        {
            if (account_table == null)
            {
                MessageBox.Show("Account 테이블 먼저 생성");
            }
            else
            {
                account_table.WriteXmlSchema(XML_SCHEMA, true);
                account_table.WriteXml(XML_DATA, true);
                MessageBox.Show("XML저장 성공");
            }

        }
        public void XMLLoad()
        {
            account_table = new DataTable();
            account_table.ReadXmlSchema(XML_SCHEMA);
            account_table.ReadXml(XML_DATA);
        }
        #endregion 
    }
}
