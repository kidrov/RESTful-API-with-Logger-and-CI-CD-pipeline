using System;
using System.Collections.Generic;
using System.Text;
using DAL;
using Entities;
using Exceptions;

namespace Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public Category CreateCategory(Category category)
        {
            // Business logic and validation (if needed)
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category), "Category cannot be null.");
            }

            // Call the repository to create the category
            return _categoryRepository.CreateCategory(category);
        }

        public bool DeleteCategory(int categoryId)
        {
            // Check if the category exists
            var existingCategory = _categoryRepository.GetCategoryById(categoryId);
            if (existingCategory == null)
            {
                throw new CategoryNotFoundException($"Category with ID {categoryId} not found.");
            }

            // Call the repository to delete the category
            return _categoryRepository.DeleteCategory(categoryId);
        }

        public List<Category> GetAllCategoriesByUserId(int userId)
        {
            // Call the repository to get categories by user ID
            return _categoryRepository.GetAllCategoriesByUserId(userId);
        }

        public Category GetCategoryById(int categoryId)
        {
            // Call the repository to get the category by ID
            return _categoryRepository.GetCategoryById(categoryId);
        }

        public bool UpdateCategory(int categoryId, Category category)
        {
            // Check if the category exists
            var existingCategory = _categoryRepository.GetCategoryById(categoryId);
            if (existingCategory == null)
            {
                throw new CategoryNotFoundException($"Category with ID {categoryId} not found.");
            }

            // Update category properties
            existingCategory.CategoryName = category.CategoryName;
            existingCategory.CategoryDescription = category.CategoryDescription;

            // Call the repository to update the category
            return _categoryRepository.UpdateCategory(existingCategory);
        }
    }
}

