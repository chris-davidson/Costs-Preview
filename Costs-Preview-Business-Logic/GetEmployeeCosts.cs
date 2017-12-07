using System;
using System.Collections.Generic;
using Costs.Preview.Models;
using Costs_Preview_Business_Logic.Data;

namespace Costs_Preview_Business_Logic
{
	public class EmployeeCosts
	{
		const decimal paychecksPerYear = 26;
		const decimal benefitCostPerEmployee = 1000;
		const decimal benefitCostPerDependent = 500;

		public EmployeeCosts()
		{
			_employees = new List<EmployeeModel>();
			_employeeDataService = new EmployeeDataService();
			Load();
		}

		public EmployeeCosts(List<EmployeeModel> employees)
		{
			_employees = employees;
			_employeeDataService = new EmployeeDataService();
		}

		private List<EmployeeModel> _employees;
		private EmployeeDataService _employeeDataService;

		/// <summary>
		/// collects all totals information and employee/dependents information
		/// </summary>
		/// <returns></returns>
		public EmployeeCostsDTO GetEmployeeCosts()
		{
			var employeeCostsDto = new EmployeeCostsDTO();

			employeeCostsDto.Employees = _employees;

			var totalCosts = CalculateAnnualCosts();

			employeeCostsDto.TotalCostsAnnualSalaries = totalCosts.TotalCostsAnnualSalaries;
			employeeCostsDto.TotalEmployees = totalCosts.TotalEmployees;
			employeeCostsDto.TotalDependents = totalCosts.TotalDependents;

			employeeCostsDto.TotalCostsAnnualBenefitsEmployees = totalCosts.TotalCostsAnnualBenefitsEmployees;
			employeeCostsDto.TotalCostsAnnualBenefitsDependents = totalCosts.TotalCostsAnnualBenefitsDependents;
			employeeCostsDto.TotalCostsAnnualBenefits = employeeCostsDto.TotalCostsAnnualBenefitsEmployees + employeeCostsDto.TotalCostsAnnualBenefitsDependents;

			employeeCostsDto.TotalDiscountsEmployees = totalCosts.TotalDiscountsEmployees;
			employeeCostsDto.TotalDiscountsDependents = totalCosts.TotalDiscountsDependents;
			employeeCostsDto.TotalDiscounts = employeeCostsDto.TotalDiscountsEmployees + employeeCostsDto.TotalDiscountsDependents;

			employeeCostsDto.TotalCosts = employeeCostsDto.TotalCostsAnnualSalaries + employeeCostsDto.TotalCostsAnnualBenefits;
			return employeeCostsDto;
		}

		/// <summary>
		/// code to determine any benefit discount
		/// </summary>
		/// <param name="employee"></param>
		/// <param name="baseCost"></param>
		/// <returns>decimal</returns>
		public  decimal BenefitDiscount(IIndividual employee, decimal baseCost)
		{
			if (employee.FirstName.ToUpper().StartsWith("A") || employee.LastName.ToUpper().StartsWith("A"))
			{
				return baseCost * (decimal)0.10;
			}
			return 0;
		}

		/// <summary>
		/// class to store individual benefit calculations
		/// </summary>
		protected class IndividualBenefitCalculation
		{
			private decimal _discountAmount;
			private decimal _actualAmount;

			public IndividualBenefitCalculation(decimal baseCost, decimal discountAmount)
			{
				_actualAmount = baseCost - discountAmount;
				_discountAmount = discountAmount;
			}

			public decimal AcutalAmount => _actualAmount;
			public decimal Discount => _discountAmount;
		}

		/// <summary>
		/// Returns the actual benefits cost of individual and the discount if any
		/// </summary>
		/// <param name="employee"></param>
		/// <param name="baseCost"></param>
		/// <returns>IndividualBenefitCalculation</returns>
		private IndividualBenefitCalculation BenefitCalculation(IIndividual employee, decimal baseCost)
		{
			var discount = BenefitDiscount(employee, baseCost);
			var result = new IndividualBenefitCalculation(baseCost, discount);
			return result;
		}

		/// <summary>
		/// Creates totals calcualations into a DTO
		/// </summary>
		/// <returns>TotalCostsDTO</returns>
		public TotalCostsDTO CalculateAnnualCosts()
		{
			TotalCostsDTO totalCosts = new TotalCostsDTO();
			totalCosts.TotalEmployees = _employees.Count;
			foreach (var employee in _employees)
			{
				totalCosts.TotalCostsAnnualSalaries += employee.PayPerPeriod * paychecksPerYear;
				var benefits = BenefitCalculation(employee, benefitCostPerEmployee);
				totalCosts.TotalCostsAnnualBenefitsEmployees += benefits.AcutalAmount;
				totalCosts.TotalDiscountsEmployees += benefits.Discount;

				foreach (var dependent in employee.Dependents)
				{
					totalCosts.TotalDependents += 1;
					var dependBenefits = BenefitCalculation(dependent, benefitCostPerDependent);
					totalCosts.TotalCostsAnnualBenefitsDependents += dependBenefits.AcutalAmount;
					totalCosts.TotalDiscountsDependents += dependBenefits.Discount;
				}
			}
			return totalCosts;
		}

		/// <summary>
		/// Loads all employee with dependent information
		/// </summary>
		public void Load()
		{
			var employees = _employeeDataService.GetAll();
			_employees.Clear();
			foreach (var employee in employees)
			{
				_employees.Add(employee);
			}
		}
	}
}
