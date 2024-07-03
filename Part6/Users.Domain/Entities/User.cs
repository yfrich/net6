using Users.Domain.ValueObjects;
using Zack.Commons;

namespace Users.Domain.Entities
{
    //聚合根
    public record User : IAggregateRoot
    {
        public Guid Id { get; init; }
        public PhoneNumber PhoneNumber { get; private set; }
        private string? passwordHash;
        public UserAccessFail UserAccessFail { get; private set; }

        private User()
        {

        }
        public User(PhoneNumber phoneNumber)
        {
            this.PhoneNumber = phoneNumber;
            this.Id = Guid.NewGuid();
            this.UserAccessFail = new UserAccessFail(this);
        }
        public bool HasPassword()
        {
            return !string.IsNullOrEmpty(passwordHash);
        }
        public void ChangePassword(string password)
        {
            if (password.Length <= 3)
            {
                throw new ArgumentException("密码长度必须大于3");
            }
            this.passwordHash = HashHelper.ComputeMd5Hash(password);
        }

        public bool CheckPassword(string password)
        {
            return this.passwordHash == HashHelper.ComputeMd5Hash(password);
        }

        public void ChangePhoneNumber(PhoneNumber phoneNumber)
        {
            this.PhoneNumber = phoneNumber;
        }
    }
}
