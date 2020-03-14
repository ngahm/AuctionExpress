﻿using AuctionExpress.Models;
using AuctionExpress.Service;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AuctionExpress.WebAPI.Controllers
{
    public class CategoryController : ApiController
    {
        private CategoryService CreateCategoryService()
        {
            var userId = Guid.Parse("1ae9afff-3752-45c4-a551-dc17f56033d8");//User.Identity.GetUserId());
            var categoryService = new CategoryService(userId);
            return categoryService;
        }





        //GET ALL
        /// <summary>
        /// Get a list of all available categories.
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult Get()
        {
            CategoryService categoryService = CreateCategoryService();
            var category = categoryService.GetCategory();
            return Ok(category);
        }

        //POST
        /// <summary>
        /// Create a new category.
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public IHttpActionResult Post(CategoryCreate category)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateCategoryService();

            if (!service.CreateCategory(category))
                return InternalServerError();

            return Ok("Category successfully added.");
        }

        //GET BY ID
        /// <summary>
        /// Get a specific category by refencing the category id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult Get(int id)
        {
            CategoryService categoryService = CreateCategoryService();
            var category = categoryService.GetCategoryById(id);
            if (category == null)
                return BadRequest("Category does not exist.");
            return Ok(category);
        }

        
        //PUT
        /// <summary>
        /// Update a category.
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public IHttpActionResult Put(CategoryEdit category)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateCategoryService();

            if (!service.UpdateCategory(category))
                return InternalServerError();

            return Ok("Category successfully updated.");
        }
        /// <summary>
        /// Delete a Category using the Category Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult Delete(int id)
        {
            var service = CreateCategoryService();

            if (!service.DeleteCategory(id))
                return InternalServerError();

            return Ok();
        }
    }
}