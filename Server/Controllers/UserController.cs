namespace ElPro.Server.Controllers
{
	using System.Linq;
    using ElPro.Server.Data;
    using ElPro.Shared;
    using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Mvc;

	[Route("[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
        private readonly ApplicationDbContext dbContext;

        public UserController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

		[HttpGet]
		public ActionResult GetId()
        {
			return Ok(new UserRoleList() { Name = User.Identity.Name });
        }


		[HttpGet("roles/{id}")]
		public ActionResult GetRoles(string id)
		{
			if(id != null)
            {
				var name = dbContext.Users.FirstOrDefault(x => x.Id == id).UserName;
				return Ok(new UserRoleList() { Name = name });
			}

			return NotFound("Not found");
		}
	}
}
