//Grade.cs
using System;

namespace WoosonbBit.Data
{
    internal class Grade
    {
        public int GradeID { get; set; }
        public int StudentID { get;set; }
        public string Subject {  get; set; }
        public int Score { get; set; }
        public Grade(int gradeID, int studentID, string subject, int score)
        {
            GradeID = gradeID;
            StudentID = studentID;
            Subject = subject;
            Score = score;
        }
        public override string ToString()
        {
            return GradeID + "\t\t" + StudentID + "\t\t" + Subject + "\t\t" + Score;
        }
    }
}
