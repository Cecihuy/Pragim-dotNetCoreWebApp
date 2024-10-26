using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models {
    public class Employee {
        public int Id { get; set; }
        [Required]
        [MaxLength(20,ErrorMessage ="Name can not exceed 20 characters")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Office Email")]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",ErrorMessage ="Invalid email format")]
        public string Email { get; set; }
        public Dept Department { get; set; }
    }
}
