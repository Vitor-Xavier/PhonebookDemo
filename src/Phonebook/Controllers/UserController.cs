using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Phonebook.Common;
using Phonebook.Models;
using Phonebook.Services.User;
using System.Threading.Tasks;

[assembly: ApiConventionType(typeof(PhonebookApiConventions))]
namespace Phonebook.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Find a User by its identification.
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <returns>User</returns>
        [HttpGet("{userId:int}")]
        public ValueTask<User> FindUserById(int userId) =>
            _userService.GetUserById(userId);

        [HttpPost("Authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate(AuthenticateModel authenticateModel)
        {
            if (await _userService.Authenticate(authenticateModel.Username, authenticateModel.Password) is User user)
                return Ok(user);
            else
                return BadRequest(new { message = "Username or password is incorrect" });
        }

        /// <summary>
        /// Returns User.
        /// </summary>
        /// <param name="username">Username</param>
        /// <returns>User</returns>
        [HttpGet("{username}")]
        public Task<User> GetUserByUsername(string username) =>
            _userService.GetUserByUsername(username);

        /// <summary>
        /// Add a new User to the Phonebook.
        /// </summary>
        /// <param name="user">User data</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<bool>> PostUser(User user) =>
            Created(nameof(Models.User), await _userService.CreateUser(user));

        /// <summary>
        /// Update an existing User.
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <param name="user">User data</param>
        /// <returns></returns>
        [HttpPut("{userId:int}")]
        public Task<bool> PutUser(int userId, User user) =>
            _userService.UpdateUser(userId, user);

        /// <summary>
        /// Delete User by its identification.
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <returns></returns>
        [HttpDelete("{userId:int}")]
        public Task<bool> DeleteUser(int userId) =>
            _userService.DeleteUser(userId);
    }
}