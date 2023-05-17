using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class EmployeeController : Controller
    {


        private readonly APIGateway apiGateway;

        public EmployeeController(APIGateway ApiGateway)
        {
            this.apiGateway = ApiGateway;
        }


        public IActionResult Index()
        {
            /*List<Employee> employees = new List<Employee>();*/
            List<Employee> employees;
            //api get will come
            employees = apiGateway.ListEmployees();
            return View(employees);
        }


        [HttpGet]
        public IActionResult Create()
        {
            Employee employee = new Employee();
            return View(employee);
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            apiGateway.CreateEmployee(employee);
            return RedirectToAction("index");
        }


        public IActionResult Details(int Id)
        {
            Employee employee = new Employee();
            employee = apiGateway.GetEmployee(Id);
            return View(employee);
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            Employee employee;
            employee = apiGateway.GetEmployee(Id);
            return View(employee);
        }

        [HttpPost]
        public IActionResult Edit(Employee employee)
        {
            apiGateway.UpdateEmployee(employee);
            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Delete (int Id)
        {
            Employee employee;
            employee = apiGateway.GetEmployee(Id);
            return View(employee);
        }

        [HttpPost]
        public IActionResult Delete (Employee employee)
        {
            apiGateway.DeleteEmployee(employee.Id);
            return RedirectToAction("Index");
        }




    }
}
