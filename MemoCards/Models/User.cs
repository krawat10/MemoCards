using System;
using MemoCards.Data;
using Microsoft.EntityFrameworkCore;

namespace MemoCards.Models
{
    public class User : Item
    {
        private User()
        {
        }

        public User(string email, Password password)
        {
            Email = email;
            Password = password;
            Created = DateTime.Now;
            Obsolete = false;
        }

        public string Email { get; protected set; }
        public Password Password { get; protected set; }
        public DateTime Created { get; protected set; }
    }

    [Owned]
    public class Password
    {
        public byte[] Hash { get; set; }
        public byte[] Salt { get; set; }
    }
}