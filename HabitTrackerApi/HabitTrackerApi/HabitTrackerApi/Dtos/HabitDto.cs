namespace HabitTrackerApi.Dtos;

public class HabitDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int TargetCount { get; set; }
    public string ScheduleType { get; set; } = string.Empty;
    public List<string>? DaysOfWeek { get; set; }
    public List<int>? DaysOfMonth { get; set; }
    public IconDto Icon { get; set; } = new();
    public DateTime CreatedAt { get; set; }
}
