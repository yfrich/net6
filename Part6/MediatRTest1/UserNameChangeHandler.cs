using MediatR;

namespace MediatRTest1
{
    public class UserNameChangeHandler : NotificationHandler<UserNameChangeNotification>
    {
        protected override void Handle(UserNameChangeNotification notification)
        {
            Console.WriteLine($"用户名从{notification.OldUserName}变成了{notification.NewUserName}");
        }
    }
}
