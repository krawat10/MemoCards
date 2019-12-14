using System;
using System.Threading.Tasks;
using MemoCards.DTOs;
using MemoCards.Models;

namespace MemoCards.Services
{
    public interface IUserService
    {
        Task<User> GetUser(Guid id);
        Task<UserDto> Register(string email, string password);
        Task<UserDto> Login(string email, string password);
        Task<bool> Exists(string email);
    }
}