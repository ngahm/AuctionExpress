using AuctionExpress.Data;
using AuctionExpress.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionExpress.Service
{
    public class TransactionService
    {
        private readonly Guid _userId;

        public TransactionService(Guid userId)
        {
            _userId = userId;
        }
        //POST Transaction

        public bool CreateTransaction(TransactionCreate model)
        {
            var entity = new Transaction()
            {
                ProductId = model.ProductId,
                IsPaid = model.IsPaid
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Transaction.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        //GET Transaction Currently returning all transactions where the signed in user is the winner

        public IEnumerable<TransactionListItem> GetTransactions()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Transaction
                    .ToList()
                    .Where(e => e.WinningBid.BidderId == _userId.ToString())
                    .Select(e => new TransactionListItem
                    {
                        TransactionId = e.TransactionId,
                        ProductId = e.ProductId,
                        ProductName = e.TransactionProduct.ProductName,
                        SellerId = e.TransactionProduct.ProductSeller,
                        IsPaid = e.IsPaid,
                        PaymentDate = e.PaymentDate
                    }
                    );
                return query.ToArray();
            }
        }

        //Get Transaction By ID
        public TransactionDetail GetTransactionById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Transaction
                    .Where(e => e.TransactionId == id)
                    .FirstOrDefault();
                return
                    new TransactionDetail
                    {
                        TransactionId = entity.TransactionId,
                        ProductId = entity.ProductId,
                        ProductName = entity.TransactionProduct.ProductName,
                        BuyerId = entity.WinningBid.BidderId,
                        BuyerName = entity.WinningBid.Buyer.UserName,
                        BuyerEmail = entity.WinningBid.Buyer.Email,
                        HighestBid = entity.TransactionProduct.HighestBid,
                        IsPaid = entity.IsPaid,
                        PaymentDate = entity.PaymentDate
                    };
            }
        }


        //Update Transaction <--Updates IsPaid, currently Sellers allowed to edit

        public bool UpdateTransaction(TransactionEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {

                var entity =
                    ctx
                    .Transaction
                    .Single(e => e.TransactionId == model.TransactionId && e.TransactionProduct.ProductSeller == _userId.ToString());

                entity.IsPaid = model.IsPaid;
                if (model.IsPaid)
                    entity.PaymentDate = DateTimeOffset.Now;
                else
                    entity.PaymentDate = null;
                return ctx.SaveChanges() == 1;
            }
        }

        public string DeleteTransaction(int transactionId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Transaction
                        .Single(e => e.TransactionId == transactionId);
                if (entity.TransactionProduct.ProductSeller == _userId.ToString())
                {
                    try
                    {
                        ctx.Transaction.Remove(entity);
                        ctx.SaveChanges();

                        return "Transaction successfully deleted";
                    }
                    catch (Exception e)
                    {
                        return e.Message;
                    }
                }

                return "User unauthorized to delete this transaction.";
            }

        }
    }

}