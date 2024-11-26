using ExerciseLINQLambda.Entities;
using System.Globalization;

namespace ExerciseLINQLambda
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Employee> employees = new List<Employee>();

            Console.WriteLine("Enter full file path: ");
            string path = Console.ReadLine();

            Console.Write("Enter salary: ");
            double salarySelected = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

            try
            {
                using (StreamReader sr = File.OpenText(path))
                {
                    while (!sr.EndOfStream)
                    {
                        string[] fields = sr.ReadLine().Split(',');
                        string name = fields[0];
                        string email = fields[1];
                        double salary = double.Parse(fields[2], CultureInfo.InvariantCulture);

                        employees.Add(new Employee(name, email, salary));
                    }
                }

                Console.WriteLine($"Email of people whose salary is more than {salarySelected.ToString("F2", CultureInfo.InvariantCulture)}: ");

                var emailForSalary = employees.Where(employee => employee.Salary > salarySelected).OrderBy(employee => employee.Email).Select(employee => employee.Email);

                foreach (string emailFiltered in emailForSalary)
                {
                    Console.WriteLine(emailFiltered);
                }

                Console.Write("Select the initial letter of the names you want to sum salary to: ");
                string letter = Console.ReadLine();

                Console.Write($"Sum of salary of people whose name starts with {letter}: ");
                var sumSalary = employees.Where(employees => employees.Name.StartsWith(letter)).Sum(employees => employees.Salary);

                Console.WriteLine(sumSalary);
            }
            catch (IOException e)
            {
                Console.WriteLine("An error occurred");
                Console.WriteLine(e.Message);
            }
        }
    }
}
