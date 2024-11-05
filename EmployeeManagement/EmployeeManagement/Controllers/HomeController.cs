using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;

namespace EmployeeManagement.Controllers {
    public class HomeController : Controller {
        private readonly IEmployeeRepository _employeeRepository;
		private readonly IWebHostEnvironment _webHostEnvironment;

		public HomeController(IEmployeeRepository employeeRepository, IWebHostEnvironment webHostEnvironment) {
            _employeeRepository = employeeRepository;
			_webHostEnvironment = webHostEnvironment;
        }
        public ViewResult Index() {
            IEnumerable<Employee> model = _employeeRepository.GetAllEmployee();
            return View(model);
        }
        public ViewResult Details(int? id) {
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel() {
                EmployeeVM = _employeeRepository.GetEmployee(id??1),
                PageTitleVM = "Employee Details"
            };
            return View(homeDetailsViewModel);
        }
        [HttpGet]
        public ViewResult Create() {
            return View();
        }
        [HttpPost]
		public IActionResult Create(EmployeeCreateViewModel model) {
            if(ModelState.IsValid) {
                string uniqueFileName = null;
                if(model.Photos != null && model.Photos.Count > 0) {
                    foreach(IFormFile photo in model.Photos) {
                        string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                        uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                        string filePath = Path.Combine(uploadFolder, uniqueFileName);
                        photo.CopyTo(new FileStream(filePath, FileMode.Create));
                    }
                }
                Employee newEmployee = new Employee {
                    Name = model.Name,
                    Email = model.Email,
                    Department = model.Department,
                    PhotoPath = uniqueFileName
                };
                _employeeRepository.Add(newEmployee);
                return RedirectToAction("details", new { id = newEmployee.Id });
            }
            return View();
		}
	}
}
