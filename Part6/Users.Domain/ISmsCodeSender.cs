using Users.Domain.ValueObjects;

namespace Users.Domain
{
    //防腐层
    public interface ISmsCodeSender
    {
        Task SendAsync(PhoneNumber phoneNumber, string code);
    }
}
