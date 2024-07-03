using Users.Domain;
using Users.Domain.ValueObjects;

namespace Users.Infrastructure
{
    public class MockSmsCodeSender : ISmsCodeSender
    {
        public Task SendAsync(PhoneNumber phoneNumber, string code)
        {
            Console.WriteLine($"向{phoneNumber.RegionNumber}-{phoneNumber.Number} 发送验证码{code}");
            return Task.CompletedTask;
        }
    }
}
