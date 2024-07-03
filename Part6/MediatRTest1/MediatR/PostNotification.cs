using MediatR;

namespace MediatRTest1
{
    public record PostNotification(string Body) : INotification;
}
