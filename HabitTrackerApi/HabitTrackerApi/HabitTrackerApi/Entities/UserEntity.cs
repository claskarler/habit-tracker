using System.ComponentModel.DataAnnotations;

namespace HabitTrackerApi.Entities;

public class UserEntity
{
    [Key]
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<HabitEntity> Habits { get; set; } = [];
}