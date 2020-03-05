using AuctionExpress.Data;
using AuctionExpress.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionExpress.Service
{
    public class BidService
    {

        public bool CreateBid(BidCreate model)
        {
            var entity = new Bid()
            {
                
                ProductId = model.ProductId,
                BidderId = model.BidderId,
                BidPrice = model.BidPrice,
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Bid.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }



        public IEnumerable<BidListItem> GetBid(int productid)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Bid
                        .Where(e => e.ProductId == productid)                 /*get all rows with the common productid*/ 
                        .Select(
                            e =>
                                new BidListItem
                                {
                                    BidId = e.BidId,
                                    ProductId = e.ProductId,
                                    BidderId = e.BidderId,
                                    BidPrice= e.BidPrice,
                                }
                        );

                return query.ToArray();
            }
        }

        public BidDetail GetBidById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Bid
                        .Single(e => e.BidId == id);           /*target one unique identifier*/
                return
                    new BidDetail
                    {
                        BidId = entity.BidId,
                        ProductId = entity.ProductId,
                        BidderId = entity.BidderId,
                        TimeOfBid = entity.TimeOfBid,
                        BidPrice = entity.BidPrice
                    };

             }
        }




    }
}
