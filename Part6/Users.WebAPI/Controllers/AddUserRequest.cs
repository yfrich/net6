using Users.Domain.ValueObjects;

namespace Users.WebAPI.Controllers
{
    public record AddUserRequest(PhoneNumber phoneNumber, string password);
}
