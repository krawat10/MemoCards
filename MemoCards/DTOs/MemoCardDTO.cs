using System;

namespace MemoCards.DTOs
{
    public class MemoCardDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}