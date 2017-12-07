using System.Collections.Generic;

namespace Costs.Preview.Models
{
	public class EmployeeCostsDTO : TotalCostsDTO
	{
		public EmployeeCostsDTO()
		{
			Employees = new List<EmployeeModel>();
		}

		public List<EmployeeModel> Employees { get; set; }
	}

	public class TotalCostsDTO
	{
		public TotalCostsDTO()
		{
			TotalCosts = 0;
			TotalCostsAnnualSalaries = 0;
			TotalCostsAnnualBenefits = 0;
			TotalEmployees = 0;
			TotalDependents = 0;
			TotalDiscounts = 0;
			TotalCostsAnnualBenefitsEmployees = 0;
			TotalCostsAnnualBenefitsDependents = 0;
			TotalDiscountsEmployees = 0;
			TotalDiscountsDependents = 0;
		}

		public decimal TotalCosts { get; set; }
		public decimal TotalCostsAnnualSalaries { get; set; }
		public decimal TotalCostsAnnualBenefits { get; set; }
		public decimal TotalDiscounts { get; set; }

		public decimal TotalEmployees { get; set; }
		public decimal TotalDependents { get; set; }

		public decimal TotalCostsAnnualBenefitsEmployees { get; set; }
		public decimal TotalCostsAnnualBenefitsDependents { get; set; }
		public decimal TotalDiscountsEmployees { get; set; }
		public decimal TotalDiscountsDependents { get; set; }
	}
}
