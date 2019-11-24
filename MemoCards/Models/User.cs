using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MemoCards.Data;
using Microsoft.AspNetCore.Mvc;

namespace MemoCards.Models
{
    public class User : Item
    {
        public string Email { get; protected set; }
        public Password Password { get; protected set; }
        public DateTime Created { get; protected set; }

        public User(string email, Password password)
        {
            Email = email;
            Password = password;
            Created = DateTime.Now;
            Obsolete = false;
        }
    }

    public class Password
    {
        public byte[] Hash { get; set; }
        public byte[] Salt { get; set; }
    }
}
