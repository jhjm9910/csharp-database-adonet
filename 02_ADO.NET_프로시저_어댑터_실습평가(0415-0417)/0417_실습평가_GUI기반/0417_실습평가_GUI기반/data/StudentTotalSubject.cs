//StudentTotalSubject.cs
using System;

namespace WoosonbBit.Data
{
    internal class StudentTotalSubject
    {
        public string StudentName { get; set; }
        public string SubjectName { get; set; }
        public int Score { get; set; }

        public StudentTotalSubject(string studentName, string subjectName, int score)
        {
            StudentName = studentName;
            SubjectName = subjectName;
            Score = score;
        }
        public override string ToString()
        {
            return StudentName + "\t\t" + SubjectName + "\t\t" + Score;
        }
    }
}
