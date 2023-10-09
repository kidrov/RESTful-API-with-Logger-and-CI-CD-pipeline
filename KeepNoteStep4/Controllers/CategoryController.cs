using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service;
using log4net;
using System;

namespace KeepNote.Controllers
{
    [ApiController]
    [Route("api/category")]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private static readonly ILog _logger = LogManager.GetLogger(typeof(CategoryController));

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        public IActionResult CreateCategory(Category category)
        {
            try
            {
                _logger.Info("Creating a new category");

                var createdCategory = _categoryService.CreateCategory(category);
                return Created("", createdCategory); // 201 Created
            }
            catch (Exception ex)
            {
                _logger.Error($"An error occurred during category creation: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpDelete("{categoryId}")]
        public IActionResult DeleteCategory(int categoryId)
        {
            try
            {
                _logger.Info($"Deleting category with ID: {categoryId}");

                var result = _categoryService.DeleteCategory(categoryId);
                if (result)
                {
                    _logger.Info("Category deleted successfully.");
                    return Ok(); // 200 OK
                }
                else
                {
                    _logger.Warn("Category deletion failed due to not found.");
                    return NotFound(); // 404 Not Found
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"An error occurred during category deletion: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPut("{categoryId}")]
        public IActionResult UpdateCategory(int categoryId, Category category)
        {
            try
            {
                _logger.Info($"Updating category with ID: {categoryId}");

                var result = _categoryService.UpdateCategory(categoryId, category);
                if (result)
                {
                    _logger.Info("Category updated successfully.");
                    return Ok(); // 200 OK
                }
                else
                {
                    _logger.Warn("Category update failed due to not found.");
                    return NotFound(); // 404 Not Found
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"An error occurred during category update: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet("{userId}")]
        public IActionResult GetCategoriesByUserId(int userId)
        {
            try
            {
                _logger.Info($"Getting categories for user with ID: {userId}");

                var categories = _categoryService.GetAllCategoriesByUserId(userId);
                return Ok(categories); // 200 OK
            }
            catch (Exception ex)
            {
                _logger.Error($"An error occurred while getting categories: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
