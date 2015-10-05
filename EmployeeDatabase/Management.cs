using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EmployeeDatabase
{
    class Management
    {
        private XmlSerializer serializer;
        private BinaryFormatter formatter;
        private string type;
        List<Employee> employees;


        public void Start()
        {
            using (FileStream fs = File.OpenRead("option.ini"))
            {
                byte[] array = new byte[fs.Length];
                fs.Read(array, 0, array.Length);
                type = System.Text.Encoding.Default.GetString(array).ToLower();
                if (type == "xml")
                {
                    serializer = new XmlSerializer(typeof (List<Employee>));
                }
                else
                {
                    formatter = new BinaryFormatter();
                }
                LoadDatabase(type);
            }
        }

        private void LoadDatabase(string type)
        {
            if (type == "xml")
            {
                using (FileStream fs = new FileStream("xmlEmployees.xml", FileMode.OpenOrCreate))
                {
                    if (fs.Length != 0)
                    {
                        employees = (List<Employee>) serializer.Deserialize(fs);
                        DoAction();
                    }
                    else
                    {
                        employees = new List<Employee>();
                        DoAction();
                    }
                }
            }
            else
            {
                using (FileStream fs = new FileStream("binEmployees.dat", FileMode.OpenOrCreate))
                {
                    if (fs.Length != 0)
                    {
                        employees = (List<Employee>) formatter.Deserialize(fs);
                        DoAction();
                    }
                    else
                    {
                        employees = new List<Employee>();
                        DoAction();
                    }
                }
            }
        }

        private void DoAction()
        {
            while (true)
            {
                string str;
                if (employees.Count == 0)
                {
                    Console.WriteLine("Database is loaded.");
                    Console.WriteLine("Database is empty.");
                    Console.WriteLine("Choose number of an action: \n 1 - Add the employee\n2 - Exit");
                    str = Console.ReadLine();
                    switch (str)
                    {
                        case "1":
                            AddEmployee();
                            break;
                        case "2":
                            Exit();
                            return;
                        default:
                            continue;
                    }
                }
                else
                {
                    Console.WriteLine("Database is loaded.");
                    Console.WriteLine("Choose number of an action: \n 1 - Add the employee\n2 - Remove the employee" +
                                      "\n3 - Get the employee by id\n4 - Get all employees\n5 - Exit");
                }
                str = Console.ReadLine();
                switch (str)
                {
                    case "1":
                        AddEmployee();
                        break;
                    case "2":
                        //RemoveEmployee();
                        break;
                    case "3":
                        //GetEmployeeById();
                        break;
                    case "4":
                        //GetEmployees();
                        break;
                    case "5":
                        Exit();
                        return;
                    default:
                        continue;
                }
            }
        }

        private void AddEmployee()
        {
            Console.WriteLine("Type the employee's first name below:");
            string firstName = Console.ReadLine();
            Console.WriteLine("Type the employee's surname below:");
            string surname = Console.ReadLine();
            Console.WriteLine("Type the employee's position below:");
            string position = Console.ReadLine();
            Employee emp = new Employee(firstName, surname, position);
            employees.Add(emp);
        }

        private void Exit()
        {
            if (type == "xml")
            {
                using (FileStream fs = new FileStream("xmlEmployees.xml", FileMode.OpenOrCreate))
                {
                    serializer.Serialize(fs, employees);
                    
                }
            }
            else
            {
                using (FileStream fs = new FileStream("binEmployees.dat", FileMode.OpenOrCreate))
                {
                    formatter.Serialize(fs, employees);
                    
                }
            }
        }
    }
}
