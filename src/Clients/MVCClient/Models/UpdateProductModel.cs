using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCClient.Models
{
    public class UpdateProductModel
    {
        public string ProductId { get; set; }

        public int NewStack {  get; set; }
    }
}
