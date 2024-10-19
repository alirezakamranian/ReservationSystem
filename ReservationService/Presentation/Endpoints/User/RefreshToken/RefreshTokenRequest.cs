namespace Presentation.Endpoints.User.RefreshToken
{
    public record RefreshTokenRequest(string ExpiredAccessToken, string RefreshToken);
}
