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

        public UserController(ApplicationDbContext dbContext,UserManager<ApplicationUser> userManager)
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
		public async Task<List<UserRoleList>> GetRoles(string id)
		{
			List<UserRoleList> userRoleList = new();

			if(id != null)
            {
				var user = dbContext.Users.FirstOrDefault(x => x.Id == id);
				var role = await userManager.GetRolesAsync(user);

                foreach (var item in role)
                {
                    userRoleList.Add(new UserRoleList() { Name = item });
                }

				return userRoleList;
			}

            return userRoleList;
        }
	}
}
