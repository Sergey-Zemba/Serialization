﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDatabase
{
    class BinaryReadingWriting : IReadingWriting
    {
        BinaryFormatter formatter = new BinaryFormatter();
        public void Read(List<Employee> employees)
        {
            using (FileStream fs = new FileStream("binEmployees.dat", FileMode.OpenOrCreate))
            {
                if (fs.Length != 0)
                {
                    employees = (List<Employee>)formatter.Deserialize(fs);
                }
                else
                {
                    employees = new List<Employee>();
                }
            }
        }

        public void Write(List<Employee> employees)
        {
            using (FileStream fs = new FileStream("binEmployees.dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, employees);
            }
        }
    }
}