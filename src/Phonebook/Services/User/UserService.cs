﻿using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Phonebook.Common;
using Phonebook.Exceptions;
using Phonebook.Repositories.User;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Phonebook.Services.User
{
    public class UserService : IUserService
    {
        private readonly ILogger _logger;

        private readonly AppSettings _appSettings;

        private readonly IUserRepository _userRepository;

        public UserService(ILogger<UserService> logger, IOptions<AppSettings> appSettings, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
            _appSettings = appSettings.Value;
        }

        public ValueTask<Models.User> GetUserById(int userId, CancellationToken cancellationToken = default) =>
            _userRepository.GetById(userId, cancellationToken);

        public async Task<Models.User> GetUserByUsername(string username, CancellationToken cancellationToken = default) =>
            await _userRepository.GetUserByUsername(username);

        public async ValueTask<Models.User> Authenticate(string username, string password, CancellationToken cancellationToken = default)
        {
            if (await _userRepository.GetUserByUsernamePassword(username, password) is Models.User user)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.Name, user.UserId.ToString()) }),
                    Expires = DateTime.UtcNow.AddMinutes(30),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                user.Token = tokenHandler.WriteToken(token);
                user.Password = null;

                return user;
            }
            _logger.LogInformation($"Authentication failed for user '{username}' at {DateTime.Now}");
            throw new NotFoundException("Username or password is incorrect");
        }

        public async Task CreateUser(Models.User user, CancellationToken cancellationToken = default)
        {
            if (!IsValid(user)) throw new BadRequestException("Registro inválido");

            await _userRepository.Add(user, cancellationToken);
        }

        public async Task UpdateUser(int userId, Models.User user, CancellationToken cancellationToken = default)
        {
            if (!IsValid(user)) throw new BadRequestException("Registro inválido");
            user.UserId = userId;

            await _userRepository.Edit(user, cancellationToken);
        }

        public async Task DeleteUser(int userId, CancellationToken cancellationToken = default)
        {
            Models.User user = new() { UserId = userId, Deleted = true };

            await _userRepository.Delete(user, cancellationToken);
        }

        public bool IsValid(Models.User user) => user is { Username: { Length: > 0 }, Email: { Length: > 0 }, Name: { Length: > 0 } };
    }
}
