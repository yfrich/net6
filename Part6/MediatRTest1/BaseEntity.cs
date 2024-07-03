using MediatR;

namespace MediatRTest1
{
    public abstract class BaseEntity : IDomainEvents
    {
        private IList<INotification> events = new List<INotification>();
        public void AddDomainEvent(INotification notification)
        {
            events.Add(notification);
        }

        public void ClearDomainEvent()
        {
            events.Clear();
        }

        public IEnumerable<INotification> GetDomainEvents()
        {
            return events;
        }
    }
}
