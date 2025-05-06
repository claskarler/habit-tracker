using System.ComponentModel.DataAnnotations;

namespace HabitTrackerApi.Entities;

public class IconEntity
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string CssClass { get; set; } = null!;

    public ICollection<HabitEntity> Habits { get; set; } = [];
}