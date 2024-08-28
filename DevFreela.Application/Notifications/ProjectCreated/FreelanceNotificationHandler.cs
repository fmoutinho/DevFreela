using MediatR;

namespace DevFreela.Application.Notifications.ProjectCreated
{
    public class FreelanceNotificationHandler : INotificationHandler<ProjectCreatedNotification>
    {
        public Task Handle(ProjectCreatedNotification notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Notifying freelancers about project {notification.Title}");

            return Task.CompletedTask;
        }
    }
}
