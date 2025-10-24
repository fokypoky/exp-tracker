using ExpTracker.Api.Extensions;
using ExpTracker.Api.Mapping.ClaimsParser;
using ExpTracker.Api.Mapping.ResponseMapper;
using ExpTracker.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpTracker.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetProfile()
        {
            var user = ClaimsParser.GetUser(this.User);
            
            var response = await _profileService.GetAsync(user);
            
            return this.MapResponse(response);
        }
    }
}