using System;
using System.Collections.Generic;
using System.Linq;

namespace BaitapLINQ2
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Department> departments = new List<Department>();
            List<Employee> employees = new List<Employee>();

            Console.WriteLine("Nhap thong tin phong ban:");
            while (true)
            {
                Console.Write("Nhap ID phong ban ( nhap 0 de ket thuc ): ");
                int departmentId = int.Parse(Console.ReadLine());
                if (departmentId == 0)
                    break;

                Console.Write("Nhap ten phong ban: ");
                string departmentName = Console.ReadLine();

                departments.Add(new Department { DepartmentId = departmentId, Name = departmentName });
            }

            Console.WriteLine("\nNhap thong tin nhan vien:");
            while (true)
            {
                Console.Write("Nhap ID nhan vien ( nhap 0 de ket thuc ): ");
                int employeeId = int.Parse(Console.ReadLine());
                if (employeeId == 0)
                    break;

                Console.Write("Nhap ten nhan vien: ");
                string employeeName = Console.ReadLine();

                Console.Write("Nhap tuoi nhan vien: ");
                int age = int.Parse(Console.ReadLine());

                Console.Write("Nhap luong nhan vien: ");
                double salary = double.Parse(Console.ReadLine());

                Console.Write("Nhap ngay sinh nhan vien (yyyy-mm-dd): ");
                DateTime birthday = DateTime.Parse(Console.ReadLine());

                Console.Write("Nhap ID phong ban cho nhan vien: ");
                int departmentId = int.Parse(Console.ReadLine());

                employees.Add(new Employee
                {
                    EmployeeId = employeeId,
                    Name = employeeName,
                    Age = age,
                    Salary = salary,
                    Birthday = birthday,
                    DepartmentId = departmentId
                });
            }

            double maxSalary = employees.Max(e => e.Salary);
            double minSalary = employees.Min(e => e.Salary);
            double averageSalary = employees.Average(e => e.Salary);

            Console.WriteLine($"\nMax Salary: {maxSalary}");
            Console.WriteLine($"Min Salary: {minSalary}");
            Console.WriteLine($"Average Salary: {averageSalary}");

            var employeeDepartmentInfo = from emp in employees
                                         join dep in departments on emp.DepartmentId equals dep.DepartmentId
                                         select new
                                         {
                                             EmployeeName = emp.Name,
                                             DepartmentName = dep.Name
                                         };

            Console.WriteLine("\nEmployee - Department:");
            foreach (var info in employeeDepartmentInfo)
            {
                Console.WriteLine($"{info.EmployeeName} works in {info.DepartmentName} department");
            }

            var groupedEmployees = from emp in employees
                                   join dep in departments on emp.DepartmentId equals dep.DepartmentId into employeeGroup
                                   from g in employeeGroup.DefaultIfEmpty()
                                   group g by g?.Name ?? "Unassigned" into grouped
                                   select new
                                   {
                                       DepartmentName = grouped.Key,
                                       EmployeeCount = grouped.Count()
                                   };

            Console.WriteLine("\nEmployee Count:");

            foreach (var group in groupedEmployees)
            {
                Console.WriteLine($"{group.DepartmentName}: {group.EmployeeCount}");
            }

            int maxAge = employees.Max(e => e.Age);
            int minAge = employees.Min(e => e.Age);

            Console.WriteLine($"\nMax Age: {maxAge}");
            Console.WriteLine($"Min Age: {minAge}");
        }
    }
}
