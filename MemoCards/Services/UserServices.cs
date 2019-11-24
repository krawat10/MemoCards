using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using MemoCards.Data;
using MemoCards.DTOs;
using MemoCards.ExtensionMethods;
using MemoCards.Models;
using Microsoft.EntityFrameworkCore;

namespace MemoCards.Services
{
    public interface IUserService
    {
        Task<UserDTO> Register(string email, string password);
        Task<UserDTO> Login(string email, string password);
        Task<bool> Exists(string email);
    }

    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public UserService(ApplicationDbContext context, IMapper mapper, ITokenService tokenService)
        {
            _context = context;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public async Task<UserDTO> Register(string email, string password)
        {
            if (!await Exists(email)) throw new ArgumentException($"User with email {email} exists", nameof(email));

            var user = new User(email, password.CreatePasswordHashAndSalt());

            return _mapper.Map<UserDTO>(user);
        }



        public async Task<UserDTO> Login(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.Email == email);

            if (user == null) throw new ArgumentException($"User with email {email} does not exists.", nameof(email));

            if (password.VerifyPassword(user.Password))
            {
                var userDto = _mapper.Map<UserDTO>(user);
                userDto.Token = _tokenService.CreateToken(new Claim(nameof(User.Email), user.Email));

                return userDto;
            }

            throw new UnauthorizedAccessException("Bad credentials.");
        }

        public async Task<bool> Exists(string email)
        {
            return await _context.Users.AnyAsync(user => user.Email == email);
        }
    }
}