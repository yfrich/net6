using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Users.Domain;
using Users.Domain.Entities;
using Users.Domain.ValueObjects;
using Zack.Infrastructure.EFCore;

namespace Users.Infrastructure
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext dbContext;
        private readonly IDistributedCache distributedCache;
        private readonly IMediator mediator;

        public UserRepository(UserDbContext dbContext, IDistributedCache distributedCache, IMediator mediator)
        {
            this.dbContext = dbContext;
            this.distributedCache = distributedCache;
            this.mediator = mediator;
        }

        public async Task AddNewLoginHistoryAsync(PhoneNumber phoneNumber, string message)
        {
            User? user = await FindOneAsync(phoneNumber);
            Guid? userId = null;
            if (user != null)
            {
                userId = user.Id;
            }
            dbContext.UserLoginHistories.Add(new UserLoginHistory(userId, phoneNumber, message));
            //dbContext.SaveChanges();//一般不在Repository直接SaveChanges 
        }

        public async Task<User?> FindOneAsync(PhoneNumber phoneNumber)
        {
            //可以手动比较是否相等，对每个对象进行判断
            User? user = await dbContext.Users.Include(t => t.UserAccessFail).SingleOrDefaultAsync(ExpressionHelper.MakeEqual((User u) => u.PhoneNumber, phoneNumber));
            return user;
        }

        public async Task<User?> FindOneAsync(Guid guid)
        {
            User? user = await dbContext.Users.Include(t => t.UserAccessFail).SingleOrDefaultAsync(t => t.Id == guid);
            return user;
        }

        public async Task<string?> FindPhoneNumberCodeAsync(PhoneNumber phoneNumber)
        {
            string key = $"PhoneNumberCode_{phoneNumber.RegionNumber}_{phoneNumber.Number}";
            string? code = await distributedCache.GetStringAsync(key);
            await distributedCache.RemoveAsync(key);
            return code;
        }

        public Task PublishEventAsync(UserAccessResultEvent _event)
        {
            return mediator.Publish(_event);
        }

        public Task SavePhoneNumberCodeAsync(PhoneNumber phoneNumber, string code)
        {
            string key = $"PhoneNumberCode_{phoneNumber.RegionNumber}_{phoneNumber.Number}";
            //加入分布式缓存
            return distributedCache.SetStringAsync(key, code, new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5) });
        }
    }
}
