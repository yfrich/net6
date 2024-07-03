using MediatR;

namespace MediatRTest1
{
    public class NewUserHandler : NotificationHandler<NewUserNotification>
    {
        protected override void Handle(NewUserNotification notification)
        {
            Console.WriteLine($"用户新增了:{notification.UserName}:{notification.Time}");
        }
    }
}
