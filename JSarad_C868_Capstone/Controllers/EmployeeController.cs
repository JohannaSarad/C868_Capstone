﻿using JSarad_C868_Capstone.Data;
using JSarad_C868_Capstone.Models;
using Microsoft.AspNetCore.Mvc;
using JSarad_C868_Capstone.ViewModels;
using Microsoft.CodeAnalysis.Operations;

namespace JSarad_C868_Capstone.Controllers
{
    public class EmployeeController : Controller
    {
        public IEnumerable<Employee> EmployeeList { get; set; }
        public Employee? SelectedEmployee { get; set; }
        private readonly AppDbContext _db;

        public EmployeeController(AppDbContext db)
        {
            _db = db;
            //_db can be used to access database tables (Entity Framework)
        }

        //Get /Employee
        //public async Task<IActionResult> Index()
        //{
        //    var employeeList = await _db.Employees.ToList();
        //    return View(await _db.Employees);
        //}
        public IActionResult Index()
        {

            //EmployeeListViewModel viewModel = new EmployeeListViewModel();
            //viewModel.EmployeeList = _db.Employees;
            //viewModel.SelectedId = 0;
            //viewModel.SelectedName = "";
            
            var employees = _db.Employees;
           
            return View(employees);
        }

        //Get /Employee/Add
        [HttpGet]
        public IActionResult Add()
        {
            EmployeeViewModel viewModel = new EmployeeViewModel();
            return PartialView("_AddEmployeeModalPartial", viewModel); ;
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Add(EmployeeViewModel viewModel)
        {
            //added viewModel and daysToChar conversion for employee.Availability (May or may not work here)
            //EmployeeViewModel viewModel = new EmployeeViewModel();

            //FIX ME!!! move days to charactors to its own class or function
            //convert days to characters

            viewModel.Employee.Availability = DaysToChar(viewModel);
            //string daysToChars = "";
            //if (viewModel.Monday)
            //{
            //    daysToChars += "M";
            //}
            //if (viewModel.Tuesday)
            //{
            //    daysToChars += "T";
            //}
            //if (viewModel.Wednesday)
            //{
            //    daysToChars += "W";
            //}
            //if (viewModel.Thursday)
            //{
            //    daysToChars += "R";
            //}
            //if (viewModel.Friday)
            //{
            //    daysToChars += "F";
            //}
            //if (viewModel.Saturday)
            //{
            //    daysToChars += "S";
            //}
            //if (viewModel.Sunday)
            //{
            //    daysToChars += "U";
            //}

            //viewModel.Employee.Availability = daysToChars;

            
            if (ModelState.IsValid)
            {
                //adding an employee to database
                _db.Employees.Add(viewModel.Employee);
                _db.SaveChanges();
                return Ok(true);
            }
            return PartialView("_AddEmployeeModalPartial", viewModel);
           
            
        }

        public string DaysToChar(EmployeeViewModel viewModel)
        {
            string daysToChars = "";
            if (viewModel.Monday)
            {
                daysToChars += "M";
            }
            if (viewModel.Tuesday)
            {
                daysToChars += "T";
            }
            if (viewModel.Wednesday)
            {
                daysToChars += "W";
            }
            if (viewModel.Thursday)
            {
                daysToChars += "R";
            }
            if (viewModel.Friday)
            {
                daysToChars += "F";
            }
            if (viewModel.Saturday)
            {
                daysToChars += "S";
            }
            if (viewModel.Sunday)
            {
                daysToChars += "U";
            }

            return daysToChars;
        }
        //Get /Employee/Edit
        
        public IActionResult Edit(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var selectedEmployee = _db.Employees.Find(id);

            //check if selectedEmployee is null
            if (selectedEmployee == null)
            {
                return NotFound();
            }

            EmployeeViewModel viewModel = new EmployeeViewModel();
            viewModel.Employee = selectedEmployee;

            if (!string.IsNullOrEmpty(viewModel.Employee.Availability)) {
                if (viewModel.Employee.Availability.Contains("M"))
                {
                    viewModel.Monday = true;
                }
                if (viewModel.Employee.Availability.Contains("T"))
                {
                    viewModel.Tuesday = true;
                }
                if (viewModel.Employee.Availability.Contains("W"))
                {
                    viewModel.Wednesday = true;
                }
                if (viewModel.Employee.Availability.Contains("R"))
                {
                    viewModel.Thursday = true;
                }
                if (viewModel.Employee.Availability.Contains("F"))
                {
                    viewModel.Friday = true;
                }
                if (viewModel.Employee.Availability.Contains("S"))
                {
                    viewModel.Saturday = true;
                }
                if (viewModel.Employee.Availability.Contains("U"))
                {
                    viewModel.Sunday = true;
                }
            }
            //return Ok();
            return View("_EditEmployeeModalPartial", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EmployeeViewModel viewModel)
        {
            string daysToChars = "";
            if (viewModel.Monday)
            {
                daysToChars += "M";
            }
            if (viewModel.Tuesday)
            {
                daysToChars += "T";
            }
            if (viewModel.Wednesday)
            {
                daysToChars += "W";
            }
            if (viewModel.Thursday)
            {
                daysToChars += "R";
            }
            if (viewModel.Friday)
            {
                daysToChars += "F";
            }
            if (viewModel.Saturday)
            {
                daysToChars += "S";
            }
            if (viewModel.Sunday)
            {
                daysToChars += "U";
            }

            viewModel.Employee.Availability = daysToChars;
            //validation
            if (ModelState.IsValid)
            {
                //updatng a employee in the database
                _db.Employees.Update(viewModel.Employee);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(viewModel);
            }
        }


        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var selectedEmployee = _db.Employees.Find(id);
            if (selectedEmployee == null)
            {
                return NotFound();
            }
            _db.Employees.Remove(selectedEmployee);
            _db.SaveChanges();
            return RedirectToAction("Index");
            
        }

        [HttpPost]
        public IActionResult Selection(int id)
        {
            SelectedEmployee = _db.Employees.Find(id);
            //if (SelectedEmployee != null)
            //{
                return Json(SelectedEmployee.Name);
            //}
            //return View("Index", id);

        }

        //public void UpdateEmployees(int? id)
        //{

        //    if (EmployeeList == null)
        //    {
        //        EmployeeList = new List<Employee>();
        //    }
        //    if (id != null)
        //    {
        //        Employee employee = _db.Employees.Find(id);
        //        EmployeeList.Append(employee);
        //    }
        //    else
        //    {
        //        EmployeeList = _db.Employees;
        //    }
        //}

        public IActionResult ModifyEmployee(EmployeeViewModel viewModel)
        {
            //added viewModel and daysToChar conversion for employee.Availability (May or may not work here)
            //EmployeeViewModel viewModel = new EmployeeViewModel();

            //FIX ME!!! move days to charactors to its own class or function
            //convert days to characters

            string daysToChars = "";
            if (viewModel.Monday)
            {
                daysToChars += "M";
            }
            if (viewModel.Tuesday)
            {
                daysToChars += "T";
            }
            if (viewModel.Wednesday)
            {
                daysToChars += "W";
            }
            if (viewModel.Thursday)
            {
                daysToChars += "R";
            }
            if (viewModel.Friday)
            {
                daysToChars += "F";
            }
            if (viewModel.Saturday)
            {
                daysToChars += "S";
            }
            if (viewModel.Sunday)
            {
                daysToChars += "U";
            }

            viewModel.Employee.Availability = daysToChars;

            //further validation explained in ClientController
            //validation
            if (ModelState.IsValid)
            {
                //adding an employee to database
                _db.Employees.Add(viewModel.Employee);

                _db.SaveChanges();
                //redirecting to the main employee page with list of employees
                //int newEmployeeId = viewModel.Employee.Id;
                //UpdateEmployees(newEmployeeId);
                return Ok(true);


            }
            //else
            //{
            //return JsonResult(ModelState.IsValid);
            return PartialView("_AddEmployeeModalPartial", viewModel);
            //}

        }
    }
    
}

