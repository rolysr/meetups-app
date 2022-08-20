namespace MeetupsApp.Models;

public record Meetup
{
    public Guid MeetupId { get; set; }
    public string ?Title { get; set; }
    public string ?Description { get; set; }
    public string ?Address { get; set; }
    public string ?Image { get; set; } //Image Url
    public bool IsFavorite { get; set; }
}