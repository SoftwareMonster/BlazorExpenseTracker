using System;
using System.Reflection;
using System.Threading.Tasks;
using BlazorExpenseTracker.Data.Repositories;
using BlazorExpenseTracker.Model;
using Microsoft.AspNetCore.Mvc;

namespace BlazorExpenseTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly Serilogger.Serilogger _seriLogger;

        public CategoryController(ICategoryRepository categoryRepository,Serilogger.Serilogger seriLogger)
        {
            var currentMethod = MethodBase.GetCurrentMethod().Name;
            try
            {
                _categoryRepository = categoryRepository;
                _seriLogger = seriLogger;
            }
            catch (Exception ex)
            {
                _seriLogger.GetInformattion(ex, ex.Message, currentMethod, "Gorka");
            }
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var currentMethod = MethodBase.GetCurrentMethod().Name;
            try
            {
                return Ok(await _categoryRepository.GetAllCategories());
            }
            catch (Exception ex)
            {
                _seriLogger.GetInformattion(ex, ex.Message, currentMethod, "Gorka");
                return null;
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryDetails(int id)
        {
            var currentMethod = MethodBase.GetCurrentMethod().Name;
            try
            {
                return Ok(await _categoryRepository.GetCategoryDetails(id));
            }
            catch (Exception ex)
            {
                _seriLogger.GetInformattion(ex, ex.Message, currentMethod, "Gorka");
                return null;
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] Category category)
        {
            var currentMethod = MethodBase.GetCurrentMethod().Name;
            try
            {
                if (category == null) return BadRequest();
                if (category.Name.Trim() == string.Empty) ModelState.AddModelError("Name", "Category Name shouldn't be empty");
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var created = await _categoryRepository.InsertCategory(category);
                return Created("created", created);
            }
            catch (Exception ex)
            {
                _seriLogger.GetInformattion(ex, ex.Message, currentMethod, "Gorka");
                return null;
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory([FromBody] Category category)
        {
            var currentMethod = MethodBase.GetCurrentMethod().Name;
            try
            {
                if (category == null) return BadRequest();
                if (category.Name.Trim() == string.Empty) ModelState.AddModelError("Name", "Category Name shouldn't be empty");
                if (!ModelState.IsValid) return BadRequest(ModelState);
                await _categoryRepository.UpdateCategory(category);
                return NoContent(); //success
            }
            catch (Exception ex)
            {
                _seriLogger.GetInformattion(ex, ex.Message, currentMethod, "Gorka");
                return null;
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var currentMethod = MethodBase.GetCurrentMethod().Name;
            try
            {
                if (id == 0) return BadRequest();
                await _categoryRepository.DeleteCategory(id);
                return NoContent(); //success
            }
            catch (Exception ex)
            {
                _seriLogger.GetInformattion(ex, ex.Message, currentMethod, "Gorka");
                return null;
            }
        }
    }
}
