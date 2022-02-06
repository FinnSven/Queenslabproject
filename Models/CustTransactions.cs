using System;
using System.Collections.Generic; 

namespace Queenslab_Project.Models
{
    public partial class CustTrans
    {
        public CustTrans()
            {
                TotalDetail = new HashSet<TotalDetail>();
            }
            public Guid CustomerId {get; set;}
            public string LoyaltyCard {get; set;}
            public DateTime? DandtimeTrans {get; set;}
            public DateTime? DandtimeInser {get; set;}

            public virtual ICollection<Basket> Basket{get; set;}
            public virtual ICollection<TotalDetail> TotalDetail {get; set;}
        

    }

}