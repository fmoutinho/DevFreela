using MediatR;

namespace DevFreela.Application.Notifications.ProjectCreated
{
    public class GenerateProjectBoardHandler : INotificationHandler<ProjectCreatedNotification>
    {
        public Task Handle(ProjectCreatedNotification notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Creating board for project {notification.Title}");

            return Task.CompletedTask;
        }
    }
}
