using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Student : BaseEntity
    {
        private static int _studentCounter = 0;
        public int Id { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public Group Group { get; set; }
        public Student()
        {
            Id = ++_studentCounter;
        }
    }
}
