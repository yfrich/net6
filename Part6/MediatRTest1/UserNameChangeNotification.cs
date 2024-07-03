using MediatR;

namespace MediatRTest1
{
    public record UserNameChangeNotification(string OldUserName, string NewUserName) : INotification;
}
