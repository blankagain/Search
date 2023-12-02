using Microsoft.AspNetCore.Mvc;
using Search.Models;
using Search.Models.Domain;
using Search.Data;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Search.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly MVCDemoDbContext mvcDemoDbContext;

        public EmployeesController(MVCDemoDbContext mvcDemoDbContext)
        {
            this.mvcDemoDbContext = mvcDemoDbContext;
            
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await mvcDemoDbContext.Employees.ToListAsync();
            return View(employees);
        }

        // Add new employee
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Add(AddEmployeeViewModel addEmployeeRequest)
        {
            var employee= new Employee()
            {
                 Id = Guid.NewGuid(),
                Name = addEmployeeRequest.Name,
                Email = addEmployeeRequest.Email,
                Salary = addEmployeeRequest.Salary,
                Department = addEmployeeRequest.Department,
                DOB = addEmployeeRequest.DOB
            };

            await mvcDemoDbContext.Employees.AddAsync(employee);
            await mvcDemoDbContext.SaveChangesAsync();
            return RedirectToAction("Add");
        }

        // Get Request for Update
        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var employee= await mvcDemoDbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);

            if (employee != null)
            {
                var viewModel = new UpdateEmployeeViewModel()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    Salary = employee.Salary,
                    Department = employee.Department,
                    DOB = employee.DOB

                };

                return await Task.Run(() => View("View", viewModel));
            }

            return RedirectToAction("Index");

           
        }

        // Update the Database
        [HttpPost]
        public async Task<IActionResult> View(UpdateEmployeeViewModel model)
        {
            var employee = await mvcDemoDbContext.Employees.FindAsync(model.Id);
            if (employee != null)
            {
                employee.Salary = model.Salary;
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Department = model.Department;
                employee.DOB = model.DOB;

                await mvcDemoDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        //Delete
        [HttpPost] 
        public async Task<IActionResult> Delete (UpdateEmployeeViewModel model)
        {
            var employee = mvcDemoDbContext.Employees.FindAsync(model.Id);

            if (employee != null)
            {
                mvcDemoDbContext.Employees.Remove(await employee);
                await mvcDemoDbContext.SaveChangesAsync();

                return RedirectToAction("Index");

            }
            return RedirectToAction("Index");
        }

        //Search

        [HttpGet]
        public async Task<IActionResult> SearchIndex(string SearchString)
        {
            ViewData["Filter"] = SearchString;
            var employee = from Name in mvcDemoDbContext.Employees
                           select Name;

            if (!String.IsNullOrEmpty(SearchString))
            {
                employee = employee.Where(x => x.Name == SearchString);

            }
            return View(employee);
        }



    }
}
