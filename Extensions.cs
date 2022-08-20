using MeetupsApp.Models;
using MeetupsApp.Dtos;

namespace MeetupsApp;

public static class Extensions
{
    public static MeetupDto AsDto(this Meetup meetup)
    {
         return new MeetupDto{ 
            MeetupId = meetup.MeetupId,
            Title = meetup.Title,
            Description = meetup.Description,
            Address = meetup.Address,
            Image = meetup.Image,
        };
    }
}