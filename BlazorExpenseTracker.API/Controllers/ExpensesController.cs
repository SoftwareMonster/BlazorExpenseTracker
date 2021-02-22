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
    public class ExpensesController : Controller
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly Serilogger.Serilogger _seriLogger;

        public ExpensesController(IExpenseRepository expenseRepository, Serilogger.Serilogger seriLogger)
        {
            var currentMethod = MethodBase.GetCurrentMethod().Name;
            try
            {
                _expenseRepository = expenseRepository;
                _seriLogger = seriLogger;
            }
            catch (Exception ex)
            {
                _seriLogger.GetInformattion(ex, ex.Message, currentMethod, "Gorka");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllExpenses()
        {
            var currentMethod = MethodBase.GetCurrentMethod().Name;
            try
            {
                return Ok(await _expenseRepository.GetAllExpenses());
            }
            catch (Exception ex)
            {
                _seriLogger.GetInformattion(ex, ex.Message, currentMethod, "Gorka");
                return null;
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetExpenseDetails(int id)
        {
            var currentMethod = MethodBase.GetCurrentMethod().Name;
            try
            {
                return Ok(await _expenseRepository.GetExpenseDetails(id));
            }
            catch (Exception ex)
            {
                _seriLogger.GetInformattion(ex, ex.Message, currentMethod, "Gorka");
                return null;
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateExpenseDetails([FromBody] Expense expense)
        {
            var currentMethod = MethodBase.GetCurrentMethod().Name;
            try
            {
                if (expense == null) return BadRequest();
                if (expense.Amount < 0) ModelState.AddModelError("Name", "Amount shouldnt be empty");
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var created = await _expenseRepository.InsertExpenseDetails(expense);
                return Created("created", created);
            }
            catch (Exception ex)
            {
                _seriLogger.GetInformattion(ex, ex.Message, currentMethod, "Gorka");
                return null;
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateExpenseDetails([FromBody] Expense expense)
        {
            var currentMethod = MethodBase.GetCurrentMethod().Name;
            try
            {
                if (expense == null) return BadRequest();
                if (expense.Amount < 0) ModelState.AddModelError("Name", "Amount shouldnt be empty");
                if (!ModelState.IsValid) return BadRequest(ModelState);
                await _expenseRepository.UpdateExpenseDetails(expense);
                return NoContent(); //success
            }
            catch (Exception ex)
            {
                _seriLogger.GetInformattion(ex, ex.Message, currentMethod, "Gorka");
                return null;
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpenseDetails(int id)
        {
            var currentMethod = MethodBase.GetCurrentMethod().Name;
            try
            {
                if (id == 0) return BadRequest();
                await _expenseRepository.DeleteExpenseDetails(id);
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