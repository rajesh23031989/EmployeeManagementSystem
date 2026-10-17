using System;
using System.Collections.Generic;
using System.Text;

namespace EMS.Domain.Entities
{
    public class Employee : BaseEntity
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public decimal Salary { get; set; }

        public DateTime DateOfJoining { get; set; }

        public bool IsActive { get; set; }

        public int DepartmentId { get; set; }

        public Department? Department { get; set; }
    }
}
