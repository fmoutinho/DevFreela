using DevFreela.Core.Entities;
using DevFreela.Core.Enums;
using Xunit;

namespace DevFreela.UnitTests.Core.Entities
{
    public class ProjectTests
    {
        [Fact]
        public void Create_IsSuccessful()
        {
            var project = new Project("Nome teste", "Descrição teste", 1, 2, 1000);

            Assert.Equal(ProjectStatusEnum.Created, project.Status);
            Assert.Null(project.StartedAt);
            Assert.NotEmpty(project.Title);
            Assert.NotEmpty(project.Description);
        }

        [Fact]
        public void Start_IsSuccessful()
        {
            var project = new Project("Nome teste","Descrição teste",1,2,1000);

            project.Start();

            Assert.Equal(ProjectStatusEnum.InProgress, project.Status);
            Assert.NotNull(project.StartedAt);
            Assert.NotEmpty(project.Title);
            Assert.NotEmpty(project.Description);
        }
    }
}
