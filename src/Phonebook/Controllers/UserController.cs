using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Phonebook.Common;
using Phonebook.Models;
using Phonebook.Services.User;
using System.Threading;
using System.Threading.Tasks;

[assembly: ApiConventionType(typeof(PhonebookApiConventions))]
namespace Phonebook.Controllers
{
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
        /// <param name="cancellationToken">Request Cancellation Token</param>
        /// <returns>User</returns>
        [HttpGet("{userId:int}")]
        public ValueTask<User> FindUserById(int userId, CancellationToken cancellationToken) =>
            _userService.GetUserById(userId, cancellationToken);

        /// <summary>
        /// Authenticate user based on credentials.
        /// </summary>
        /// <param name="authenticateModel">User credentials</param>
        /// <param name="cancellationToken">Request Cancellation Token</param>
        /// <returns>User</returns>
        [HttpPost("Authenticate")]
        [AllowAnonymous]
        public ValueTask<User> Authenticate(AuthenticateModel authenticateModel, CancellationToken cancellationToken) =>
            _userService.Authenticate(authenticateModel.Username, authenticateModel.Password, cancellationToken);

        /// <summary>
        /// Returns User.
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="cancellationToken">Request Cancellation Token</param>
        /// <returns>User</returns>
        [HttpGet("{username}")]
        public Task<User> GetUserByUsername(string username, CancellationToken cancellationToken) =>
            _userService.GetUserByUsername(username, cancellationToken);

        /// <summary>
        /// Add a new User to the Phonebook.
        /// </summary>
        /// <param name="user">User data</param>
        /// <param name="cancellationToken">Request Cancellation Token</param>
        /// <returns>Created user</returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<User>> PostUser(User user, CancellationToken cancellationToken)
        {
            await _userService.CreateUser(user, cancellationToken);
            return Created(nameof(Models.User), user);
        }

        /// <summary>
        /// Update an existing User.
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <param name="user">User data</param>
        /// <param name="cancellationToken">Request Cancellation Token</param>
        /// <returns>Updated user</returns>
        [HttpPut("{userId:int}")]
        public async Task<ActionResult<User>> PutUser(int userId, User user, CancellationToken cancellationToken)
        {
            await _userService.UpdateUser(userId, user, cancellationToken);
            return Ok(user);
        }

        /// <summary>
        /// Delete User by its identification.
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <param name="cancellationToken">Request Cancellation Token</param>
        /// <returns></returns>
        [HttpDelete("{userId:int}")]
        public async Task<ActionResult> DeleteUser(int userId, CancellationToken cancellationToken)
        {
            await _userService.DeleteUser(userId, cancellationToken);
            return NoContent();
        }
    }
}