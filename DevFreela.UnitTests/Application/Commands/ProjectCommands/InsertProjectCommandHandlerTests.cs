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
        public async Task InputDataIsOkd_Executed_ReturnProjectId()
        {
            //Arrange
            var projectRepositoryMock = new Mock<IProjectRepository>();
            var insertProjectCommandMock = new Mock<InsertProjectCommand>();
            projectRepositoryMock.Setup(pr => pr.AddAsync(It.IsAny<Project>()).Result).Returns(1);

            var mediatorMock = new Mock<IMediator>().Object;

            var insertProjectCommandHandler = new InsertProjectCommandHandler(projectRepositoryMock.Object, mediatorMock);
            //Act

            var result = await insertProjectCommandHandler.Handle(insertProjectCommandMock.Object, new CancellationToken());

            //Assert
            Assert.NotNull(result);
            Assert.True(result.Data >= 0);

            projectRepositoryMock.Verify(pr => pr.AddAsync(It.IsAny<Project>()).Result, Times.Once);
        }
    }
}
