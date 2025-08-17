using Microsoft.AspNetCore.Mvc;

namespace ExpTracker.Api.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class ProfileController : ControllerBase
	{
		[HttpGet]
		public async Task<IResult> GetProfile()
		{
			throw new NotImplementedException("Not implemented");
		}
	}
}
