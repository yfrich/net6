using MediatR;

namespace MediatRTest1
{
    public class PostNotifHandler2 : NotificationHandler<PostNotification>
    {
        protected override void Handle(PostNotification notification)
        {
            Console.WriteLine("222:" + notification.Body);
        }
    }
}
