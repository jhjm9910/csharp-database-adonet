//MemDB.cs
using System;
using System.CodeDom;
using System.Data;
using System.Windows.Forms;

namespace _04_16_메모리DB
{
    internal class MemDB
    {
        private DataTable account_table = null;
        public DataTable AccountTable { get { return account_table; } }

        #region 테이블 생성, 제거, 정보
        public void Account_DesingTable()
        {
            
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

        public string Table_ViewAll(DataTable dt)
        {
            string tmp = string.Empty;
            tmp += "테이블 명 :" + dt.TableName + "\r\n";
            tmp += "컬럼 명 :" + dt.Columns.Count + "\r\n";
            tmp += "로우 데이터 개수 :" + dt.Rows.Count + "\r\n";
            tmp += "기본 키 :" + dt.PrimaryKey[0].ColumnName + "\r\n";
            tmp += "_______________________________" + "\r\n";

            foreach (DataColumn dc in dt.Columns)
            {
                tmp += dc.ColumnName + "," + dc.DataType + "," + dc.AllowDBNull + "\r\n";
            }
            tmp += "_______________________________";
            return tmp;
        }
        #endregion
        #region CRUD (insert, select, delete, update)
        public void Account_Add(int number, string name, int money)
        {
            try
            {
                DataRow dr = account_table.NewRow();
                dr["number"] = number;
                dr["name"] = name;
                dr["balance"] = money;
                dr["nowdate"] = DateTime.Now;
                account_table.Rows.Add(dr);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        Const string XML_SCHEMA = "accounts.xsd";
        Const string XML_DATA = "accounts.xsd";

        public void XMLSave()
        {
            if(account_table == null)
            {
                MessageBox.Show("Account ");
            }
            account_table.WriteXmlSchema(XML_SCHEMA,true);
            account_table.WriteXml(XML_DATA,true);
        }

        public void XMLLoad()
        {

        }





        #endregion
    }
}
