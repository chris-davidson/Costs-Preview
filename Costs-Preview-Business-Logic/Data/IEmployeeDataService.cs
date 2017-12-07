using System.Collections.Generic;
using Costs.Preview.Models;

namespace Costs_Preview_Business_Logic.Data
{
	public interface IEmployeeDataService
	{
		int Add(EmployeeModel employee);
		bool Delete(int id);
		EmployeeModel GetEmployee(int id);
		IEnumerable<EmployeeModel> GetAll();
	}
}