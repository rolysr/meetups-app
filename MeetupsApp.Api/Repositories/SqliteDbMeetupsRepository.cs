using Microsoft.EntityFrameworkCore;
using MeetupsApp.Api.Models;

namespace MeetupsApp.Api.Repositories;

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
            var meetupsAppDbContext = scope.ServiceProvider.GetRequiredService<MeetupsAppContext>();
            return await meetupsAppDbContext.Meetups.ToListAsync();
        }
    }

    public async Task<Meetup> GetMeetupAsync(Guid id)
    {
        using (var scope = this.serviceProvider.CreateScope())
        {
            var meetupsAppDbContext = scope.ServiceProvider.GetRequiredService<MeetupsAppContext>();
            return await meetupsAppDbContext.Meetups.FirstOrDefaultAsync(meetup => meetup.MeetupId == id);
        }
    }

    public async Task CreateMeetupAsync(Meetup meetup)
    {
        using (var scope = this.serviceProvider.CreateScope())
        {
            var meetupsAppDbContext = scope.ServiceProvider.GetRequiredService<MeetupsAppContext>();
            meetupsAppDbContext.Meetups.Add(meetup);
            await meetupsAppDbContext.SaveChangesAsync();
        }  
    }

    public async Task UpdateMeetupAsync(Meetup meetup)
    {
        using (var scope = this.serviceProvider.CreateScope())
        {
            var meetupsAppDbContext = scope.ServiceProvider.GetRequiredService<MeetupsAppContext>();
            var oldMeetup = await meetupsAppDbContext.Meetups.FirstOrDefaultAsync(oldMeetup => meetup.MeetupId == oldMeetup.MeetupId);
            oldMeetup.Title = meetup.Title;
            oldMeetup.Address = meetup.Address;
            oldMeetup.Description = meetup.Description;
            oldMeetup.Image = meetup.Image;
            oldMeetup.IsFavorite = meetup.IsFavorite;

            await meetupsAppDbContext.SaveChangesAsync();
        } 
    }

    public async Task DeleteMeetupAsync(Guid id)
    {
        using (var scope = this.serviceProvider.CreateScope())
        {
            var meetupsAppDbContext = scope.ServiceProvider.GetRequiredService<MeetupsAppContext>();
            meetupsAppDbContext.Meetups.Remove(await meetupsAppDbContext.Meetups.FirstOrDefaultAsync(meetup => meetup.MeetupId == id));
            await meetupsAppDbContext.SaveChangesAsync();
        }   
    }
}