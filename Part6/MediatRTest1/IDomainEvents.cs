using MediatR;

namespace MediatRTest1
{
    public interface IDomainEvents
    {
        //获取已注册Events
        IEnumerable<INotification> GetDomainEvents();
        //注册Event
        void AddDomainEvent(INotification notification);
        void ClearDomainEvent();
    }
}
