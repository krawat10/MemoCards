using System.ComponentModel.DataAnnotations;

namespace MemoCards.Data
{
    public abstract class Item
    {
        [Key]
        public long Id { get;  }
        public bool Obsolete { get; protected set; }
    }
}