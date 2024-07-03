using MediatR;

namespace MediatRTest1
{
    public class PostNotifHandler1 : NotificationHandler<PostNotification>
    {
        protected override void Handle(PostNotification notification)
        {
            Console.WriteLine("111:" + notification.Body);
        }
    }
}
