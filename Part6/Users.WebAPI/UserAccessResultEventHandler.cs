using MediatR;
using Users.Domain;
using Users.Infrastructure;

namespace Users.WebAPI
{
    public class UserAccessResultEventHandler : INotificationHandler<UserAccessResultEvent>
    {
        private readonly IUserRepository userRepository;
        private readonly UserDbContext userDbContext;

        public UserAccessResultEventHandler(IUserRepository userRepository, UserDbContext userDbContext)
        {
            this.userRepository = userRepository;
            this.userDbContext = userDbContext;
        }

        //private readonly IServiceScopeFactory serviceScopeFactory;

        /*
        public UserAccessResultEventHandler(IServiceScopeFactory serviceScopeFactory)
        {
            this.serviceScopeFactory = serviceScopeFactory;
        }
        */

        public async Task Handle(UserAccessResultEvent notification, CancellationToken cancellationToken)
        {
            /*
            using var scope = this.serviceScopeFactory.CreateScope();
            await scope.ServiceProvider.GetRequiredService<IUserRepository>().AddNewLoginHistoryAsync(notification.PhoneNumber, $"登录结果是:{notification.Result}");
            await scope.ServiceProvider.GetRequiredService<UserDbContext>().SaveChangesAsync();
            */
            await userRepository.AddNewLoginHistoryAsync(notification.PhoneNumber, $"登录结果是:{notification.Result}");
            await userDbContext.SaveChangesAsync();
        }

    }
}
