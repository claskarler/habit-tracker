using HabitTrackerApi.Entities;
using Microsoft.EntityFrameworkCore;
namespace HabitTrackerApi.Contexts;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<HabitEntity> Habits { get; set; }
    public DbSet<HabitEntryEntity> HabitEntries { get; set; }
    public DbSet<IconEntity> Icons { get; set; }

}
