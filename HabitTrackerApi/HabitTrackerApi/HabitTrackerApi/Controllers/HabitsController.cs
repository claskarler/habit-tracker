using HabitTrackerApi.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using HabitTrackerApi.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using HabitTrackerApi.Dtos;
using System.Drawing;

namespace HabitTrackerApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class HabitsController(DataContext context, IHttpContextAccessor httpContextAccessor) : ControllerBase
{
    private readonly DataContext _context = context;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    private int GetUserId() =>
        int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

    [HttpGet]
    public async Task<ActionResult<IEnumerable<HabitDto>>> GetHabits()
    {
        var userId = GetUserId();
        var habits = await _context.Habits
            .Where(h => h.UserId == userId)
            .Include(h => h.Icon)
            .ToListAsync();

        return habits.Select(MapToDto).ToList();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<HabitDto>> GetHabit(int id)
    {
        var userId = GetUserId();
        var habit = await _context.Habits
            .Include(h => h.Icon)
            .FirstOrDefaultAsync(h => h.Id == id && h.UserId == userId);

        if (habit == null)
            return NotFound();

        return MapToDto(habit);
    }

    [HttpPost]
    public async Task<ActionResult<HabitDto>> CreateHabit(CreateHabitRequest dto)
    {
        var userId = GetUserId();

        var habit = new HabitEntity
        {
            Name = dto.Name,
            TargetCount = dto.TargetCount,
            ScheduleType = dto.ScheduleType,
            DaysOfWeekJson = dto.DaysOfWeek != null ? JsonSerializer.Serialize(dto.DaysOfWeek) : null,
            DaysOfMonthJson = dto.DaysOfMonth != null ? JsonSerializer.Serialize(dto.DaysOfMonth) : null,
            IconId = dto.IconId,
            Color = dto.Color,
            UserId = userId,
            CreatedAt = DateTime.UtcNow
        };

        _context.Habits.Add(habit);
        await _context.SaveChangesAsync();

        var createdHabit = await _context.Habits
            .Include(h => h.Icon)
            .FirstOrDefaultAsync(h => h.Id == habit.Id);

        return CreatedAtAction(nameof(GetHabit), new { id = createdHabit.Id }, MapToDto(createdHabit));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateHabit(int id, UpdateHabitRequest dto)
    {
        var userId = GetUserId();
        var habit = await _context.Habits.FirstOrDefaultAsync(h => h.Id == id && h.UserId == userId);

        if (habit == null)
            return NotFound();

        habit.Name = dto.Name;
        habit.TargetCount = dto.TargetCount;
        habit.ScheduleType = dto.ScheduleType;
        habit.DaysOfWeekJson = dto.DaysOfWeek != null ? JsonSerializer.Serialize(dto.DaysOfWeek) : null;
        habit.DaysOfMonthJson = dto.DaysOfMonth != null ? JsonSerializer.Serialize(dto.DaysOfMonth) : null;
        habit.Color = dto.Color;
        habit.IconId = dto.IconId;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteHabit(int id)
    {
        var userId = GetUserId();
        var habit = await _context.Habits.FirstOrDefaultAsync(h => h.Id == id && h.UserId == userId);

        if (habit == null)
            return NotFound();

        _context.Habits.Remove(habit);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private static HabitDto MapToDto(HabitEntity habit) => new HabitDto
    {
        Id = habit.Id,
        Name = habit.Name,
        TargetCount = habit.TargetCount,
        ScheduleType = habit.ScheduleType,
        DaysOfWeek = habit.DaysOfWeekJson != null ? JsonSerializer.Deserialize<List<string>>(habit.DaysOfWeekJson) : null,
        DaysOfMonth = habit.DaysOfMonthJson != null ? JsonSerializer.Deserialize<List<int>>(habit.DaysOfMonthJson) : null,
        Icon = new IconDto
        {
            Id = habit.Icon.Id,
            Name = habit.Icon.Name,
            CssClass = habit.Icon.CssClass
        },
        CreatedAt = habit.CreatedAt
    };
}
