using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TestBackendUser.Domain.Commands;
using TestBackendUser.Service.Interfaces;

namespace TestBackendUser.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ISecurityService _securityService;

        public SecurityController(IUserService userService, ISecurityService securityService)
        {
            _userService = userService;
            _securityService = securityService;
        }
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login(LoginCommand command)
        {
            var response = await _userService.Login(command);

            if (response.Errors.Count > 0 || !response.Success)
                return BadRequest(response);

            var token = await _securityService.GerarJwt(response.Data);

            return Ok(token);
        }
        
    }
}
