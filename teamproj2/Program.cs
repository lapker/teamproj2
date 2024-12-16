using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace teamproj2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 1. Создание двух объектов типа Person с совпадающими данными
            Person person1 = new Person { FirstName = "Иван", LastName = "Иванов", Birthday = new DateTime(2000, 1, 1) };
            Person person2 = new Person { FirstName = "Иван", LastName = "Иванов", Birthday = new DateTime(2000, 1, 1) };

            // Проверка, что ссылки на объекты не равны, а объекты равны
            Console.WriteLine($"Ссылки на объекты равны: {ReferenceEquals(person1, person2)}"); // false
            Console.WriteLine($"Объекты равны: {person1.Equals(person2)}"); // true
            Console.WriteLine($"Хэш-коды: {person1.GetHashCode()}, {person2.GetHashCode()}");

            // 2. Создание объекта типа Student и добавление экзаменов и зачетов
            Student student = new Student(person1, Education.Bachelor, 101);
            student.AddExams(new Exam("Математика", 5, DateTime.Now), new Exam("Физика", 4, DateTime.Now), new Exam("Химия", 1, DateTime.Now));
            student.Tests.Add(new Test("Математика", true));
            student.Tests.Add(new Test("Физика", false));

            // Вывод данных объекта Student
            Console.WriteLine(student.ToString());

            // 3. Вывод значения свойства типа Person для объекта типа Student
            Console.WriteLine("Данные студента:");
            Console.WriteLine(student.BasePerson);

            // 4. Создание полной копии объекта Student с помощью DeepCopy()
            Student copiedStudent = (Student)student.DeepCopy();

            // Изменение данных в исходном объекте Student
            student.FirstName = "Петр"; // Изменение имени
            student.GroupNumber = 200; // Изменение номера группы

            // Вывод копии и исходного объекта
            Console.WriteLine("Копия студента:");
            Console.WriteLine(copiedStudent.ToString()); // Должен оставаться без изменений
            Console.WriteLine("Измененный студент:");
            Console.WriteLine(student.ToString()); // Измененный

            // 5. Обработка исключения при некорректном значении номера группы
            try
            {
                student.GroupNumber = 50; // Неправильное значение
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            // 6. Вывод списка всех зачетов и экзаменов с помощью итератора
            Console.WriteLine("Список всех зачетов и экзаменов:");
            foreach (var item in student)
            {
                Console.WriteLine(item);
            }

            // 7. Вывод экзаменов с оценкой выше 3
            Console.WriteLine("Экзамены с оценкой выше 3:");
            foreach (var exam in student.GetPassedExams())
            {
                Console.WriteLine(exam);
            }

            // 8. Вывод списка предметов, которые есть как в списке зачетов, так и в списке экзаменов
            Console.WriteLine("Предметы, которые есть как в зачетах, так и в экзаменах:");
            foreach (var subject in student)
            {
                Console.WriteLine(subject);
            }

            // 9. Вывод всех сданных зачетов и сданных экзаменов
            Console.WriteLine("Сданные зачеты и экзамены:");
            foreach (var passedItem in student.GetPassedExamsAndTests())
            {
                Console.WriteLine(passedItem);
            }

            // 10. Вывод сданных зачетов, для которых сдан и экзамен
            Console.WriteLine("Сданные зачеты, для которых сдан и экзамен:");
            foreach (var passedTest in student.GetPassedTests())
            {
                Console.WriteLine(passedTest);
            }
        }
    }
}
