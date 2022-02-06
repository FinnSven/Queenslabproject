using System;
using System.Collections.Generic; 

namespace Queenslab_Project.Models

{

    public partial class TotalDetail
 {
     public int TotalID {get; set;}
     public Guid customerId   {get; set;}
     public double? DiscountApplied {get; set;}
     public double Grandtotal {get; set;}
     public double TotalAmount {get; set;}
     public int? PointsTotal {get; set;}
     public DateTime? DandtimeInser {get; set;}

     public virtual CustTrans Customer {get; set;}

 }   
}