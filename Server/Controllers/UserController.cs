namespace ElPro.Server.Controllers
{
    using ElPro.Server.Data;
    using ElPro.Server.Models;
    using ElPro.Shared;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;

        public UserController(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }

        [HttpGet]
        public ActionResult GetId()
        {
            return Ok(new UserRoleList() { Name = User.Identity.Name });
        }


        [HttpGet("roles/{id}")]
        public async Task<List<string>> GetRoles(string id)
        {
            List<string> roles = new();

            if (id != null)
            {
                var user = dbContext.Users.FirstOrDefault(x => x.Id == id);
                var rolesList = await userManager.GetRolesAsync(user);

                roles.AddRange(rolesList);

                return roles;
            }

            return roles;
        }
    }
}
