using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ks.application.Models.OrderLines
{
    public class OrderLineViewModel
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid? FishId { get; set; }
        public Guid? FishPackageId { get; set; }
    }
}
