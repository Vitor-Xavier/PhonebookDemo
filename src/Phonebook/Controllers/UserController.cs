using Microsoft.AspNetCore.Mvc;
using Phonebook.Models;
using Phonebook.Services.User;
using System.Threading.Tasks;

namespace Phonebook.Controllers
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

        /// <summary>
        /// Find a User by its identification.
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <returns>User</returns>
        [HttpGet("{userId:int}")]
        public async Task<User> FindUserById(int userId) =>
            await _userService.GetUserById(userId);

        /// <summary>
        /// Returns User.
        /// </summary>
        /// <param name="username">Username</param>
        /// <returns>User</returns>
        [HttpGet("{username}")]
        public async Task<User> GetUserByUsername(string username) =>
            await _userService.GetUserByUsername(username);

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
        public async Task<bool> PutUser(int userId, User user) =>
            await _userService.UpdateUser(userId, user);

        /// <summary>
        /// Delete User by its identification.
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <returns></returns>
        [HttpDelete("{userId:int}")]
        public async Task<bool> DeleteUser(int userId) =>
            await _userService.DeleteUser(userId);
    }
}