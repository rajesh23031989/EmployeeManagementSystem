using System;
using System.Collections.Generic;
using System.Text;

namespace EMS.Domain.Entities
{
    public class Department : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public ICollection<Employee> Employees { get; set; }
            = new List<Employee>();
    }
}
