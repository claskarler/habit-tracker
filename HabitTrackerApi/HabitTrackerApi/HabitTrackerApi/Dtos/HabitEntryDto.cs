namespace HabitTrackerApi.Dtos;

public class HabitEntryDto
{
    public int Id { get; set; }
    public int HabitId { get; set; }
    public DateTime Date { get; set; }
    public int ActualCount { get; set; }
}
