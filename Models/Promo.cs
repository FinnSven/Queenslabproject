using System;
using System.Collections.Generic;

namespace Queenslab_Project.Models

{
    public partial class Promo
    {
        public string PromoID {get; set;}
        public string PromoName {get; set;}
        public string Category {get; set;}

        public int PointsPerDollar {get; set;}

        public DateTime? DandtimeInser {get; set;}
        public DateTime? StartDate {get; set;}
        public DateTime? EndDate {get; set;}

    }    
}