using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers {
    public class HomeController : Controller {
        private readonly IEmployeeRepository _employeeRepository;
        public HomeController(IEmployeeRepository employeeRepository) {
            _employeeRepository = employeeRepository;
        }
        public string Index() {
            return _employeeRepository.GetEmployee(1).Name;
        }
        public ViewResult Details() {
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel() {
                EmployeeVM = _employeeRepository.GetEmployee(1),
                PageTitleVM = "Employee Details"
            };
            return View(homeDetailsViewModel);
        }
    }
}
