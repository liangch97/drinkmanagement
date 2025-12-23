using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DrinkManagement.Data;
using DrinkManagement.Models;
using DrinkManagement.DTOs;

namespace DrinkManagement.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DrinksController : ControllerBase
{
    private readonly DrinkDbContext _context;
    private readonly ILogger<DrinksController> _logger;

    public DrinksController(DrinkDbContext context, ILogger<DrinksController> logger)
    {
        _context = context;
        _logger = logger;
    }

    // GET: api/drinks
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DrinkDto>>> GetDrinks()
    {
        var drinks = await _context.Drinks
            .Include(d => d.Category)
            .Select(d => new DrinkDto
            {
                Id = d.Id,
                Name = d.Name,
                Description = d.Description,
                Price = d.Price,
                CategoryId = d.CategoryId,
                CategoryName = d.Category.Name,
                Stock = d.Stock,
                ImageUrl = d.ImageUrl,
                CreatedAt = d.CreatedAt,
                UpdatedAt = d.UpdatedAt
            })
            .ToListAsync();

        return Ok(drinks);
    }

    // GET: api/drinks/5
    [HttpGet("{id}")]
    public async Task<ActionResult<DrinkDto>> GetDrink(int id)
    {
        var drink = await _context.Drinks
            .Include(d => d.Category)
            .Where(d => d.Id == id)
            .Select(d => new DrinkDto
            {
                Id = d.Id,
                Name = d.Name,
                Description = d.Description,
                Price = d.Price,
                CategoryId = d.CategoryId,
                CategoryName = d.Category.Name,
                Stock = d.Stock,
                ImageUrl = d.ImageUrl,
                CreatedAt = d.CreatedAt,
                UpdatedAt = d.UpdatedAt
            })
            .FirstOrDefaultAsync();

        if (drink == null)
        {
            return NotFound(new { message = "饮品未找到" });
        }

        return Ok(drink);
    }

    // POST: api/drinks
    [HttpPost]
    public async Task<ActionResult<DrinkDto>> CreateDrink(CreateDrinkDto createDto)
    {
        // Check if category exists
        var categoryExists = await _context.Categories.AnyAsync(c => c.Id == createDto.CategoryId);
        if (!categoryExists)
        {
            return BadRequest(new { message = "分类不存在" });
        }

        var drink = new Drink
        {
            Name = createDto.Name,
            Description = createDto.Description,
            Price = createDto.Price,
            CategoryId = createDto.CategoryId,
            Stock = createDto.Stock,
            ImageUrl = createDto.ImageUrl,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.Drinks.Add(drink);
        await _context.SaveChangesAsync();

        var drinkDto = await _context.Drinks
            .Include(d => d.Category)
            .Where(d => d.Id == drink.Id)
            .Select(d => new DrinkDto
            {
                Id = d.Id,
                Name = d.Name,
                Description = d.Description,
                Price = d.Price,
                CategoryId = d.CategoryId,
                CategoryName = d.Category.Name,
                Stock = d.Stock,
                ImageUrl = d.ImageUrl,
                CreatedAt = d.CreatedAt,
                UpdatedAt = d.UpdatedAt
            })
            .FirstAsync();

        return CreatedAtAction(nameof(GetDrink), new { id = drink.Id }, drinkDto);
    }

    // PUT: api/drinks/5
    [HttpPut("{id}")]
    public async Task<ActionResult<DrinkDto>> UpdateDrink(int id, UpdateDrinkDto updateDto)
    {
        var drink = await _context.Drinks.FindAsync(id);
        if (drink == null)
        {
            return NotFound(new { message = "饮品未找到" });
        }

        // Check if new category exists
        if (updateDto.CategoryId.HasValue)
        {
            var categoryExists = await _context.Categories.AnyAsync(c => c.Id == updateDto.CategoryId.Value);
            if (!categoryExists)
            {
                return BadRequest(new { message = "分类不存在" });
            }
            drink.CategoryId = updateDto.CategoryId.Value;
        }

        if (updateDto.Name != null) drink.Name = updateDto.Name;
        if (updateDto.Description != null) drink.Description = updateDto.Description;
        if (updateDto.Price.HasValue) drink.Price = updateDto.Price.Value;
        if (updateDto.Stock.HasValue) drink.Stock = updateDto.Stock.Value;
        if (updateDto.ImageUrl != null) drink.ImageUrl = updateDto.ImageUrl;
        
        drink.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        var drinkDto = await _context.Drinks
            .Include(d => d.Category)
            .Where(d => d.Id == drink.Id)
            .Select(d => new DrinkDto
            {
                Id = d.Id,
                Name = d.Name,
                Description = d.Description,
                Price = d.Price,
                CategoryId = d.CategoryId,
                CategoryName = d.Category.Name,
                Stock = d.Stock,
                ImageUrl = d.ImageUrl,
                CreatedAt = d.CreatedAt,
                UpdatedAt = d.UpdatedAt
            })
            .FirstAsync();

        return Ok(drinkDto);
    }

    // DELETE: api/drinks/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDrink(int id)
    {
        var drink = await _context.Drinks.FindAsync(id);
        if (drink == null)
        {
            return NotFound(new { message = "饮品未找到" });
        }

        _context.Drinks.Remove(drink);
        await _context.SaveChangesAsync();

        return Ok(new { message = "饮品已删除" });
    }

    // GET: api/drinks/category/5
    [HttpGet("category/{categoryId}")]
    public async Task<ActionResult<IEnumerable<DrinkDto>>> GetDrinksByCategory(int categoryId)
    {
        var drinks = await _context.Drinks
            .Include(d => d.Category)
            .Where(d => d.CategoryId == categoryId)
            .Select(d => new DrinkDto
            {
                Id = d.Id,
                Name = d.Name,
                Description = d.Description,
                Price = d.Price,
                CategoryId = d.CategoryId,
                CategoryName = d.Category.Name,
                Stock = d.Stock,
                ImageUrl = d.ImageUrl,
                CreatedAt = d.CreatedAt,
                UpdatedAt = d.UpdatedAt
            })
            .ToListAsync();

        return Ok(drinks);
    }
}
