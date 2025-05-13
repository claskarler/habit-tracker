using HabitTrackerApi.Contexts;
using HabitTrackerApi.Dtos;
using HabitTrackerApi.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HabitTrackerApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class HabitEntriesController(DataContext context, IHttpContextAccessor httpContextAccessor) : ControllerBase
{
    private readonly DataContext _context = context;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    private int GetUserId() =>
        int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

    [HttpPost]
    public async Task<IActionResult> LogEntry(CreateHabitEntryRequest dto)
    {
        var userId = GetUserId();

        var habit = await _context.Habits
            .FirstOrDefaultAsync(h => h.Id == dto.HabitId && h.UserId == userId);

        if (habit == null)
            return NotFound("Habit not found or unauthorized.");

        var existingEntry = await _context.HabitEntries
            .FirstOrDefaultAsync(e => e.HabitId == dto.HabitId && e.Date.Date == dto.Date.Date);

        if (existingEntry != null)
        {
            existingEntry.ActualCount = dto.ActualCount;
        }
        else
        {
            var entry = new HabitEntryEntity
            {
                HabitId = dto.HabitId,
                Date = dto.Date.Date,
                ActualCount = dto.ActualCount
            };

            _context.HabitEntries.Add(entry);
        }

        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<HabitEntryWithHabitDto>>> GetAllEntries()
    {
        var userId = GetUserId();

        var entries = await _context.HabitEntries
            .Include(e => e.Habit)
            .Where(e => e.Habit.UserId == userId)
            .OrderByDescending(e => e.Date)
            .ToListAsync();

        var result = entries.Select(e => new HabitEntryWithHabitDto
        {
            Id = e.Id,
            HabitId = e.HabitId,
            HabitName = e.Habit.Name,
            Date = e.Date,
            ActualCount = e.ActualCount
        }).ToList();

        return result;
    }

    [HttpGet("{habitId}")]
    public async Task<ActionResult<IEnumerable<HabitEntryDto>>> GetEntriesForHabit(int habitId)
    {
        var userId = GetUserId();

        var habit = await _context.Habits
            .FirstOrDefaultAsync(h => h.Id == habitId && h.UserId == userId);

        if (habit == null)
            return NotFound("Habit not found or unauthorized.");

        var entries = await _context.HabitEntries
            .Where(e => e.HabitId == habitId)
            .OrderBy(e => e.Date)
            .ToListAsync();

        return entries.Select(e => new HabitEntryDto
        {
            Id = e.Id,
            Date = e.Date,
            ActualCount = e.ActualCount
        }).ToList();
    }

    [HttpDelete("{entryId}")]
    public async Task<IActionResult> DeleteEntry(int entryId)
    {
        var userId = GetUserId();

        var entry = await _context.HabitEntries
            .Include(e => e.Habit)
            .FirstOrDefaultAsync(e => e.Id == entryId && e.Habit.UserId == userId);

        if (entry == null)
            return NotFound("Habit entry not found or unauthorized.");

        _context.HabitEntries.Remove(entry);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
