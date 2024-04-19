using FluentNHibernate.Mapping;
using FluentNHibernate_Demo.Models;

namespace FluentNHibernate_Demo.Mappings
{
    public class EmployeeMap : ClassMap<Employee>
    {
        public EmployeeMap()
        {
            Table("Employees");
            Id(x => x.EmployeeId).GeneratedBy.Identity();
            Map(x => x.FirstName)
                .Not.Nullable();
            Map(x => x.LastName)
                .Not.Nullable();
            Map(x => x.Gender)
                .Not.Nullable();
            Map(x => x.JobTitle)
                .Not.Nullable();
            Map(x => x.HireDate);
            Map(x => x.Salary);
            Map(x => x.Phone).Not.Nullable();
            Map(x => x.DateOfBirth);

            References(x => x.Department)
                .Column("DepartmentId")
                .Not.Nullable();

        }
    }
}
