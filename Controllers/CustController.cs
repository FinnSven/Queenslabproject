using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Queenslab_Project.Datasets;
using Queenslab_Project.Models; 
using Queenslab_Project.Service;

namespace Queenslab_Project.Controllers
{
    [ApiController]
    [Route("api/[cust]")]
    // I would insert a style here. 
    public class CustController : ControllerBase
    {

        private readonly ILoyaltyService _LoyaltyService;

        public CustController(ILoyaltyService LoyaltyService)
        {
            _LoyaltyService= LoyaltyService;
        }

        [HttpPost]
        public PurchaseRequest CustData( [FromBody] PurchaseResponse purchaseResponse)
        {
            return _LoyaltyService.ServoData(purchaseResponse);
        }
    }
}