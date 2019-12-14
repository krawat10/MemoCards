using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using MemoCards.Data;
using MemoCards.DTOs;
using MemoCards.Exceptions;
using MemoCards.ExtensionMethods;
using MemoCards.Models;
using Microsoft.EntityFrameworkCore;

namespace MemoCards.Services
{
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

        public async Task<User> GetUser(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<UserDto> Register(string email, string password)
        {
            if (await Exists(email)) throw new ArgumentException($"User with email {email} exists", nameof(email));

            var user = new User(email, password.CreatePasswordHashAndSalt());
            _context.Add(user);
            await _context.SaveChangesAsync();

            var userDto = _mapper.Map<UserDto>(user);
            userDto.Token = _tokenService.CreateToken(new Claim(nameof(User.Id), user.Id.ToString()));
            
            return userDto;
        }



        public async Task<UserDto> Login(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(usr => usr.Email == email);

            if (user == null) throw new UserNotFoundException(email);

            if (password.VerifyPassword(user.Password))
            {
                var userDto = _mapper.Map<UserDto>(user);
                userDto.Token = _tokenService.CreateToken(new Claim(nameof(User.Id), user.Id.ToString()));

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