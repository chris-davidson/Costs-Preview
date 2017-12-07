using System.Web.Mvc;
using Costs.Preview.Models;
using Costs_Preview_Business_Logic;
using Costs_Preview_Business_Logic.Data;

namespace Costs_Preview.Controllers
{
	public class CostsController : Controller
    {
		private IEmployeeDataService _employeeDataService;
		public CostsController()
		{
			_employeeDataService = new EmployeeDataService();
		}
		public ActionResult IndexCosts()
        {
			var ec = new EmployeeCosts();
			var data = ec.GetEmployeeCosts();
			return View(data);
        }

		private JsonResult GetAllData()
		{
			var ec = new EmployeeCosts();
			var data = ec.GetEmployeeCosts();
			var response = Json(data, JsonRequestBehavior.AllowGet);
			return response;
		}

		[HttpGet]
		public JsonResult GetData()
		{
			return GetAllData();
		}

		[HttpGet]
		public JsonResult GetEmployee(int id)
		{
			var employee = _employeeDataService.GetEmployee(id);
			return Json(employee, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public JsonResult AddEmployee(EmployeeModel employee)
		{
			_employeeDataService.Add(employee);
			return GetAllData();
		}

		[HttpGet]
		public JsonResult DeleteEmployee(int id)
		{
			_employeeDataService.Delete(id);
			return GetAllData();
		}
	}
}