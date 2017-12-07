using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Costs.Preview.Models;

namespace Costs.Preview.Data.Layer.Models
{
	public class EmployeesData
	{
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public virtual ICollection<DependentModel> Dependents { get; set; }
	}
}
