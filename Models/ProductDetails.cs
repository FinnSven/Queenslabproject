using System;
using System.Collections.Generic;

namespace Queenslab_Project.Models

{
    public partial class ProductDetails
    {
        public ProductDetails()
        {

            TblDiscount = new HashSet<Discount>();
        }

        public string productId {get; set;}
        public string ProductName {get; set;}
        public string Category {get; set;}
         public double UnitPrice {get; set;}
        public DateTime? DandtimeInser {get; set;}

        public virtual ICollection<Discount> TblDiscount {get; set;}

    }
}