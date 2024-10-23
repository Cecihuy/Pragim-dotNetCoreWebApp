using System.Collections.Generic;
using System.Linq;

namespace EmployeeManagement.Models {
    public class MockEmployeeRepository : IEmployeeRepository {
        private List<Employee> _employees;
        public MockEmployeeRepository() {
            _employees = new List<Employee>() {
                new Employee() { Id = 1, Name = "Marry", Department = "HR", Email = "marry@pragim.tech" },
                new Employee() { Id = 2, Name = "John", Department = "IT", Email = "john@pragim.tech" },
                new Employee() { Id = 3, Name = "Sam", Department = "IT", Email = "sam@pragim.tech" }
            };
        }
        public Employee GetEmployee(int id) {
            return _employees.FirstOrDefault(B => B.Id == id);
        }
    }
}
