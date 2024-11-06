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
			Employee employee = _employeeRepository.GetEmployee(id.Value);
			if(employee == null) {
				Response.StatusCode = 404;
				return View("EmployeeNotFound", id.Value);
			}
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel() {
                EmployeeVM = employee,
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
				string uniqueFileName = ProcessUploadedFile(model);
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
		[HttpGet]
		public ViewResult Edit(int id) {
            Employee employee = _employeeRepository.GetEmployee(id);
            EmployeeEditViewModel employeeEditViewModel = new EmployeeEditViewModel {
                Id = employee.Id,
                Name = employee.Name,
                Department = employee.Department,
                Email = employee.Email,
                ExistingPhotoPath = employee.PhotoPath
            };
			return View(employeeEditViewModel);
		}
		[HttpPost]
		public IActionResult Edit(EmployeeEditViewModel model) {
            if(ModelState.IsValid) {
				Employee employee = _employeeRepository.GetEmployee(model.Id);
				employee.Name = model.Name;
				employee.Email = model.Email;
				employee.Department = model.Department;
				if(model.Photos != null) {
					if(model.ExistingPhotoPath != null) { 
						string filePath = Path.Combine(
							_webHostEnvironment.WebRootPath, "images", model.ExistingPhotoPath
						);
						System.IO.File.Delete(filePath);
					}
					employee.PhotoPath = ProcessUploadedFile(model);
				}
				_employeeRepository.Update(employee);
				return RedirectToAction("index");
			}
			return View();
		}

		private string ProcessUploadedFile(EmployeeCreateViewModel model) {
			string uniqueFileName = null;
			if(model.Photos != null && model.Photos.Count > 0) {
				foreach(IFormFile photo in model.Photos) {
					string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
					uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
					string filePath = Path.Combine(uploadFolder, uniqueFileName);
					using(var fileStream = new FileStream(filePath, FileMode.Create)) {
						photo.CopyTo(fileStream);
					}
				}
			}
			return uniqueFileName;
		}
	}
}
