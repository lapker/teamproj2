using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace teamproj2
{
    internal class Exam : IDateAndCopy
    {
        public string Subject { get; set; }
        public int Grade { get; set; }
        public DateTime ExamDate { get; set; }

        public Exam(string subject, int grade, DateTime examDate)
        {
            Subject = subject;
            Grade = grade;
            ExamDate = examDate;
        }

        public Exam()
        {
            Subject = "предмет";
            Grade = 0;
            ExamDate = DateTime.Now;
        }

        public object DeepCopy()
        {
            return new Exam
            {
                Subject = this.Subject,
                Grade = this.Grade,
                ExamDate = this.ExamDate
            };
        }

        public DateTime Date
        {
            get => ExamDate;
            set => ExamDate = value;
        }

        public override string ToString()
        {
            return $"Предмет: {Subject}, Оценка: {Grade}, Дата экзамена: {ExamDate.ToShortDateString()}";
        }
    }
}
