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
<<<<<<< HEAD:AuctionExpress.WebAPI/Controllers/ProductController.cs
            var userId = Guid.Parse("137ae0c4-7144-445d-b6c0-2918a3dd5907");
            //User.Identity.GetUserId());
=======
            Guid userId = new Guid();
            if (!User.Identity.IsAuthenticated)
            { userId = Guid.Parse("00000000-0000-0000-0000-000000000000"); }
            else
            { userId = Guid.Parse(User.Identity.GetUserId()); }

>>>>>>> d97ee118e3b517dd7714c640f9c3ada5db551b9d:AuctionExpress.WebAPI/Controllers/ApiControllers/ProductController.cs
            var productService = new ProductService(userId);
            return productService;
        }

        //Post Product
        /// <summary>
        /// Create a new auction.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [Authorize]
        public IHttpActionResult Post(ProductCreate product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (User == null)
                return BadRequest("Must be a registered user.");
            var service = CreateProductService();
            var result = new DateValidator(product.ProductStartTime, product.ProductCloseTime);
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

            return Ok("Product successfully created.");
        }

        //GET Products
        /// <summary>
        /// Get a list of auction that the user has created.
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public IHttpActionResult Get()
        {
            ProductService productService = CreateProductService();
            var products = productService.GetProducts();
            return Ok(products);
        }
        /// <summary>
        /// Get a list of all auctions that are open for bidding.
        /// </summary>
        /// <returns></returns>
        [Route("api/Product/GetOpenAuctions")]
        [AllowAnonymous]
        public IHttpActionResult GetOpenProducts()
        {
            ProductService productService = CreateProductService();
            var products = productService.GetOpenProducts();
            return Ok(products);
        }
        /// <summary>
        /// Get a list of all auctions that are open for bidding under a specific category.
        /// </summary>
        /// <returns></returns>
        [Route("api/Product/GetAuctionsByCat")]
        [AllowAnonymous]
        public IHttpActionResult GetOpenProdByCategory(int Id)
        {
            ProductService productService = CreateProductService();
            var products = productService.GetOpenProdByCategory(Id);
            return Ok(products);
        }

        //GET products by Id
        /// <summary>
        /// Get a specific auction by referencing the auction id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAnonymous]
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
        [Authorize]
        public IHttpActionResult Put(ProductEdit product)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateProductService();
            var prodDetail = service.ValidateAuctionStatus(product.ProductId);
            if (prodDetail == "")
            {

                var result = new DateValidator(product.ProductCloseTime);
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

                if (!service.UpdateProduct(product))
                    return InternalServerError();

                return Ok("Product succesfully updated.");
            }
            return BadRequest(prodDetail);
        }
        /// <summary>
        /// Delete a product using the product id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
       //  [Authorize]
       // [Route("Product/Delete")]

        public IHttpActionResult Delete(int id)
        {
            var service = CreateProductService();

            if (!service.DeleteProduct(id))
                return InternalServerError();

            return Ok();
        }
    }
}
