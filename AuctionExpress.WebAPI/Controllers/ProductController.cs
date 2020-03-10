using AuctionExpress.Models;
using AuctionExpress.Service;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AuctionExpress.WebAPI.Controllers
{
    public class ProductController : ApiController
    {
        private ProductService CreateProductService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var productService = new ProductService(userId);
            return productService;
        }

        //Post Product
        /// <summary>
        /// Create a new auction.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public IHttpActionResult Post(ProductCreate product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateProductService();
            var result = new DateValidator (product.ProductStartTime, product.ProductCloseTime);
            bool validateAllProperties = false;
            var results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(result,
                new ValidationContext(result, null, null),
                results, validateAllProperties);
            if (!isValid)
            {
                string errorMessage = "";
                foreach (var item in results)
                {
                    errorMessage = item + ", ";
                }
                return BadRequest(errorMessage);
            }
            if (!service.CreateProduct(product))
                return InternalServerError();

            return Ok();
        }

        //GET Products
        /// <summary>
        /// Get a list of auction that the user has created.
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult Get()
        {
            ProductService productService = CreateProductService();
            var products = productService.GetProducts();
            return Ok(products);
        }

        //GET products by Id
        /// <summary>
        /// Get a specific auction by referencing the auction id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult Get(int id)
        {
            ProductService productService = CreateProductService();
            var product = productService.GetProductById(id);
            return Ok(product);
        }

        //EDIT POST
        /// <summary>
        /// Update an auction.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public IHttpActionResult Put(ProductEdit product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateProductService();

            if (!service.UpdateProduct(product))
                return InternalServerError();

            return Ok();
        }
    }
}
