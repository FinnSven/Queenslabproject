using System;
using System.Collections.Generic;

namespace Queenslab_Project.Models

{
    public partial class Discount
    {
        public string discountId {get; set;}
        public string DiscountLoyalty{get; set;}
        public DateTime? DandtimeInser {get; set;}
        public double Percent {get; set;}
        public DateTime? StartDate {get; set;}
        public DateTime? EndDate {get; set;}
        public string productId {get; set;}

        public virtual ProductDetails Product {get; set;}
    }    
}