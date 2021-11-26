namespace ElPro.Server.Services
{
    using ElPro.Server.Models;
    using Microsoft.AspNetCore.Identity;
    using System.Threading.Tasks;

    public interface IUserService
    {
        public Task<IdentityResult> Create(ApplicationUser user, string password, string role);

    }
}
