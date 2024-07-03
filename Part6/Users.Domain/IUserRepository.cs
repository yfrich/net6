using Users.Domain.Entities;
using Users.Domain.ValueObjects;

namespace Users.Domain
{
    //仓储
    public interface IUserRepository
    {
        public Task<User?> FindOneAsync(PhoneNumber phoneNumber);
        public Task<User?> FindOneAsync(Guid guid);
        public Task AddNewLoginHistoryAsync(PhoneNumber phoneNumber, string message);
        public Task SavePhoneNumberCodeAsync(PhoneNumber phoneNumber, string code);
        public Task<string?> FindPhoneNumberCodeAsync(PhoneNumber phoneNumber);

        public Task PublishEventAsync(UserAccessResultEvent _event);

    }
}
