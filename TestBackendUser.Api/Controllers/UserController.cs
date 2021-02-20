using Microsoft.AspNetCore.Authorization;
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
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }
       
        [HttpPost]
        [Route("create")]
        public async Task<ActionResult> Create(UserCommand command)
        {
            var response = _userService.Insert(command);

            if (response.Errors.Count > 0 || !response.Success)
                return BadRequest(response);

            return Ok(response);
        }
        [HttpPut]
        [Authorize]
        [Route("update")]
        public async Task<ActionResult> Update([FromBody] UserCommand command)
        {           
            var response = _userService.Update(command);

            if (response.Errors.Count > 0 || !response.Success)
                return BadRequest(response);

            return Ok(response);
        }
        [HttpDelete]
        [Authorize]
        public async Task<ActionResult> Delete(DeleteUserCommand command)
        {
            var response = _userService.Delete(command);

            if (response.Errors.Count > 0 || !response.Success)
                return BadRequest(response);

            return Ok(response);
        }
    }
}
