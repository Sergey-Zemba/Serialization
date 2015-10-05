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
        private XmlSerializer serializer = new XmlSerializer(typeof(List<Employee>));
        private BinaryFormatter formatter = new BinaryFormatter();
        private string type;
        List<Employee> employees;


        public void Start()
        {
            try
            {
                using (FileStream fs = File.OpenRead("option.ini"))
                {
                    byte[] array = new byte[fs.Length];
                    fs.Read(array, 0, array.Length);
                    type = System.Text.Encoding.Default.GetString(array).ToLower();
                    LoadDatabase(type);
                }
            }
            catch (FileNotFoundException ex)
            {
                type = "bin";
                using (FileStream fs = new FileStream("option.ini", FileMode.Create))
                {
                    byte[] array = System.Text.Encoding.Default.GetBytes(type);
                    fs.Write(array, 0, array.Length);
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
                        employees = (List<Employee>)serializer.Deserialize(fs);
                    }
                    else
                    {
                        employees = new List<Employee>();
                    }
                }
                DoAction();
            }
            else
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
                DoAction();
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
                    Console.WriteLine("Choose number of an action:\n1 - Add the employee\n2 - Exit");
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
                    Console.WriteLine("Choose number of an action:\n1 - Add the employee\n2 - Remove the employee" +
                                      "\n3 - Get the employee by id\n4 - Get all employees\n5 - Exit");
                }
                str = Console.ReadLine();
                switch (str)
                {
                    case "1":
                        AddEmployee();
                        break;
                    case "2":
                        RemoveEmployee();
                        break;
                    case "3":
                        GetEmployeeById();
                        break;
                    case "4":
                        GetEmployees();
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
            Employee emp;
            Console.WriteLine("Type the employee's first name below:");
            string firstName = Console.ReadLine();
            Console.WriteLine("Type the employee's surname below:");
            string surname = Console.ReadLine();
            Console.WriteLine("Type the employee's position below:");
            string position = Console.ReadLine();
            if (employees.Count == 0)
            {
                emp = new Employee(firstName, surname, position, 1);
            }
            else
            {
                emp = new Employee(firstName, surname, position, employees.Last().ID + 1);
            }
            employees.Add(emp);
            Console.WriteLine("The employee wass successfully added to the database. Press <enter> to continue");
            Console.ReadLine();
        }

        private void RemoveEmployee()
        {
            int firedId = 0;
            Console.WriteLine("Choose the id of the fired employee");
            string str = Console.ReadLine();
            try
            {
                firedId = Int32.Parse(str);
            }
            catch (FormatException ex)
            {
                Console.WriteLine("You can type only a number. Press <enter> to continue");
                Console.ReadLine();
                return;
            }
            foreach (Employee e in employees)
            {
                if (e.ID == firedId)
                {
                    employees.Remove(e);
                    Console.WriteLine("The employee {0} was successfully fired. Press <enter> to continue", e);
                    Console.ReadLine();
                    return;
                }
            }
            Console.WriteLine("You don't have an employee with the id = {0}. Press <enter> to continue", firedId);
            Console.ReadLine();
        }

        private void GetEmployeeById()
        {
            int chosenId = 0;
            Console.WriteLine("Choose the id of the employee");
            string str = Console.ReadLine();
            try
            {
                chosenId = Int32.Parse(str);
            }
            catch (FormatException ex)
            {
                Console.WriteLine("You can type only a number. Press <enter> to continue");
                Console.ReadLine();
                return;
            }
            foreach (Employee e in employees)
            {
                if (e.ID == chosenId)
                {
                    Console.WriteLine(e);
                    Console.WriteLine("Press <enter> to continue");
                    Console.ReadLine();
                    return;
                }
            }
            Console.WriteLine("You don't have an employee with the id = {0}. Press <enter> to continue", chosenId);
            Console.ReadLine();
        }

        private void GetEmployees()
        {
            foreach (Employee e in employees)
            {
                Console.WriteLine(e);
            }
            Console.WriteLine("Press <enter> to continue");
            Console.ReadLine();
        }

        private void Exit()
        {
            using (FileStream fs = new FileStream("xmlEmployees.xml", FileMode.OpenOrCreate))
                {
                    serializer.Serialize(fs, employees);
                }
            using (FileStream fs = new FileStream("binEmployees.dat", FileMode.OpenOrCreate))
                {
                    formatter.Serialize(fs, employees);
                }
        }
    }
}
