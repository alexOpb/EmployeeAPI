using System;

namespace EmployeeAPI.Models
{
    public class Employee
    {
        public long EmployeeId { get; set; }
        public string Name { get; set; }
        public long Department { get; set; }
        public string Title { get; set; }
        public long ReportsTo { get; set; }
        public string HireDate { get; set; }
    }
}