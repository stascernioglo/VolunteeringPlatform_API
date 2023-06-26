namespace VolunteeringPlatform.Bll.Interfaces
{
    public interface IAuthenticationService
    {
        Task<bool> PasswordSignInAsync(string userName, string password);
    }
}
