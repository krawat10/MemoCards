using System;

namespace MemoCards.Services
{
    public class TokenDTO
    {
        public string Value { get; set; }
        public DateTime Expires { get; set; }
    }
}