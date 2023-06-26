namespace VolunteeringPlatform.Bll.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateAccessToken(string username);
    }
}
