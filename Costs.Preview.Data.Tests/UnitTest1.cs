using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Costs.Preview.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Costs.Preview.Data.Tests
{
	[TestClass]
	public class CostsPreviewDataTests
	{
		[TestMethod]
		public void AddEmployeeWithDependentsShouldSucceed()
		{
			//  Arrange
			var EmployeesData = new List<EmployeeModel>
			{
				new EmployeeModel
				{ FirstName = "Alfred", LastName = "Jones",
				  Dependents = new List<DependentModel>
					{
						new DependentModel { FirstName = "Bob", LastName = "Jones" },
						new DependentModel { FirstName = "Angela" , LastName = "Jones" }
					}
				}
			};
		}
		
		//  Act


		//  Assert

	}
}
