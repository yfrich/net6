using MediatR;
using Users.Domain.ValueObjects;

namespace Users.Domain
{
    //领域事件
    public record class UserAccessResultEvent(PhoneNumber PhoneNumber, UserAccessResult Result) : INotification;
}
