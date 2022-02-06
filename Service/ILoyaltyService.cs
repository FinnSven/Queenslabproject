using Queenslab_Project.Datasets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Queenslab_Project.Service
{
   public interface ILoyaltyService
    {
        PurchaseResponse ServoData(PurchaseRequest request);
        void EditServoData(Guid customerid, string loyaltycard, DateTime transactiondate, IList<BasketData> basketData);

        void PopulateBasket(Guid customerid, BasketData basketData);

        void SaveTotal(Guid customerid, double totalDiscount, double grandTotal, double totalAmount, double totalPoints);

        double CalculatePoints(string productId,DateTime transactionDate, int quantity, double unitPrice);

        double CalculateDiscount(string productId, DateTime transactionDate, int quantity, double unitPrice);
        double CalculateTotal(int quantity, double unitPrice);
    }
}