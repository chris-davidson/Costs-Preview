using System;
using System.Collections.Generic;
using Costs.Preview.Models;
using Costs_Preview_Business_Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Costs.Preview.Business.LogicTests.Costs
{
	[TestClass]
	public class EmployeeCostsTests
	{
		private EmployeeCosts employeeCostsTestClass;

		private List<EmployeeModel> defaultEmployeeModelList = new List<EmployeeModel>
		{
			new EmployeeModel
				{
					FirstName = "Alfred",
					LastName = "Jones",
					PayPerPeriod = 2000,
					Dependents = new List<DependentModel>
					{
						new DependentModel { FirstName = "Bob", LastName = "Jones" },
						new DependentModel { FirstName = "Angela" , LastName = "Jones" }
					}
				},
				new EmployeeModel
				{
					FirstName = "Thad",
					LastName = "Smith",
					PayPerPeriod = 2000,
					Dependents = new List<DependentModel>
					{
						new DependentModel { FirstName = "Janie", LastName = "Smith" }
					}
				},
				new EmployeeModel
				{
					FirstName = "Robert",
					LastName = "Johnson",
					PayPerPeriod = 2000
				},
				new EmployeeModel
				{
					FirstName = "David",
					LastName = "Walsh",
					PayPerPeriod = 2000,
					Dependents = new List<DependentModel>
					{
						new DependentModel { FirstName = "Bob", LastName = "Walsh" },
						new DependentModel { FirstName = "Angela" , LastName = "Walsh" }
					}
				}

		};

		public class TestIIndividual : IIndividual
		{
			public string FirstName { get; set; }
			public int Id { get; set; }
			public string LastName { get; set; }
		}

		public void SetupMockTestClass(List<EmployeeModel> employeeModelList = null)
		{
			if (employeeModelList == null) employeeModelList = defaultEmployeeModelList;
			employeeCostsTestClass = new EmployeeCosts(employeeModelList);
		}

		[TestMethod]
		public void TestEmployeeCountAndTotalSalaryCalculations()
		{
			SetupMockTestClass();
			var result = employeeCostsTestClass.GetEmployeeCosts();
			var employeeCount = result.TotalEmployees;
			Assert.AreEqual(result.TotalEmployees, 4);

			var value = 26 * employeeCount * 2000;
			Assert.AreEqual(result.TotalCostsAnnualSalaries, value);
		}

		[TestMethod]
		public void TestBenefitDiscount()
		{
			SetupMockTestClass();
			var result = employeeCostsTestClass.BenefitDiscount(new TestIIndividual { FirstName = "Alan", LastName = "Jones" }, 1000);
			Assert.IsTrue(result == 100);

			result = employeeCostsTestClass.BenefitDiscount(new TestIIndividual { FirstName = "Dava", LastName = "Jones" }, 500);
			Assert.IsTrue(result == 0);

			result = employeeCostsTestClass.BenefitDiscount(new TestIIndividual { FirstName = "Dava", LastName = "Anderson" }, 500);
			Assert.IsTrue(result == 50);

			result = employeeCostsTestClass.BenefitDiscount(new TestIIndividual { FirstName = "anna", LastName = "Jones" }, 1000);
			Assert.IsTrue(result == 100);
		}

		[TestMethod]
		public void TestCalculateAnnualCosts()
		{
			SetupMockTestClass(new List<EmployeeModel> {
				new EmployeeModel
				{
					FirstName = "Alfred",
					LastName = "Jones",
					PayPerPeriod = 2000,
					Dependents = new List<DependentModel>
					{
						new DependentModel { FirstName = "Bob", LastName = "Jones" },
						new DependentModel { FirstName = "Angela" , LastName = "Jones" }
					}
				}
			});
			var result = employeeCostsTestClass.CalculateAnnualCosts();

			Assert.IsTrue(result.TotalEmployees == 1);
			Assert.IsTrue(result.TotalDependents == 2);
			Assert.IsTrue(result.TotalCostsAnnualBenefitsEmployees == 900);
			Assert.IsTrue(result.TotalDiscountsEmployees == 100);
			Assert.IsTrue(result.TotalCostsAnnualBenefitsDependents == 950);
			Assert.IsTrue(result.TotalDiscountsDependents == 50);
		}
	}
}
