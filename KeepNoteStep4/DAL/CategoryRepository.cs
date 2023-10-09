using System;
using System.Collections.Generic;
using System.Linq;
using Entities;

namespace DAL
{
    // Repository class is used to implement all Data access operations
    public class CategoryRepository : ICategoryRepository
    {
        private readonly KeepDbContext _dbContext;

        public CategoryRepository(KeepDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Category CreateCategory(Category category)
        {
            _dbContext.Categories.Add(category);
            _dbContext.SaveChanges();
            return category;
        }

        public bool DeleteCategory(int categoryId)
        {
            var category = _dbContext.Categories.Find(categoryId);
            if (category != null)
            {
                _dbContext.Categories.Remove(category);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public List<Category> GetAllCategoriesByUserId(int userId)
        {
            return _dbContext.Categories.Where(c => c.CategoryCreatedBy == userId).ToList();
        }

        public Category GetCategoryById(int categoryId)
        {
            return _dbContext.Categories.Find(categoryId);
        }

        public bool UpdateCategory(Category category)
        {
            var existingCategory = _dbContext.Categories.Find(category.CategoryId);
            if (existingCategory != null)
            {
                existingCategory.CategoryName = category.CategoryName;
                existingCategory.CategoryDescription = category.CategoryDescription;
                
                // Update other properties here
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
