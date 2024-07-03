using Microsoft.EntityFrameworkCore;

namespace Users.WebAPI
{
    [AttributeUsage(AttributeTargets.Method)]
    public class UnitOfWorkAttribute : Attribute
    {
        public Type[] DbContextTypes { get; init; }
        public UnitOfWorkAttribute(params Type[] types)
        {
            this.DbContextTypes = types;
        }
    }
}
