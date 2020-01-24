using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MemoCards.Data
{
    public abstract class Item
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Column(name: "Id", TypeName = "uniqueidentifier")]

        public Guid Id { get; protected set; }
        public bool Obsolete { get; protected set; }
    }
}