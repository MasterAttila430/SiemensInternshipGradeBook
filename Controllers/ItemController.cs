using Microsoft.AspNetCore.Mvc;
using Siemens.Internship2026.GradeBook.Interfaces;

namespace Siemens.Internship2026.GradeBook.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemController : ControllerBase
{
    private readonly IItemRepository _repository;
    // Added logger
    private readonly ILogger<ItemController> _logger;

    public ItemController(IItemRepository reader, ILogger<ItemController> logger)
    {
        _repository = reader;
        _logger = logger;
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
}