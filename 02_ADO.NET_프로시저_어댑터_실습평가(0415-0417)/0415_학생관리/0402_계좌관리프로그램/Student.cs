using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoosongBit.Data
{
    internal class Student
    {
        public int StudentID { get; private set; }
        public string Name { get; private set; }

        public Student(int studentID, string name)
        {
            StudentID = studentID;
            Name = name;
        }

        public void Print()
        {
            Console.WriteLine("{0}\t\t{1}", StudentID, Name);
        }
    }

    internal class Grade
    {
        public int GradeID { get; private set; }
        public int StudentID { get; private set; }
        public string Subject { get; private set; }
        public int Score { get; private set; }

        public Grade(int gradeID, int studentID, string subject, int score)
        {
            GradeID = gradeID;
            StudentID = studentID;
            Subject = subject;
            Score = score;
        }

        public void Print()
        {
            Console.WriteLine("{0}\t{1}\t{2}\t\t{3}", GradeID, StudentID, Subject, Score);
        }

        public void PrintWithName(string name)
        {
            Console.WriteLine("{0}\t\t{1}\t\t{2}", name, Subject, Score);
        }
    }
}