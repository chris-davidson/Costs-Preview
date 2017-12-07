using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Costs.Preview.Models;
using Costs.Preview.DAL;
using System.Data.Entity;

namespace Costs_Preview_Business_Logic.Data
{
	public class EmployeeDataService : IEmployeeDataService
	{
		/// <summary>
		/// Add employee with dependents
		/// </summary>
		/// <param name="employee"></param>
		/// <returns>added Employee Id or -1 on error</returns>
		public int Add(EmployeeModel employee)
		{
			try
			{
				using (var db = new CostsPreviewDbContext())
				{
					var result = db.Employees.Add(employee);
					db.SaveChanges();
					return result.Id;
				}
			}
			catch (Exception ex)
			{
				return -1;
			}
		}

		/// <summary>
		/// Delete employee by ID
		/// </summary>
		/// <param name="id"></param>
		/// <returns>boolean</returns>
		public bool Delete(int id)
		{
			try
			{
				using (var db = new CostsPreviewDbContext())
				{
					var employee = db.Employees.Include(e => e.Dependents).SingleOrDefault(e => e.Id == id);
					db.Employees.Remove(employee);
					db.SaveChanges();
					return true;
				}
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		/// <summary>
		/// Get all employee data
		/// </summary>
		/// <returns>IEnumerable<EmployeeModel></returns>
		public IEnumerable<EmployeeModel> GetAll()
		{
            using(var ctx = new CostsPreviewDbContext())
            {
                return ctx.Employees.Include(e => e.Dependents).ToList();
            }
		}

		/// <summary>
		/// Get Employee from database by ID
		/// </summary>
		/// <param name="id"></param>
		/// <returns>EmployeeModel or null</returns>
		public EmployeeModel GetEmployee(int id)
		{
			using (var db = new CostsPreviewDbContext())
			{
				var employee = db.Employees.Include(e => e.Dependents).SingleOrDefault(e => e.Id == id);
				
				return employee ?? null;
			}
		}
	}
}
