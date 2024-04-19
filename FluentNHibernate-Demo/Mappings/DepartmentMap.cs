using FluentNHibernate.Mapping;
using FluentNHibernate_Demo.Models;

namespace FluentNHibernate_Demo.Mappings
{
    public class DepartmentMap : ClassMap<Department>
    {
        public DepartmentMap()
        {
            Table("Departments");
            Id(x => x.DepartmentId).GeneratedBy.Identity();
            Map(x => x.DepartmentName);
            Map(x => x.DepartmentCost);
            HasMany(x => x.Employees)
                .KeyColumn("DepartmentId")
                .Inverse()
                .Cascade.All();
        }
    }
}
