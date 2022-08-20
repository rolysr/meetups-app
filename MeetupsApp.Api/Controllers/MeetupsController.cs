using Microsoft.AspNetCore.Mvc;
using MeetupsApp.Api.Models;
using MeetupsApp.Api.Repositories;
using MeetupsApp.Api.Dtos;

namespace MeetupsApp.Api.Controllers;

//meetups
[ApiController]
[Route("meetups")]
public class MeetupsController : ControllerBase
{
    private readonly IMeetupsRepository repository;
    private readonly ILogger<MeetupsController> logger;

    public MeetupsController(IMeetupsRepository repository, ILogger<MeetupsController> logger)
    {
        this.repository = repository;
        this.logger = logger;
    }

    //GET /meetups
    [HttpGet]
    public async Task<IEnumerable<MeetupDto>> GetMeetupsAsync()
    {
        var meetups = (await repository.GetMeetupsAsync())
                        .Select(meetup => meetup.AsDto());
        
        logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss")}: Retrieved {meetups.Count()} items");

        return meetups;
    }

    //GET /meetups/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<MeetupDto>> GetMeetupAsync(Guid id)
    {
        var meetup = await repository.GetMeetupAsync(id);

        if(meetup is null)
            return NotFound();

        return meetup.AsDto();
    }

    //POST /meetups
    [HttpPost]
    public async Task<ActionResult<MeetupDto>> CreateMeetupAsync(CreateMeetupDto meetupDto)
    {
        Meetup meetup = new(){
            MeetupId = Guid.NewGuid(),
            Title = meetupDto.Title,
            Description = meetupDto.Description,
            Address = meetupDto.Address,
            Image = meetupDto.Image
        };

        await repository.CreateMeetupAsync(meetup);
        return CreatedAtAction(nameof(CreateMeetupAsync), new {id = meetup.MeetupId}, meetup.AsDto());
    }

    //PUT /meetups/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateMeetupAsync(Guid id, UpdateMeetupDto meetupDto)
    {
        var existingMeetup = await repository.GetMeetupAsync(id);

        if(existingMeetup is null)
            return NotFound();
        
        Meetup updatedMeetup = existingMeetup with
        {
            Title = meetupDto.Title,
            Description = meetupDto.Description,
            Address = meetupDto.Address,
            Image = meetupDto.Image
        };

        await repository.UpdateMeetupAsync(updatedMeetup);

        return NoContent();
    }

    //DELETE /meetups/{id}
    [HttpDelete]
    public async Task<ActionResult> DeleteMeetupAsync(Guid id)
    {
        var existingMeetup = await repository.GetMeetupAsync(id);

        if(existingMeetup is null)
            return NotFound();

        await repository.DeleteMeetupAsync(id);

        return NoContent();
    }
}