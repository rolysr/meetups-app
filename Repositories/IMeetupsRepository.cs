using MeetupsApp.Models;

namespace MeetupsApp.Repositories;

public interface IMeetupsRepository
{
    Task<Meetup> GetMeetupAsync(Guid id);
    Task<IEnumerable<Meetup>> GetMeetupsAsync();
    Task CreateMeetupAsync(Meetup meetup);
    Task UpdateMeetupAsync(Meetup meetup);
    Task DeleteMeetupAsync(Guid id);
}