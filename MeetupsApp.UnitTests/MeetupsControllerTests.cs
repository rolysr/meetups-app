namespace MeetupsApp.UnitTests;

public class MeetupsControllerTests
{
    private readonly Mock<IMeetupsRepository> repositoryStub = new();
    private readonly Mock<ILogger<MeetupsController>> loggerStub = new();
    private readonly Random rand = new();

    [Fact]
    public async Task GetMeetupAsync_WithUnexistingMeetup_RetursnNotFound()
    {
        //Arrange
        var repositoryStub = new Mock<IMeetupsRepository>();
        repositoryStub.Setup(repo => repo.GetMeetupAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Meetup)null);

        var loggerStub = new Mock<ILogger<MeetupsController>>();

        var controller = new MeetupsController(repositoryStub.Object, loggerStub.Object);

        //Act
        var result = await controller.GetMeetupAsync(Guid.NewGuid());
 
        //Assert
        result.Result.Should().BeOfType<NotFoundResult>(); 
    }

    [Fact]
    public async Task GetMeetupAsync_WithExistingMeetup_ReturnsExpectedMeetup()
    {
        //Arrange
        Meetup expectedMeetup = CreateRandomMeetup();

        repositoryStub.Setup(repo => repo.GetMeetupAsync(It.IsAny<Guid>()))
            .ReturnsAsync(expectedMeetup);

        var controller = new MeetupsController(repositoryStub.Object, loggerStub.Object);

        //Act
        var result = await controller.GetMeetupAsync(Guid.NewGuid());

        //Assert
        result.Value.Should().BeEquivalentTo(expectedMeetup, options => options.ExcludingMissingMembers());
    }

    [Fact]
    public async Task GetMeetupsAsync_WithExistingMeetups_ReturnsAllMeetups()
    {
        //Arrange
        var expectedMeetups = new[]{CreateRandomMeetup(), CreateRandomMeetup(), CreateRandomMeetup()};

        repositoryStub.Setup(repo => repo.GetMeetupsAsync())
            .ReturnsAsync(expectedMeetups);

        var controller = new MeetupsController(repositoryStub.Object, loggerStub.Object);

        //Act
        var actualMeetups = await controller.GetMeetupsAsync();

        //Assert
        actualMeetups.Should().BeEquivalentTo(expectedMeetups, options => options.ExcludingMissingMembers());
    }

    [Fact]
    public async Task CreateMeetupAsync_WithMeetupToCreate_ReturnsCreatedMeetup()
    {
        //Arrange
        var artworkToCreate = new CreateMeetupDto(){
            Title = Guid.NewGuid().ToString(),
            Address = Guid.NewGuid().ToString(),
            Description = Guid.NewGuid().ToString(),
            Image = Guid.NewGuid().ToString(),
        };

        var controller = new MeetupsController(repositoryStub.Object, loggerStub.Object);

        //Act
        var result = await controller.CreateMeetupAsync(artworkToCreate);

        //Assert
        var createdMeetup = (result.Result as CreatedAtActionResult).Value as MeetupDto;
        artworkToCreate.Should().BeEquivalentTo(
            createdMeetup,
            options => options.ComparingByMembers<MeetupDto>().ExcludingMissingMembers()
        );
        createdMeetup.MeetupId.Should().NotBeEmpty();
    }

    [Fact]
    public async Task UpdateMeetupAsync_WithExistingMeetup_ReturnsNoContent()
    {
        //Arrange
        Meetup existingMeetup = CreateRandomMeetup();
        repositoryStub.Setup(repo => repo.GetMeetupAsync(It.IsAny<Guid>()))
            .ReturnsAsync(existingMeetup);

        var artworkId = existingMeetup.MeetupId;
        var artworkToUpdate = new UpdateMeetupDto(){
            Title = Guid.NewGuid().ToString(),
            Address = Guid.NewGuid().ToString(),
            Description = Guid.NewGuid().ToString(),
            Image = Guid.NewGuid().ToString(),
        };

        var controller = new MeetupsController(repositoryStub.Object, loggerStub.Object);

        //Act
        var result = await controller.UpdateMeetupAsync(artworkId, artworkToUpdate);

        //Assert
        result.Should().BeOfType<NoContentResult>();
    }

    [Fact]
    public async Task DeleteMeetupAsync_WithExistingMeetup_ReturnsNoContent()
    {
        //Arrange
        Meetup existingMeetup = CreateRandomMeetup();
        repositoryStub.Setup(repo => repo.GetMeetupAsync(It.IsAny<Guid>()))
            .ReturnsAsync(existingMeetup);

        var controller = new MeetupsController(repositoryStub.Object, loggerStub.Object);

        //Act
        var result = await controller.DeleteMeetupAsync(existingMeetup.MeetupId);

        //Assert
        result.Should().BeOfType<NoContentResult>(); 
    }

    private Meetup CreateRandomMeetup()
    {
        return new ()
        {
            MeetupId = Guid.NewGuid(),
            Title = Guid.NewGuid().ToString(),
            Address = Guid.NewGuid().ToString(),
            Description = Guid.NewGuid().ToString(),
            Image = Guid.NewGuid().ToString(),
        };
    }
}