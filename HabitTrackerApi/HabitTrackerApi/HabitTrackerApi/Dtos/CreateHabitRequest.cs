namespace HabitTrackerApi.Dtos;

public class CreateHabitRequest
{
    public string Name { get; set; } = null!;
    public int TargetCount { get; set; }
    public string ScheduleType { get; set; } = null!;
    public List<string>? DaysOfWeek { get; set; }
    public List<int>? DaysOfMonth { get; set; }
    public int IconId { get; set; }
}
