using Application.User.RegisterUser;

namespace Presentation.Endpoints.User.RegisterUser
{
    public record RegisterUserRequest(string Name, string Email, string Password, RegisterUserRoles Role);

}
