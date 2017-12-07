using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Costs.Preview.Models;
using System.Collections.Generic;

namespace Costs_Preview_Business_Logic.Data.Tests
{
    [TestClass()]
    public class EmployeeDataServiceTests
    {
		[TestMethod]
		public void TestAddingEmployee()
		{
			// Arrange
			var service = new EmployeeDataService();
			var employee = new EmployeeModel
			{
				FirstName = "Joseph",
				LastName = "Martin",
				PayPerPeriod = 2000
			};

			// Act
			var result = service.Add(employee);

			// Assert
			Assert.IsTrue(result > -1);
			service.Delete(result);			
		}

		[TestMethod]
		public void TestAddingEmployeeWithDependent()
		{
			// Arrange
			var service = new EmployeeDataService();
			var employee = new EmployeeModel
			{
				FirstName = "Joseph",
				LastName = "Martin",
				PayPerPeriod = 2000,
				Dependents = new List<DependentModel>
					{
						new DependentModel { FirstName = "Linda", LastName = "Martin" },
						new DependentModel { FirstName = "Steve" , LastName = "Martin" }
					}
			};

			// Act
			var employeeId = service.Add(employee);
			var lookup = service.GetEmployee(employeeId);

			// Assert
			Assert.IsTrue(employeeId > -1);
			Assert.IsTrue(lookup != null);
			Assert.IsTrue(lookup.Dependents.Count == 2);
			service.Delete(employeeId);
		}

		[TestMethod]
		public void TestGettingEmployee()
		{
			// Arrange
			var service = new EmployeeDataService();
			var employee = new EmployeeModel
			{
				FirstName = "Joseph",
				LastName = "Martin",
				PayPerPeriod = 2000
			};

			// Act
			var id = service.Add(employee);
			var result = service.GetEmployee(id);

			// Assert
			Assert.IsTrue(result != null);
			Assert.IsTrue(result.Id > -1);
			Assert.IsTrue(result.FirstName == "Joseph");
			Assert.IsTrue(result.LastName == "Martin");
			service.Delete(result.Id);
		}

		[TestMethod]
		public void TestDeletingEmployee()
		{
			// Arrange
			var service = new EmployeeDataService();
			var employee = new EmployeeModel
			{
				FirstName = "Joseph",
				LastName = "Martin",
				PayPerPeriod = 2000
			};

			// Act
			var id = service.Add(employee);
			var result = service.GetEmployee(id);

			// Assert
			Assert.IsTrue(result.Id > -1);
			service.Delete(id);
			var lookup = service.GetEmployee(id);
			Assert.IsTrue(lookup == null);
		}
	}
}