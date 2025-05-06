using System.ComponentModel.DataAnnotations;

namespace HabitTrackerApi.Entities;

public class HabitEntryEntity
{
    [Key]
    public int Id { get; set; }
    public int HabitId { get; set; }
    public DateTime Date { get; set; }
    public int ActualCount { get; set; }

    public HabitEntity Habit { get; set; } = null!;
}
