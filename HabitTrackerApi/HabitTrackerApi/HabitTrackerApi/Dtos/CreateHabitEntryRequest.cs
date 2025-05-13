namespace HabitTrackerApi.Dtos;

public class CreateHabitEntryRequest
{
    public int HabitId { get; set; }
    public DateTime Date { get; set; }
    public int ActualCount { get; set; }
}
