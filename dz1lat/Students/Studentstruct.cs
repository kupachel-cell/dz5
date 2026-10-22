using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dz1lat.Students
{
    struct Studentstruct
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public int BirthYear { get; set; }
        public string ExamName { get; set; }
        public int Score { get; set; }

        public Studentstruct(string lastName, string firstName, int birthYear, string examName, int score)
        {
            LastName = lastName;
            FirstName = firstName;
            BirthYear = birthYear;
            ExamName = examName;
            Score = score;
        }

        public override string ToString()
        {
            return $"{LastName} {FirstName}, Год рождения: {BirthYear}, Экзамен: {ExamName}, Баллы: {Score}";
        }
        internal class Student
        {
        }
    }
}
