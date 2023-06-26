using Microsoft.AspNetCore.Identity;
using VolunteeringPlatform.Bll.Interfaces;
using VolunteeringPlatform.Domain.Auth;

namespace VolunteeringPlatform.Bll.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly SignInManager<User> _signInManager;

        public AuthenticationService(SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<bool> PasswordSignInAsync(string userName, string password)
        {
            var checkingPasswordResult = await _signInManager.PasswordSignInAsync(userName, password, false, false);

            return checkingPasswordResult.Succeeded;
        }

    }
}
