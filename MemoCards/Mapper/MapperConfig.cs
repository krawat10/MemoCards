using AutoMapper;
using MemoCards.DTOs;
using MemoCards.Models;

namespace MemoCards.Mapper
{
    public class MapperConfig
    {
        public static IMapper Initialize()
        {
            var mappingConfig = new MapperConfiguration(expression =>
            {
                expression.CreateMap<User, UserDto>();
                expression.CreateMap<MemoCard, MemoCardDto>();
            });

            return mappingConfig.CreateMapper();
        }
    }
}