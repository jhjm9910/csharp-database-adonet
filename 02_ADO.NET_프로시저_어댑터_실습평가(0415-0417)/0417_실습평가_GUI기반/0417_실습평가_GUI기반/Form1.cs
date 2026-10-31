using System;
using System.Windows.Forms;

namespace _0417_실습평가_GUI기반
{
    public partial class Form1 : Form
    {
        private WoosongBit.Control con = WoosongBit.Control.Instance;

        public Form1()
        {
            InitializeComponent();
            InitConnectInfo();
        }

        #region DB연결 및 종료
        private void InitConnectInfo()
        {
            textBox1.Text = "DESKTOP-0I86BTV\\SQLEXPRESS";
            textBox2.Text = "WB43";
            textBox3.Text = "ccm";
            textBox4.Text = "1234";

        }
        //DB연결
        private void button1_Click(object sender, EventArgs e)
        {
            if (con.Server_Join(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text) == true)
            {
                dataGridView1.DataSource = con.Select_Students();
                dataGridView2.DataSource = con.Select_Grades();                
            }
            else
                MessageBox.Show("DB연결 실패");
        }
        //DB종료
        private void button2_Click(object sender, EventArgs e)
        {
            if (con.Server_Drop() == true)
            {
                dataGridView1.DataSource = null;
                dataGridView2.DataSource = null;
                dataGridView3.DataSource = null;
            }
            else
                MessageBox.Show("DB연결해제 실패");
        }
        #endregion

        #region 기능 1, 2 학생 등록 / 전체 출력
        
        //학생 등록
        private void button3_Click(object sender, EventArgs e)
        {
            string name = textBox5.Text;

            try
            {
                con.Insert_Student(name);
                dataGridView1.DataSource = con.Select_Students();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "<학생등록>");
            }
        }
        #endregion

        #region 기능 3,4,5 성적 입력 / 수정 / 전체 출력
        
        //3.성적 입력
        private void button4_Click(object sender, EventArgs e)
        {
            int stu_id          = int.Parse(textBox6.Text);
            string subj_name    = textBox7.Text;
            int score           = int.Parse(textBox8.Text);

            try
            {
                con.Insert_Grade(stu_id, subj_name, score);
                dataGridView2.DataSource =  con.Select_Grades();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //4.성적 수정
        private void button5_Click(object sender, EventArgs e)
        {
            int stu_id = int.Parse(textBox6.Text);
            string subj_name = textBox7.Text;
            int score = int.Parse(textBox8.Text);

            try
            {
                con.Update_Grade(stu_id, subj_name, score);
                dataGridView2.DataSource = con.Select_Grades();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //5. 전체 출력
        private void button6_Click(object sender, EventArgs e)
        {
            string subject = textBox9.Text;
            dataGridView2.DataSource = con.Select_Grades(subject);
            if (dataGridView2.Rows.Count == 0)
            {
                dataGridView2.DataSource = con.Select_Grades();
            }
        }
        #endregion

        #region 기능 6. 학생별 성적 조회
        private void button7_Click(object sender, EventArgs e)
        {
            int stu_id = int.Parse(textBox10.Text);
            dataGridView3.DataSource = con.Select_Student_Scores(stu_id);
        }
        #endregion 
    }
}
