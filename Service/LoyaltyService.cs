using Queenslab_Project.Datasets;
using Queenslab_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Queenslab_Project.Service
{
    public class LoyaltyService : ILoyaltyService
    {


        static double totalPoints =0;
        static double totalDiscount =0;
        static double totalAmount = 0;

        public LoyaltyService()
        {


        }

        //This can be replaced by injecting the context as dependancy, however I am skipping it here as I am not using an interface for the DBContext.

        sampleContext _context = new sampleContext();

        public double CalculatePoints(string productId, DateTime transactionDate, int quantity, double unitPrice)
        {
            var startDate = (from s in _context.Set<Promo>()
                             where s.Category == "Any"
                             select
                          s.StartDate).FirstOrDefault();

            var endDate = (from e in _context.Set<Promo>()
                             where e.Category == "Any"
                             select
                          e.EndDate).FirstOrDefault();


            if(transactionDate >= startDate && transactionDate <= endDate) 
            {
                var newYearPromo = (from n in _context.Set<Promo>()
                                     where n.Category == "Any"
                                     select
                                  n.PointsPerDollar).FirstOrDefault();
                return Math.Round((double)newYearPromo * quantity);
            }
            else 
            {
                var category = (from c in _context.Set<ProductDetails>()
                                where c.productId == productId
                                select
                             c.Category).FirstOrDefault();
                var points = ((from p in _context.Set<Promo>()
                               where p.Category == category
                               && p.EndDate >= transactionDate && p.StartDate <= transactionDate
                               select
                            p.PointsPerDollar).FirstOrDefault());
                return Math.Round((double)points * quantity); ;
            }
   
        }

        public double CalculateTotal(int quantity, double unitPrice)
        {
            return (quantity * unitPrice);
        }

        public void PopulateBasket(Guid customerid, BasketData basketData)
        {
            Basket basket = new Basket();
            basket.CustomerId = customerid;
            basket.ProductId = basketData.ProductId;
            basket.Quantity = basketData.Quantity;
            basket.UnitPrice = basketData.UnitPrice;
            _context.Basket.Add(basket);
            _context.SaveChanges();
        }

        public void EditServoData(Guid customerid, string loyaltycard, DateTime transactiondate, IList<BasketData> BasketData)
        {
           
            CustTrans custTrans = new CustTrans();
            custTrans.CustomerId = customerid;
            custTrans.LoyaltyCard = loyaltycard;
            custTrans.DandtimeTrans = transactiondate;
            custTrans.DandtimeInser = DateTime.Now;
            _context.CustTrans.Add(custTrans);
            _context.SaveChanges();

            foreach (BasketData basket in BasketData)
            {
                PopulateBasket(customerid, basket);
                totalPoints = totalPoints + CalculatePoints(basket.ProductId, transactiondate, basket.Quantity, basket.UnitPrice);
                totalDiscount = totalDiscount + CalculateDiscount(basket.ProductId, transactiondate, basket.Quantity, basket.UnitPrice);
                totalAmount = totalAmount + CalculateTotal(basket.Quantity, basket.UnitPrice);
            }

            SaveTotal(customerid, totalDiscount, totalAmount - totalDiscount, totalAmount, totalPoints);
        }

        public PurchaseResponse ServoData(PurchaseRequest request)
        {
            EditServoData(request.CustomerId, request.LoyaltyCard, request.TransactionDate, request.BasketData);
            
            return new PurchaseResponse
            {
                CustomerId = request.CustomerId,
                LoyaltyCard = request.LoyaltyCard,
                TransactionDate = request.TransactionDate,
                DiscountApplied = totalDiscount,
                TotalAmount = totalAmount,
                GrandTotal = totalAmount - totalDiscount,
                PointsTotal = totalPoints

            };
        }

        public double CalculateDiscount(string productId, DateTime transactionDate, int quantity, double unitPrice)
        {
            var discount = ((from d in _context.Set<Discount>()
                           where d.productId== productId
                           && d.EndDate >= transactionDate && d.StartDate <= transactionDate
                           select
                        d.Percent).FirstOrDefault());
            if(!discount.Equals(null))
            {
                discount = (discount * unitPrice) * quantity;
                return discount;
            }
            else
            {
                return 0;
            }
        }

        //This could be retrived either from the database - tblTotalDetails if data integrity is the priority or it can be returned from here if the priority is to save DB calls.
        public void SaveTotal(Guid customerid, double totalDiscount, double grandTotal, double totalAmount, double totalPoints)
        {
            TotalDetail totalDetail = new TotalDetail();
            totalDetail.customerId = customerid;
            totalDetail.DiscountApplied = totalDiscount;
            totalDetail.Grandtotal = grandTotal;
            totalDetail.TotalAmount = totalAmount;
            totalDetail.PointsTotal = Convert.ToInt32(totalPoints);
            totalDetail.DandtimeInser = DateTime.Now;
            _context.TotalDetail.Add(totalDetail);
            _context.SaveChanges();
        }
    }
}