using DevFreela.Application.Commands.ProjectCommands.InsertProject;
using DevFreela.Application.Notifications.ProjectCreated;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using MediatR;
using Moq;

namespace DevFreela.UnitTests.Application.Commands.ProjectCommands
{
    public class InsertProjectCommandHandlerTests
    {
        [Fact]
        public async Task InputDataIsOk_Executed_ReturnProjectId()
        {
            // Arrange
            var projectRepositoryMock = new Mock<IProjectRepository>();
            projectRepositoryMock.Setup(pr => pr.AddAsync(It.IsAny<Project>())).ReturnsAsync(1);

            var mediatorMock = new Mock<IMediator>();

            var insertProjectCommand = new InsertProjectCommand("New Project", "Description", 1, 2, 1000);
            var insertProjectCommandHandler = new InsertProjectCommandHandler(projectRepositoryMock.Object, mediatorMock.Object);

            // Act
            var result = await insertProjectCommandHandler.Handle(insertProjectCommand, new CancellationToken());

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Data >= 0);
            projectRepositoryMock.Verify(pr => pr.AddAsync(It.IsAny<Project>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ValidCommand_PublishesNotification()
        {
            // Arrange
            var projectRepositoryMock = new Mock<IProjectRepository>();
            projectRepositoryMock.Setup(pr => pr.AddAsync(It.IsAny<Project>())).ReturnsAsync(1);

            var mediatorMock = new Mock<IMediator>();

            var insertProjectCommand = new InsertProjectCommand("New Project", "Description", 1, 2, 1000);
            var insertProjectCommandHandler = new InsertProjectCommandHandler(projectRepositoryMock.Object, mediatorMock.Object);

            // Act
            var result = await insertProjectCommandHandler.Handle(insertProjectCommand, new CancellationToken());

            // Assert
            Assert.NotNull(result);
            mediatorMock.Verify(m => m.Publish(It.IsAny<ProjectCreatedNotification>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_AddAsyncFails_ThrowsException()
        {
            // Arrange
            var projectRepositoryMock = new Mock<IProjectRepository>();
            projectRepositoryMock.Setup(pr => pr.AddAsync(It.IsAny<Project>())).ThrowsAsync(new Exception("Database error"));

            var mediatorMock = new Mock<IMediator>();

            var insertProjectCommand = new InsertProjectCommand("New Project", "Description", 1, 2, 1000);
            var insertProjectCommandHandler = new InsertProjectCommandHandler(projectRepositoryMock.Object, mediatorMock.Object);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () =>
                await insertProjectCommandHandler.Handle(insertProjectCommand, new CancellationToken()));

            projectRepositoryMock.Verify(pr => pr.AddAsync(It.IsAny<Project>()), Times.Once);
            mediatorMock.Verify(m => m.Publish(It.IsAny<ProjectCreatedNotification>(), It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}