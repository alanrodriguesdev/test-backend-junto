using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TestBackendUser.Domain.Commands;
using TestBackendUser.Service.Interfaces;

namespace TestBackendUser.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]  
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Create(UserCommand command)
        {
            var response = await _userService.Insert(command);

            if (response.Errors.Count > 0 || !response.Success)
                return BadRequest(response);

            return new CreatedResult(nameof(GetUserById),response);
        }
        [HttpPut]
        [Authorize]
        [Route("{userId}")]
        public async Task<ActionResult> Update([FromBody] UpdateUserCommand command,int userId)
        {
           
            var response = await _userService.Update(command,userId);

            if (response.Errors.Count > 0 || !response.Success)
                return BadRequest(response);

            return Ok(response);
        }
        [HttpDelete]
        [Authorize]
        public async Task<ActionResult> Delete(DeleteUserCommand command)
        {
            var response = await _userService.Delete(command);

            if (response.Errors.Count > 0 || !response.Success)
                return BadRequest(response);

            return Ok(response);
        }
        [HttpGet]
        public async Task<ActionResult> GetAllUsers()
        {
            var response = await _userService.GetAllUsers();

            if (response.Errors.Count > 0 || !response.Success)
                return BadRequest(response);

            return Ok(response);
        }
        [HttpGet]
        [Route("{userId}")]
        public async Task<ActionResult> GetUserById(int userId)
        {
            var response = await _userService.GetUserById(userId);

            if (response.Errors.Count > 0 || !response.Success)
                return BadRequest(response);

            return Ok(response);
        }
    }
}
