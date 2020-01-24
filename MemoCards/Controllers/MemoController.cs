using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityModel;
using MemoCards.Data;
using MemoCards.DTOs;
using MemoCards.Models;
using MemoCards.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MemoCards.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class MemoController : Controller
    {
        private readonly IMemoCardService _cardService;

        public MemoController(IMemoCardService cardService, IUserService userService)
        {
            _cardService = cardService;
        }

        [HttpGet]
        public async Task<IEnumerable<MemoCardDto>> Get()
        {
            var user = HttpContext.Items["User"] as User;

            return await _cardService.GetUserCards(user); // Parsed to JSON
        }

        [HttpPut]
        public async Task Delete(Guid id)
        {
            await _cardService.Delete(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post(MemoCardDto dto)
        {
            var user = HttpContext.Items["User"] as User;

            var card = await _cardService.Get(dto.Id);

            if (card != null)
            {
                return BadRequest($"Card with guid {dto.Id} exists.");
            }

            card = await _cardService.AddMemoCard(user, dto);

            return CreatedAtAction(nameof(Post), new {id = card.Id}, dto);
        }

        [HttpPut("{id}")]
        public async Task Put(MemoCardDto dto, string id)
        {
            await _cardService.UpdateMemoCard(dto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = HttpContext.Items["User"] as User;

            if (Guid.TryParse(id, out var guid))
            {
                var card = await _cardService.Get(guid);

                if (user.Id == card.UserId)
                {
                    await _cardService.Delete(guid);
                    return new OkResult();
                }
            }

            return NotFound($"Memo car with id {id} does not exists");
        }
    }
}