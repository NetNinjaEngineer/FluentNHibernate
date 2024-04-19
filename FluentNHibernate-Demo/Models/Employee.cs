namespace FluentNHibernate_Demo.Models
{
    public class Employee
    {
        public virtual int EmployeeId { get; protected set; }

        public virtual string? FirstName { get; set; }

        public virtual string? LastName { get; set; }

        public virtual string? Gender { get; set; }

        public virtual string? JobTitle { get; set; }

        public virtual DateTime HireDate { get; set; }

        public virtual decimal Salary { get; set; }

        public virtual string? Phone { get; set; }

        public virtual DateTime DateOfBirth { get; set; }

        public virtual int DepartmentId { get; set; }

        public virtual Department Department { get; set; } = null!;
    }
}
