namespace ElPro.Server.Services
{
    using ElPro.Server.Data;
    using ElPro.Server.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Threading.Tasks;

    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ApplicationDbContext dbContext;

        public UserService(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext dbContext)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.dbContext = dbContext;
        }

        public async Task<IdentityResult> Create(ApplicationUser user, string password, string role)
        {
            var addRole = IdentityResult.Success;
            var IsRoleExist = await dbContext.Roles.AnyAsync(x => x.Name == role);
            if (!IsRoleExist)
            {
               addRole = await roleManager.CreateAsync(new IdentityRole(role));
            }

            var result = await userManager.CreateAsync(user, password);
            if (result.Succeeded & addRole.Succeeded)
            {
                await userManager.AddToRoleAsync(user, role);
                return IdentityResult.Success;
            }
            return IdentityResult.Failed();
        }
    }
}
