using System.ComponentModel.DataAnnotations;

namespace EmployeeManAPI.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public decimal Salary { get; set; }
    }
}
