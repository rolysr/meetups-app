using Microsoft.EntityFrameworkCore;
using MeetupsApp.Models;

namespace MeetupsApp.Repositories;

public class SqliteDbMeetupsRepository : IMeetupsRepository
{
    private readonly IServiceProvider serviceProvider;

    public SqliteDbMeetupsRepository(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    public async Task<IEnumerable<Meetup>> GetMeetupsAsync()
    {
        using (var scope = this.serviceProvider.CreateScope())
        {
            var iMuseumDbContext = scope.ServiceProvider.GetRequiredService<MeetupsAppContext>();
            return await iMuseumDbContext.Meetups.ToListAsync();
        }
    }

    public async Task<Meetup> GetMeetupAsync(Guid id)
    {
        using (var scope = this.serviceProvider.CreateScope())
        {
            var iMuseumDbContext = scope.ServiceProvider.GetRequiredService<MeetupsAppContext>();
            return await iMuseumDbContext.Meetups.FirstOrDefaultAsync(meetup => meetup.MeetupId == id);
        }
    }

    public async Task CreateMeetupAsync(Meetup meetup)
    {
        using (var scope = this.serviceProvider.CreateScope())
        {
            var iMuseumDbContext = scope.ServiceProvider.GetRequiredService<MeetupsAppContext>();
            iMuseumDbContext.Meetups.Add(meetup);
            await iMuseumDbContext.SaveChangesAsync();
        }  
    }

    public async Task UpdateMeetupAsync(Meetup meetup)
    {
        using (var scope = this.serviceProvider.CreateScope())
        {
            var iMuseumDbContext = scope.ServiceProvider.GetRequiredService<MeetupsAppContext>();
            var oldMeetup = await iMuseumDbContext.Meetups.FirstOrDefaultAsync(oldMeetup => meetup.MeetupId == oldMeetup.MeetupId);
            oldMeetup.Title = meetup.Title;
            oldMeetup.Address = meetup.Address;
            oldMeetup.Description = meetup.Description;
            oldMeetup.Image = meetup.Image;
            oldMeetup.IsFavorite = meetup.IsFavorite;

            await iMuseumDbContext.SaveChangesAsync();
        } 
    }

    public async Task DeleteMeetupAsync(Guid id)
    {
        using (var scope = this.serviceProvider.CreateScope())
        {
            var iMuseumDbContext = scope.ServiceProvider.GetRequiredService<MeetupsAppContext>();
            iMuseumDbContext.Meetups.Remove(await iMuseumDbContext.Meetups.FirstOrDefaultAsync(meetup => meetup.MeetupId == id));
            await iMuseumDbContext.SaveChangesAsync();
        }   
    }
}