using System.ComponentModel.DataAnnotations;

namespace MeetupsApp.Api.Dtos;

public record CreateMeetupDto
{
    [Required]
    public string ?Title { get; set; }
    [Required]
    public string ?Description { get; set; }
    [Required]
    public string ?Address { get; set; }
    [Required]
    public string ?Image { get; set; } //Image Url
}