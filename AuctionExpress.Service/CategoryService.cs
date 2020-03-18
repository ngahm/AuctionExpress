using AuctionExpress.Data;
using AuctionExpress.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionExpress.Service
{
    public class CategoryService
    {
        private readonly Guid _userId;

        public CategoryService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateCategory(CategoryCreate model)
        {
            var entity = new Category()
            {
                CategoryName = model.CategoryName,
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Category.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<CategoryListItem> GetCategory()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Category
                        .Select(
                            e =>
                                new CategoryListItem
                                {
                                    CategoryId = e.CategoryId,
                                    CategoryName = e.CategoryName,
                                }
                        );
                return query.ToArray();
            }
        }

        public CategoryDetail GetCategoryById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                     ctx
                    .Category
                    .Where(e => e.CategoryId == id)
                    .FirstOrDefault();
                return
                    new CategoryDetail
                    {
                        CategoryId = entity.CategoryId,
                        CategoryName = entity.CategoryName,
                    };
            }
        }

        public bool UpdateCategory(CategoryEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Category
                        .Single(e => e.CategoryId == model.CategoryId);

                entity.CategoryId = model.CategoryId;
                entity.CategoryName = model.CategoryName;

                return ctx.SaveChanges() == 1;
            }
        }

        public string DeleteCategory(int categoryId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Category
                        .Single(e => e.CategoryId == categoryId);

                try
                {
                    ctx.Category.Remove(entity);
                    ctx.SaveChanges();
                    return "Category successfully deleted";
                }
                catch (Exception e)
                {
                    return e.Message;
                }
            }
        }
    }
}

