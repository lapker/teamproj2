using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace teamproj2
{
    public enum Education
    {
        Specialist,
        Bachelor,
        SecondEducation
    }
    internal class Student : Person, IDateAndCopy, IEnumerable<string>
    {
        private Education education; // Форма обучения
        private int groupNumber; // Номер группы
        private List<Test> tests; // Список зачетов
        private List<Exam> exams; // Список экзаменов

        public Student()
        {
            this.education = Education.Bachelor;
            this.GroupNumber = 101; // Инициализация через свойство, может бросить исключение
            this.exams = new List<Exam>();
            this.tests = new List<Test>();
        }

        public Student(Person person, Education education, int groupNumber)
        {
            this.FirstName = person.FirstName;
            this.LastName = person.LastName;
            this.Birthday = person.Birthday;

            this.education = education;
            this.GroupNumber = groupNumber; // Использование свойства
            this.exams = new List<Exam>();
            this.tests = new List<Test>();
        }

        public Education Education
        {
            get => education;
            set => education = value;
        }

        public int GroupNumber
        {
            get => groupNumber;
            set
            {
                if (value <= 100 || value > 599)
                    throw new Exception("Номер группы должен быть в диапазоне от 101 до 599.");
                groupNumber = value;
            }
        }

        public List<Test> Tests
        {
            get => tests;
            set => tests = value;
        }

        public double AverageGrade
        {
            get
            {
                if (exams == null || exams.Count == 0)
                    return 0;

                double sum = 0;
                foreach (Exam exam in exams)
                {
                    sum += exam.Grade;
                }
                return sum / exams.Count;
            }
        }

        // Свойство Person
        public Person BasePerson
        {
            get => new Person { FirstName = this.FirstName, LastName = this.LastName, Birthday = this.Birthday };
            set
            {
                this.FirstName = value.FirstName;
                this.LastName = value.LastName;
                this.Birthday = value.Birthday;
            }
        }

        // Свойство для списка экзаменов
        public List<Exam> Exams
        {
            get => exams;
            set => exams = value;
        }

        public void AddExams(params Exam[] newExams)
        {
            foreach (var exam in newExams)
            {
                exams.Add(exam);
            }
        }

        public override string ToString()
        {
            string examsInfo = "";
            foreach (Exam exam in exams)
            {
                examsInfo += exam.ToString() + ", ";
            }

            string testsInfo = "";
            foreach (Test test in tests)
            {
                testsInfo += test.ToString() + ", ";
            }

            return $"Студент: Имя: {FirstName}, Фамилия: {LastName}, Форма обучения: {Education}, Номер группы: {GroupNumber}, Средний балл: {AverageGrade:F2}, Экзамены: [{examsInfo.TrimEnd(',', ' ')}], Зачеты: [{testsInfo.TrimEnd(',', ' ')}]";
        }

        public virtual string ToShortString()
        {
            return $"Студент: Имя: {FirstName}, Фамилия: {LastName}, Форма обучения: {Education}, Номер группы: {GroupNumber}, Средний балл: {AverageGrade:F2}";
        }

        public object DeepCopy()
        {
            var deepCopiedStudent = new Student
            {
                FirstName = this.FirstName,
                LastName = this.LastName,
                Birthday = this.Birthday,
                Education = this.Education,
                GroupNumber = this.GroupNumber,
                exams = new List<Exam>(this.exams), // Клонирование списка экзаменов
                tests = new List<Test>(this.tests) // Клонирование списка зачетов
            };

            return deepCopiedStudent;
        }

        public DateTime Date
        {
            get => Birthday; // Можно использовать Birthday как дату
            set => Birthday = value;
        }

        // Реализация интерфейса IEnumerable для названий предметов
        public IEnumerator<string> GetEnumerator()
        {
            HashSet<string> subjects = new HashSet<string>();

            foreach (Exam exam in exams)
            {
                subjects.Add(exam.Subject);
            }

            foreach (Test test in tests)
            {
                if (test.IsPassed) // Проверяем, что зачет сдан
                {
                    subjects.Add(test.Subject);
                }
            }

            foreach (var subject in subjects)
            {
                yield return subject; // Вернуть название предмета
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerable<object> GetPassedExams()
        {
            // Перебираем сданные экзамены
            foreach (Exam exam in exams)
            {
                if (exam.Grade > 2) // Проверка на сданный экзамен
                {
                    yield return exam; // Возвращаем сданный экзамен
                }
            }
        }
        // Итератор для перебора сданных экзаменов
        public IEnumerable<object> GetPassedExamsAndTests()
        {
            // Перебираем сданные экзамены
            foreach (Exam exam in exams)
            {
                if (exam.Grade > 2) // Проверка на сданный экзамен
                {
                    yield return exam; // Возвращаем сданный экзамен
                }
            }

            // Перебираем сданные зачетов
            foreach (Test test in tests)
            {
                if (test.IsPassed) // Проверка на сданный зачет
                {
                    yield return test; // Возвращаем сданный зачет
                }
            }
        }

        // Итератор для перебора сданных зачетов
        public IEnumerable<Test> GetPassedTests()
        {
            foreach (Test test in tests)
            {
                // Проверка, что зачет сдан и что для него сдан экзамен
                if (test.IsPassed && exams.Exists(exam => exam.Subject == test.Subject && exam.Grade > 2))
                {
                    yield return test; // Возвращаем сданный зачет
                }
            }
        }
    }
}
