using System.Collections.Generic;
using MeetupsApp.Api.Models;

namespace MeetupsApp.Api.Repositories;

public interface IMeetupsRepository
{
    Task<Meetup> GetMeetupAsync(Guid id);
    Task<IEnumerable<Meetup>> GetMeetupsAsync();
    Task CreateMeetupAsync(Meetup meetup);
    Task UpdateMeetupAsync(Meetup meetup);
    Task DeleteMeetupAsync(Guid id);
}