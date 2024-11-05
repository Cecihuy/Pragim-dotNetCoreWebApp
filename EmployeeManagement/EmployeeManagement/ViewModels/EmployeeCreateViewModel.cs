using EmployeeManagement.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace EmployeeManagement.ViewModels {
	public class EmployeeCreateViewModel {
		[Required]
		[MaxLength(20, ErrorMessage = "Name can not exceed 20 characters")]
		public string Name { get; set; }
		[Required]
		[DisplayName("Office Email")]
		[RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid email format")]
		public string Email { get; set; }
		[Required]
		public Dept? Department { get; set; }
		public List<IFormFile>? Photos { get; set; }
	}
}
