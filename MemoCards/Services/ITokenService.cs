using System.Security.Claims;

namespace MemoCards.Services
{
    public interface ITokenService
    {
        TokenDTO CreateToken(params Claim[] claims);
    }
}