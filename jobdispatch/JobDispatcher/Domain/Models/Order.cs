using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobDispatcher.Domain.Models
{
    public class Order
    {
        public Order() { }
        public int OrderID { get; set; }
        public int Status { get; set; }
    }
}
