using System;
using MemoCards.Models;

namespace MemoCards.Data
{
    public class MemoCard : Item
    {
        public User User { get;  }
        public DateTime Created { get; protected set; }
        public DateTime Updated { get; protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }
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