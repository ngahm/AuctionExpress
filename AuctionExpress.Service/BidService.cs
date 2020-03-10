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
        private readonly Guid _userId;         /*ApplicationUser*/

        public BidService(Guid userId)
        {
            _userId = userId;
        }


        public bool CreateBid(BidCreate model)
        {
            var entity = new Bid()
            {
                ProductId = model.ProductId,
                BidderId = _userId.ToString(),
                BidPrice = model.BidPrice,
            };
<<<<<<< HEAD
            if (entity.Auction == null)
                return entity;

            //look in product database for product matching product id and assign auction to this instance

            if (entity.Auction.HighestBid >= entity.BidPrice)
                return null;
            return entity;
        }

        public bool CreateBid(Bid entity)
        {

=======
>>>>>>> d175c2b9a02cc03175101a4c10ab4215ba9f7362
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
