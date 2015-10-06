using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDatabase
{
    interface IReadingWriting
    {
        void Read(List<Employee> employees);
        void Write(List<Employee> employees);
    }
}
