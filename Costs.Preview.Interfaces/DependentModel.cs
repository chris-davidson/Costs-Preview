using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Costs.Preview.Models
{
	public class DependentModel : Individual
	{
		public DependentModel()
		{
		}
		public int EmployeeId { get; set; }
	}
}
