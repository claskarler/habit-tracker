using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HabitTrackerApi.Entities;

public class HabitEntity
{
    [Key]
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Name { get; set; } = null!;
    public int TargetCount { get; set; }
    public string ScheduleType { get; set; } = null!;

    [Column(TypeName = "nvarchar(max)")]
    public string? DaysOfWeekJson { get; set; }

    [Column(TypeName = "nvarchar(max)")]
    public string? DaysOfMonthJson { get; set; }
    public string Color { get; set; } = "blue";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public int IconId { get; set; }

    public UserEntity User { get; set; } = null!;
    public IconEntity Icon { get; set; } = null!;
    public ICollection<HabitEntryEntity> HabitEntries { get; set; } = [];
}