using Microsoft.AspNetCore.Mvc;
using Siemens.Internship2026.GradeBook.Interfaces;

namespace Siemens.Internship2026.GradeBook.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemController : ControllerBase
{
    private readonly IItemRepository _repository;
    private readonly ILogger<ItemController> _logger;
    private readonly IGradeService _gradeService; 

    public ItemController(IItemRepository repository, ILogger<ItemController> logger, IGradeService gradeService)
    {
        _repository = repository;
        _logger = logger;
        _gradeService = gradeService; 
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("GET api/item called");
        var items = await _repository.GetAllAsync();
        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        _logger.LogInformation("GET api/item/{Id} called", id);
        if (id <= 0)
        {
            _logger.LogWarning("Invalid id: {Id}", id);
            return BadRequest("Id must be a positive integer.");
        }
        var item = await _repository.GetByIdAsync(id);
        if (item == null)
        {
            _logger.LogWarning("Item {Id} not found", id);
            return NotFound($"Item with Id {id} was not found.");
        }
        return Ok(item);
    }

    [HttpGet("passing/{n}")]
    public async Task<IActionResult> GetPassingGrades(int n)
    {
        _logger.LogInformation("GET api/item/passing/{N} called", n);

        if (n <= 0)
        {
            return BadRequest("N must be a positive integer.");
        }

        var grades = await _gradeService.GetFirstNPassingGradesAsync(n);
        return Ok(grades);
    }
}