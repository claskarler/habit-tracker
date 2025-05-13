namespace HabitTrackerApi.Dtos;

public class HabitEntryWithHabitDto
{
    public int Id { get; set; }
    public int HabitId { get; set; }
    public string? HabitName { get; set; }
    public DateTime Date { get; set; }
    public int ActualCount { get; set; }
}

