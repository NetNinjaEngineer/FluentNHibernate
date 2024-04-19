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

            var employees = session.Query<Employee>();
            var departments = session.Query<Department>();

            transaction.Commit();


            Console.ReadKey();
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
