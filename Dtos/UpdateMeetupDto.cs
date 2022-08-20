using System.ComponentModel.DataAnnotations;

namespace MeetupsApp.Dtos;

public record UpdateMeetupDto
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