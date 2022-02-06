using System;
using System.Collections.Generic;

namespace Queenslab_Project.Models
{
    public partial class Basket
    {
        public int BasketId { get; set; }
        public Guid CustomerId { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }

        public virtual CustTrans Customer { get; set; }
    }
}