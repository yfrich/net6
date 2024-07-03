using Users.Domain.ValueObjects;

namespace Users.Domain.Entities
{
    //聚合根
    public record UserLoginHistory : IAggregateRoot
    {
        public long Id { get; init; }
        public Guid? UserId { get; init; }//因为是不同聚合，所以相互之间使用标识符来关联而非实体。指向User实体的外键，但是他在物理上，我们并没有创建他们的外键关系（跨库，等，只是通过标识符来关联）
        public PhoneNumber PhoneNumber { get; init; }
        public DateTime CreateDateTime { get; init; }
        public string Message { get; init; }
        private UserLoginHistory() { }
        public UserLoginHistory(Guid? userId, PhoneNumber phoneNumber, string message)
        {
            this.UserId = userId;
            this.PhoneNumber = phoneNumber;
            this.CreateDateTime = DateTime.Now;
            this.Message = message;
        }
    }
}
