using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MemoCards.DTOs;
using MemoCards.Models;

namespace MemoCards.Services
{
    public interface IMemoCardService
    {
        Task<IEnumerable<MemoCardDto>> GetUserCards(User user);
        Task<MemoCardDto> AddMemoCard(User user, MemoCardDto memoCard);
        Task<MemoCardDto> Get(Guid id);
        Task UpdateMemoCard(MemoCardDto dto);
        Task Delete(Guid id);
    }
}