using AutoMapper;
using MemoCards.DTOs;
using MemoCards.Models;
using MemoCards.Services;

namespace MemoCards.Mapper
{
    public class MemoCardProfile : Profile
    {
        public MemoCardProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<MemoCard, MemoCardDto>();
        }
    }
}