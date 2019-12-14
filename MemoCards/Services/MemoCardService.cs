using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using MemoCards.Data;
using MemoCards.DTOs;
using MemoCards.Models;
using Microsoft.EntityFrameworkCore;

namespace MemoCards.Services
{
    internal class MemoCardService : IMemoCardService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public MemoCardService(ApplicationDbContext context, IUserService userService, IMapper mapper)
        {
            _context = context;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MemoCardDto>> GetUserCards(User user)
        {
            var memoCards = await _context.MemoCards.Where(card => card.User == user).ToListAsync();
            return _mapper.Map<IEnumerable<MemoCardDto>>(memoCards);
        }

        public async Task<MemoCardDto> AddMemoCard(User user, MemoCardDto memoCard)
        {
            var card = new MemoCard(user, HttpUtility.HtmlEncode(memoCard.Name), HttpUtility.HtmlEncode(memoCard.Description));

            await _context.MemoCards.AddAsync(card);
            await _context.SaveChangesAsync();

            return _mapper.Map<MemoCardDto>(card);
        }

        public async Task UpdateMemoCard(MemoCardDto dto)
        {
            var memoCard = await GetById(dto.Id);

            memoCard.SetName(memoCard.Name);
            memoCard.SetDescription(memoCard.Description);

            _context.Update(memoCard);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var memoCard = await GetById(id);
            _context.Remove(memoCard);

            await _context.SaveChangesAsync();
        }

        public async Task<MemoCardDto> Get(Guid id)
        {
            var memoCard = await GetById(id);

            return _mapper.Map<MemoCardDto>(memoCard);
        }

        private async Task<MemoCard> GetById(Guid id)
        {
            return await _context.MemoCards.FirstOrDefaultAsync(card => card.Id == id);
        }
    }
}