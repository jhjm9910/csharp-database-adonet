//Student
using System;

namespace WoosonbBit.Data
{
    internal class Student
    {
        public int StudentID { get; set; }
        public string Name { get; set;  }

        public Student(int studentID, string name)
        {
            StudentID = studentID;
            Name = name;
        }

        public override string ToString()
        {
            return StudentID + "\t\t" + Name;
        }
    }
}
