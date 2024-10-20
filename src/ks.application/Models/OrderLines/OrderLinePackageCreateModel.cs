using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ks.application.Models.OrderLines
{
    public class OrderLinePackageCreateModel
    {
        public Guid OrderId { get; set; }
        public Guid FishPackageId { get; set; }
    }
}
