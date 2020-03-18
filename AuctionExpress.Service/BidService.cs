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
        private readonly Guid _userId;

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
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Bid.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        //GET All Bids
        public IEnumerable<BidListItem> GetAllBids()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Bid
                        .Select(
                            e => new BidListItem
                            {
                                BidId = e.BidId,
                                ProductId = e.ProductId,
                                BidderId = e.BidderId,
                                BidPrice = e.BidPrice
                            }
                        );
                return query.ToList();
            }
        }

        //GET Bids By Product Id
        public IEnumerable<BidListItem> GetBid(int productid)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Bid
                        .Where(e => e.ProductId == productid)
                        .Select(
                            e =>
                                new BidListItem
                                {
                                    BidId = e.BidId,
                                    ProductId = e.ProductId,
                                    BidderId = e.BidderId,
                                    BidPrice = e.BidPrice,
                                }
                        );

                return query.ToArray();
            }
        }

        //GET Bid By Bid Id
        public BidDetail GetBidById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Bid
                        .Where(e => e.BidId == id)
                        .FirstOrDefault();
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

        //GET My Bids
        public IEnumerable<BidListItem> GetBidsByUser()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Bid
                        .Where(e => e.BidderId == _userId.ToString())
                        .Select(
                            e =>
                                new BidListItem
                                {
                                    BidId = e.BidId,
                                    ProductId = e.ProductId,
                                    BidderId = e.BidderId,
                                    BidPrice = e.BidPrice,
                                }
                        );
                return query.ToList();
            }
        }

        public string DeleteBid(int bidId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Bid
                        .Single(e => e.BidId == bidId);

                if (entity.BidderId == _userId.ToString())
                {
                    try
                    {
                        ctx.Bid.Remove(entity);
                        ctx.SaveChanges();
                        return "Bid successfully deleted";
                    }
                    catch (Exception e)
                    {
                        return e.Message;
                    }
                }
                return "User not Authorized to delete this bid.";
            }
        }

    }
}
