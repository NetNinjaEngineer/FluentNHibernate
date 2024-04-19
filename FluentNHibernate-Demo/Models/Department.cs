namespace FluentNHibernate_Demo.Models
{
    public class Department
    {
        public virtual int DepartmentId { get; protected set; }

        public virtual string DepartmentName { get; set; } = null!;

        public virtual decimal? DepartmentCost { get; set; }

        public virtual IList<Employee> Employees { get; set; } = [];
    }
}
