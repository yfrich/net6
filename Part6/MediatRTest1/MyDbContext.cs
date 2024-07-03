using MediatR;
using MediatRTest1;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace EFCore实体属性操作的秘密1
{
    public class MyDbContext : DbContext
    {
        private readonly IMediator mediator;
        public DbSet<User> Users { get; set; }
        public MyDbContext(DbContextOptions<MyDbContext> options, IMediator mediator) : base(options)
        {
            this.mediator = mediator;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            //过滤出有带发送消息的domain对象
            var domainEntitys = this.ChangeTracker.Entries<IDomainEvents>().Where(t => t.Entity.GetDomainEvents().Any());
            //获取所有事件
            var domainEvents = domainEntitys.SelectMany(t => t.Entity.GetDomainEvents()).ToList();
            //清空事件
            domainEntitys.ToList().ForEach(t => t.Entity.ClearDomainEvent());
            foreach (var domainEvent in domainEvents)
            {
                await mediator.Publish(domainEvent);
            }
            //把消息的发布放在base.SaveChangesAsync 之前，可以保证领域事件响应代码中的事务操作和base.SaveChangesAsync中的代码在同一个食物中，实现强一致性事务
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
