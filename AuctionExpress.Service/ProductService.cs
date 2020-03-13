﻿using AuctionExpress.Data;
using AuctionExpress.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionExpress.Service
{
    public class ProductService
    {
        private readonly Guid _userId;

        public ProductService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateProduct(ProductCreate model)
        {
            var entity = new Product()
            {
                ProductName = model.ProductName,
                ProductCategoryId = model.ProductCategoryId,
                ProductDescription = model.ProductDescription,
                ProductQuantity = model.ProductQuantity,
                ProductStartTime = model.ProductStartTime,
                ProductCloseTime = model.ProductCloseTime,
                ProductSeller = _userId.ToString(),
                MinimumSellingPrice = model.MinimumSellingPrice
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Product.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<ProductListItem> GetProducts()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Product
                    .Where(e => e.ProductSeller == _userId.ToString())
                    .Select(e => new ProductListItem
                    {
                        ProductId = e.ProductId,
                        ProductName = e.ProductName,
                        CategoryName = e.ProductCategoryCombo.CategoryName,
                        ProductQuantity = e.ProductQuantity,
                      // ProductIsActive = e.DetermineIsActive(),
                        ProductStartTime = e.ProductStartTime,
                        ProductCloseTime = e.ProductCloseTime,
                        MinimumSellingPrice = e.MinimumSellingPrice
                    }
                    );
                return query.ToList();
                
            }
        }

        public ProductDetail GetProductById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Product
                    .Where(e => e.ProductId == id)
                    .FirstOrDefault();
                if (entity == null)
                    return null;

                return
                    new ProductDetail
                    {
                        ProductId=entity.ProductId,
                        ProductName = entity.ProductName,
                        ProductCategoryId = entity.ProductCategoryId,
                        ProductDescription = entity.ProductDescription,
                        ProductQuantity = entity.ProductQuantity,
                        ProductIsActive = entity.ProductIsActive,
                        ProductStartTime = entity.ProductStartTime,
                        ProductCloseTime = entity.ProductCloseTime,
                        ProductSeller = entity.ProductSeller,
                        HighestBid = entity.HighestBid,
                        MinimumSellingPrice = entity.MinimumSellingPrice

                    };
            }
        }
        public bool UpdateProduct(ProductEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Product
                    .Single(e => e.ProductId == model.ProductId && e.ProductSeller == _userId.ToString());

                entity.ProductId = model.ProductId;
                entity.ProductName = model.ProductName;
                entity.ProductCategoryId = model.ProductCategoryId;
                //entity.ProductIsActive = model.ProductIsActive;
                entity.ProductDescription = model.ProductDescription;
                entity.ProductQuantity = model.ProductQuantity;
                entity.ProductCloseTime = model.ProductCloseTime;
                entity.MinimumSellingPrice = model.MinimumSellingPrice;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteProduct(int productId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Product
                        .Single(e => e.ProductId == productId);

                ctx.Product.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

        public string ValidateAuctionStatus(int id)
        {
            var prodDetail = GetProductById(id);
            
            if (prodDetail == null)
                return "Product has been removed or does not exist.";
            if (!prodDetail.ProductIsActive)
                return "Auction is closed";
            return "";
        }

        public string ValidateBid(BidCreate bid)
        {
            var prodDetail = GetProductById(bid.ProductId);

            if (prodDetail == null)
                return "Product has been removed or does not exist.";
            if (!prodDetail.ProductIsActive)
                return "Auction is closed";
            if (prodDetail.HighestBid > bid.BidPrice)
                return "Bid must be higher than current selling price.";
            return "";
        }

    }
}