using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace teamproj2
{
    internal class Test
    {
        public string Subject { get; set; }

        public bool IsPassed { get; set; }

        public Test(string subject, bool isPassed)
        {
            Subject = subject;
            IsPassed = isPassed;
        }

        public Test()
        {
            Subject = "Неопределенный предмет";
            IsPassed = false;
        }

        public override string ToString()
        {
            return $"Предмет: {Subject}, Сдан зачет: {IsPassed}";
        }
    }

}
