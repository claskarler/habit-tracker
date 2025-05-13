namespace HabitTrackerApi.Dtos;

public class UpdateHabitRequest
{
    public string Name { get; set; } = string.Empty;
    public int TargetCount { get; set; }
    public string ScheduleType { get; set; } = string.Empty;
    public List<string>? DaysOfWeek { get; set; }
    public List<int>? DaysOfMonth { get; set; }
    public string Color { get; set; } = "blue";
    public int IconId { get; set; }
}
