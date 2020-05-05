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

        public UserController(IUserService userService) =>
            _userService = userService;

        /// <summary>
        /// Find a User by its identification.
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <returns>User</returns>
        [HttpGet("{userId:int}")]
        public ValueTask<User> FindUserById(int userId) =>
            _userService.GetUserById(userId);

        /// <summary>
        /// Authenticate user based on credentials.
        /// </summary>
        /// <param name="authenticateModel">User credentials</param>
        /// <returns>User</returns>
        [HttpPost("Authenticate")]
        [AllowAnonymous]
        public ValueTask<User> Authenticate(AuthenticateModel authenticateModel) =>
            _userService.Authenticate(authenticateModel.Username, authenticateModel.Password);

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
        /// <returns>Created user</returns>
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            await _userService.CreateUser(user);
            return Created(nameof(Models.User), user);
        }

        /// <summary>
        /// Update an existing User.
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <param name="user">User data</param>
        /// <returns>Updated user</returns>
        [HttpPut("{userId:int}")]
        public async Task<ActionResult<User>> PutUser(int userId, User user)
        {
            await _userService.UpdateUser(userId, user);
            return Ok(user);
        }

        /// <summary>
        /// Delete User by its identification.
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <returns></returns>
        [HttpDelete("{userId:int}")]
        public async Task DeleteUser(int userId) =>
            await _userService.DeleteUser(userId);
    }
}