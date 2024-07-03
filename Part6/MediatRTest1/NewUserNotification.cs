using MediatR;

namespace MediatRTest1
{
    public record NewUserNotification(string UserName, DateTime Time) : INotification;
}
