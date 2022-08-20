using Microsoft.EntityFrameworkCore;

namespace MeetupsApp.Models;

public class MeetupsAppContext : DbContext
{
    public DbSet<Meetup> ?Meetups { get; set; }

    public string DbPath { get; }

    public MeetupsAppContext(DbContextOptions<MeetupsAppContext> options) : base (options)
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join("", "meetups.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {} 

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}