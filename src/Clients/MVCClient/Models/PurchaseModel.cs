using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCClient.Models
{
    public class PurchaseModel
    {
		public Guid Id { get; set; }

		public decimal Price { get; set; }

		public int Amount { get; set; }
	}
}
