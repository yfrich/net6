namespace Users.Domain.Entities
{
    //User聚合中的聚合
    public record UserAccessFail
    {
        public Guid Id { get; init; }
        public User User { get; init; }
        public Guid UserId { get; init; }
        private bool isLockOut;
        public DateTime? LockEnd { get; private set; }
        public int AccessFailCount { get; set; }
        private UserAccessFail() { }
        public UserAccessFail(User user)
        {
            this.Id = Guid.NewGuid();
            this.User = user; ;
        }
        public void Reset()
        {
            this.AccessFailCount = 0;
            this.LockEnd = null;
            this.isLockOut = false;
        }
        public void Fail()
        {
            this.AccessFailCount++;
            if (this.AccessFailCount >= 3)
            {
                this.LockEnd = DateTime.Now.AddMinutes(5);
                this.isLockOut = true;
            }
        }
        public bool IsLockOut()
        {
            if (this.isLockOut)
            {
                if (DateTime.Now > LockEnd)//锁定已过期
                {
                    Reset();
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
