using System.ComponentModel.DataAnnotations.Schema;
using MemoCards.Data;

namespace MemoCards.Models
{
    public enum Language
    {
        EN,
        PL
    }

    public class Word : Item
    {
        public Word()
        {
        }

        [Column(name: "Language", TypeName = "smallint")]
        public Language Language { get; set; }

        [Column(name: "Value", TypeName = "nvarchar(50)")]

        public string Value { get;  set; }

        [Column(name: "Key", TypeName = "nvarchar(30)")]

        public string Key { get;  set; }
    }
}