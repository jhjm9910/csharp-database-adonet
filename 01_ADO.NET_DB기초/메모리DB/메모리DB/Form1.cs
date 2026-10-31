//Form1.cs
using _04_16_메모리DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 메모리DB;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace _0416_메모리DB
{
    public partial class Form1 : Form
    {
        private MemDB db = new MemDB();
        #region 컨트롤과 속성의 연결(테이블 정보 출력)
        public string AccountInfo
        {
            get { return textBox1.Text; }
            set { textBox1.Text = value; }
        }
        #endregion

        #region 컨트롤과 속성 연결(계좌 생성)
        public int AccNumber
        {
            get { return int.Parse(textBox2.Text); }
            set { textBox2.Text = value.ToString(); }
        }
        public string AccName
        {
            get { return textBox3.Text; }
            set { textBox3.Text = value; }
        }
        public int AccMoney
        {
            get { return int.Parse(textBox4.Text); }
            set { textBox4.Text = value.ToString(); }
        }
        #endregion

        #region 컨트롤과 속성 연결(계좌 검색)
        public int AccSelectNumber
        {
            get { return int.Parse(textBox5.Text); }
            set { textBox5.Text = value.ToString(); }
        }
        public string AccSelectName
        {
            get { return textBox6.Text; }
            set { textBox6.Text = value; }
        }
        public int AccSelectMoney
        {
            get { return int.Parse(textBox7.Text); }
            set { textBox7.Text = value.ToString(); }
        }
        public DateTime AccSelectDate
        {
            get { return DateTime.Parse(textBox8.Text); }
            set { textBox8.Text = value.ToLongDateString(); }
        }
        public DateTime AccSelectTime
        {
            get { return DateTime.Parse(textBox9.Text); }
            set { textBox9.Text = value.ToLongDateString(); }
        }
        public DateTime AccSelectDateTime
        {
            get { return dateTimePicker1.Value; }
            set { dateTimePicker1.Value = value; }
        }
        #endregion

        #region 컨트롤과 속성 연결(계좌 입출금)
        public int AccUpdateNumber
        {
            get { return int.Parse(textBox10.Text); }
            set { textBox10.Text = value.ToString(); }
        }
        public int AccUpdateMoney
        {
            get { return int.Parse(textBox11.Text); }
            set { textBox11.Text = value.ToString(); }
        }
        #endregion
        public Form1()
        {
            InitializeComponent();
        }

        //테이블 생성
        private void button1_Click(object sender, EventArgs e)
        {
            db.Account_DesingTable();
            AccountInfo = db.Table_ViewAll(db.AccountTable);

            dataGridView1.DataSource = db.AccountTable; //**

            /*
            textBox1.Text += "aaa" + "\r\n";
            textBox1.Text += "bbb" + "\r\n";
            textBox1.Text += "aaa" + "\r\n";
            textBox1.AppendText("bbb" + "\r\n");    //같은 기능
            */
            /*
            AccountInfo += "aaa" + "\r\n";
            AccountInfo += "bbb" + "\r\n";
            AccountInfo += "ccc" + "\r\n";
            AccountInfo += "ddd" + "\r\n";
            */
        }

        //테이블 제거
        private void button2_Click(object sender, EventArgs e)
        {
            //this.Text = textBox1.Text;
            this.Text = AccountInfo;
        }

        //계좌 생성
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                db.Account_Add(AccNumber, AccName, AccMoney);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "계좌 생성 오류");
            }
        }

        //계좌 삭제
        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                db.Account_Delete(AccNumber);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "계좌 생성 오류");
            }
        }

        //계좌 검색
        private void button4_Click(object sender, EventArgs e)
        {
            Account acc = db.Account_Select(AccSelectNumber);
            if (acc == null)
            {
                MessageBox.Show("없는 계좌번호입니다.", "알림");
            }
            else
            {
                AccSelectNumber = acc.Number;
                AccSelectName = acc.Name;
                AccSelectMoney = acc.Balance;
                AccSelectDate = acc.NowDate;
                AccSelectTime = acc.NowDate;
                AccSelectDateTime = acc.NowDate;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (button5 == (Button)sender)
                {
                    db.Account_Update_Input(AccUpdateNumber, AccUpdateMoney);
                }
                else if (button6 == (Button)sender)
                {
                    db.Account_Update_Output(AccUpdateNumber, AccUpdateMoney);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "입출금");
            }
        }


    }
}
