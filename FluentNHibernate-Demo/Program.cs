using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate_Demo.Models;
using Microsoft.Extensions.Configuration;
using NHibernate;

namespace FluentNHibernate_Demo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var sessionFactory = CreateSessionFactory();
            using var session = sessionFactory.OpenSession();
            using var transaction = session.BeginTransaction();

            S01_Structure_Of_The_Query(session);

            S02_Parameter_Types(session);

            transaction.Commit();


            Console.ReadKey();
        }

        private static void S02_Parameter_Types(ISession session)
        {

        }

        private static void S01_Structure_Of_The_Query(ISession session)
        {
            var employees = session.Query<Employee>().ToList();
            employees.ForEach(e => Console.WriteLine(string.Concat(e.FirstName, " ", e.LastName)));

            var department = session.Get<Department>(3);
            var empsInDepartment = department.Employees
                .AsQueryable()
                .Where(e => e.Gender!.ToLower() == "male");

            foreach (var emp in empsInDepartment)
                Console.WriteLine($"{string.Concat(emp.FirstName, " ", emp.LastName)}  [{emp.JobTitle}]");
        }

        private static void CreateNewEmployee(ISession session)
        {
            var newEmployee = new Employee
            {
                FirstName = "John",
                LastName = "Doe",
                Gender = "Male",
                JobTitle = "Doctor",
                HireDate = DateTime.Now,
                Salary = 50000,
                Phone = "123-456-7890",
                DateOfBirth = new DateTime(1990, 5, 15)
            };

            var cardiologyDept = session.Query<Department>()
                .SingleOrDefault(x => x.DepartmentId == 3);

            if (cardiologyDept is not null)
                newEmployee.Department = cardiologyDept;


            session.Save(newEmployee);
        }

        private static void CreateNewDepartment(ISession session)
        {
            var department = new Department { DepartmentName = "Cardiology", DepartmentCost = 100 };

            session.Save(department);
        }

        private static ISessionFactory CreateSessionFactory()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var constr = configuration.GetSection("constr").Value;

            return Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012.ConnectionString(constr))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Program>())
                .BuildSessionFactory();
        }
    }
}
