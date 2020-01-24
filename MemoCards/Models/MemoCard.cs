using System;
using System.ComponentModel.DataAnnotations.Schema;
using MemoCards.Data;

namespace MemoCards.Models
{
    public class MemoCard : Item
    {
        [ForeignKey(nameof(UserId))] public User User { get; protected set; }

        [Column(name: "UserId", TypeName = "uniqueidentifier")]
        public Guid UserId { get; protected set; }

        [Column(name: "Created", TypeName = "datetime2")]
        public DateTime Created { get; protected set; }

        [Column(name: "Updated", TypeName = "datetime2")]

        public DateTime Updated { get; protected set; }

        [Column(name: "Name", TypeName = "nvarchar(20)")]

        public string Name { get; protected set; }

        [Column(name: "Description", TypeName = "nvarchar(40)")]

        public string Description { get; protected set; }

        private MemoCard()
        {
        }

        public MemoCard(User user, string name, string description)
        {
            User = user;
            Name = name;
            Description = description;
            Created = DateTime.Now;
            Updated = Created;
            Obsolete = false;
        }

        public void SetName(string name)
        {
            Name = name;
            Updated = DateTime.Now;
        }

        public void SetDescription(string description)
        {
            Description = description;
            Updated = DateTime.Now;
        }

        public void Remove()
        {
            Obsolete = true;
        }
    }
}