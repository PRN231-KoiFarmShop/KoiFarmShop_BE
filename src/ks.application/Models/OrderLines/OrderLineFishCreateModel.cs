using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ks.application.Models.OrderLines
{
    public class OrderLineFishCreateModel
    {
        public Guid OrderId { get; set; }
        
        public Guid? FishId { get; set; }        
    }
}
