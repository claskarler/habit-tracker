namespace HabitTrackerApi.Dtos;

public class CreateHabitRequest
{
    public string Name { get; set; } = null!;
    public int TargetCount { get; set; }
    public string ScheduleType { get; set; } = null!;
    public List<string>? DaysOfWeek { get; set; }
    public List<int>? DaysOfMonth { get; set; }
    public string Color { get; set; } = null!;
    public int IconId { get; set; }
}
