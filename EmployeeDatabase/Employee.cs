using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDatabase
{
    [Serializable]
    class Employee
    {
        private static int id;

        public Employee()
        {
            
        }
        public Employee(string firstName, string surname, string position)
        {
            FirstName = firstName;
            Surname = surname;
            Position = position;
            ID = ++id;
        }
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Position { get; set; }

        public override string ToString()
        {
            string str = ID + " " + FirstName + " " + Surname + " " + Position;
            return str;
        }
    }
}
