using HabitTrackerApi.Contexts;
using HabitTrackerApi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HabitTrackerApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IconsController(DataContext db) : ControllerBase
{
    private readonly DataContext _db = db;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<IconEntity>>> GetAll()
    {
        return Ok(await _db.Icons.ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IconEntity>> GetOne(int id)
    {
        var icon = await _db.Icons.FindAsync(id);
        if (icon == null) return NotFound();
        return Ok(icon);
    }
}

