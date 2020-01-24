using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using MemoCards.Data;
using MemoCards.DTOs;
using MemoCards.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace MemoCards.Services
{
    internal class MemoCardService : IMemoCardService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;

        public MemoCardService(ApplicationDbContext context, IUserService userService, IMapper mapper, IWebHostEnvironment environment)
        {
            _context = context;
            _mapper = mapper;
            _environment = environment;


        }

        public async Task<IEnumerable<MemoCardDto>> GetUserCards(User user)
        {
            // SELECT[m].[Id], [m].[Created], [m].[Description], [m].[Name], [m].[Obsolete], [m].[Updated], [m].[UserId]
            // FROM[MemoCards] AS[m]
            // INNER JOIN[Users] AS[u] ON[m].[UserId] = [u].[Id]
            // WHERE[u].[Id] = Id
            // ORDER BY[m].[Created] DESC
            var memoCards = await _context
                .MemoCards
                .Where(card => card.User == user)
                .OrderByDescending(card => card.Created)
                .ToListAsync();


            return _mapper.Map<IEnumerable<MemoCardDto>>(memoCards);
        }

        public async Task<MemoCardDto> AddMemoCard(User user, MemoCardDto memoCard)
        {
            // HttpUtility encode provided data to prevent storing HTML tags
            var card = new MemoCard(user, HttpUtility.HtmlEncode(memoCard.Name), HttpUtility.HtmlEncode(memoCard.Description));

            await _context.MemoCards.AddAsync(card);
            await _context.SaveChangesAsync();

            return _mapper.Map<MemoCardDto>(card);
        }

        public async Task UpdateMemoCard(MemoCardDto dto)
        {
            var memoCard = await GetById(dto.Id);

            memoCard.SetName(HttpUtility.HtmlEncode(HttpUtility.HtmlEncode(memoCard.Name)));
            memoCard.SetDescription(HttpUtility.HtmlEncode(HttpUtility.HtmlEncode(memoCard.Description)));

            // UPDATE[MemoCards] [Description] = Description, [Name] = Name, [Updated] = Updated
            // WHERE[Id] = MemoIdId;
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