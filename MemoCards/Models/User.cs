using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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

        public ICollection<MemoCard> MemoCards { get; protected set; }

        [Column(name: "Email", TypeName = "nvarchar(30)")]
        public string Email { get; protected set; }

        public Password Password { get; protected set; }

        [Column(name: "Created", TypeName = "datetime2")]

        public DateTime Created { get; protected set; }
    }

    [Owned]
    public class Password
    {
        [Column(name:"Hash", TypeName = "varbinary(64)")]
        public byte[] Hash { get; set; }
        [Column(name: "Salt", TypeName = "varbinary(128)")]
        public byte[] Salt { get; set; }
    }
}