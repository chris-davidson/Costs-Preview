using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Costs.Preview.Models
{
	public class EmployeeModel : IIndividual
	{
		public EmployeeModel()
		{
			Dependents = new List<DependentModel>();
		}

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
		public decimal PayPerPeriod { get; set; }
        public ICollection<DependentModel> Dependents { get; set; }

	}
}
