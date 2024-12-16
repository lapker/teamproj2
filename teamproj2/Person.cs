using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace teamproj2
{
    internal class Person : IDateAndCopy
    {
        protected String firstName;
        protected String lastName;
        protected DateTime birthday;

        public DateTime Birthday
        {
            get => birthday;
            set
            {
                if (DateTime.Today >= value)
                    birthday = value;
                else
                    throw new ArgumentException("День рождения не может быть больше сегодняшнего дня");
            }
        }
        public String FirstName
        {
            get => firstName;
            set
            {
                if (String.IsNullOrEmpty(value))
                    throw new ArgumentException("Значение имеет null или пустая строка");

                firstName = value;
            }
        }
        public String LastName
        {
            get => lastName;
            set
            {
                if (String.IsNullOrEmpty(value))
                    throw new ArgumentException("Значение имеет null или пустая строка");

                lastName = value;
            }
        }

        public int Years
        {
            get => birthday.Year;
            set
            {
                if (value > DateTime.Now.Year)
                    throw new ArgumentException("Передаваемый год больше года в данный момент");

                int differenceYers = DateTime.Now.Year - value;

                birthday = birthday.AddYears(-differenceYers);
            }
        }

        public Person()
        {
            this.firstName = "имя";
            this.lastName = "фамилия";
            this.birthday = DateTime.Today;
        }

        public Person(String firstName, String lastName, DateTime birthday)
        {
            FirstName = firstName;
            LastName = lastName;
            Birthday = birthday;
        }

        public override string ToString() => $"Имя: {firstName}; Фамилия: {LastName}; дата рождения: {birthday.ToShortDateString()}";

        public virtual string ToShortString() => $"Имя: {firstName}; Фамилия: {LastName}";

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Person other = (Person)obj;
            return firstName == other.firstName && lastName == other.lastName && birthday == other.birthday;
        }

        public static bool operator ==(Person left, Person right)
        {
            if (ReferenceEquals(left, right))
                return true;

            if ((object)left == null || (object)right == null)
                return false;

            return left.Equals(right);
        }

        public static bool operator !=(Person left, Person right)
        {
            return !(left == right);
        }

        public override int GetHashCode()
        {
            int hashFirstName = firstName == null ? 0 : firstName.GetHashCode();
            int hashLastName = lastName == null ? 0 : lastName.GetHashCode();
            int hashBirthday = birthday.GetHashCode();

            return hashFirstName ^ hashLastName ^ hashBirthday;
        }

        public object DeepCopy()
        {
            return new Person
            {
                FirstName = this.FirstName,
                LastName = this.LastName,
                Birthday = this.Birthday
            };
        }

        public DateTime Date
        {
            get => this.Birthday;
            set => this.Birthday = value;
        }
    }
}
