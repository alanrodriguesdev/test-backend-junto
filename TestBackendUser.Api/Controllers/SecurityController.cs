using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestBackendUser.Domain.Commands;
using TestBackendUser.Service;

namespace TestBackendUser.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly SecurityService _securityService;

        public SecurityController(UserService userService, SecurityService securityService)
        {
            _userService = userService;
            _securityService = securityService;
        }
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login(LoginCommand command)
        {
            var response = _userService.Login(command);

            if (response.Errors.Count > 0 || !response.Success)
                return BadRequest(response);

            var token = _securityService.GerarJwt(response.Data);

            return Ok(token);
        }
        
    }
}
