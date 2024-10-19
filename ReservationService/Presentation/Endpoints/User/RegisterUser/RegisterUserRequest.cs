using Application.User.RegisterUser;

namespace Presentation.Endpoints.User.RegisterUser
{
    public record RegisterUserRequest(string name, string email, string password, RegisterUserRoles role);

}
