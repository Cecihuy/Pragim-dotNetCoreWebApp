using Microsoft.AspNetCore.Http.HttpResults;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeManagement.Models {
    public class MockEmployeeRepository : IEmployeeRepository {
        private List<Employee> _employees;
        public MockEmployeeRepository() {
            _employees = new List<Employee>() {
                new Employee() { Id = 1, Name = "Marry", Department = Dept.HR, Email = "marry@pragim.tech" },
                new Employee() { Id = 2, Name = "John", Department = Dept.IT, Email = "john@pragim.tech" },
                new Employee() { Id = 3, Name = "Sam", Department = Dept.IT, Email = "sam@pragim.tech" }
            };
        }
		public Employee Add(Employee employee) {
            employee.Id = _employees.Max(x => x.Id) + 1;
            _employees.Add(employee);
            return employee;
		}
		public Employee Delete(int id) {
            Employee employee = _employees.FirstOrDefault(x => x.Id == id);
            if(employee != null) {
                _employees.Remove(employee);
            }
            return employee;
		}
		public IEnumerable<Employee> GetAllEmployee() {
            return _employees;
        }
        public Employee GetEmployee(int id) {
            return _employees.FirstOrDefault(B => B.Id == id);
        }
		public Employee Update(Employee employeeChanges) {
			Employee employee = _employees.FirstOrDefault(x => x.Id == employeeChanges.Id);
			if(employee != null) {
				employee.Name = employeeChanges.Name;
                employee.Department = employeeChanges.Department;
                employee.Email = employeeChanges.Email;
			}
			return employee;
		}
	}
}
