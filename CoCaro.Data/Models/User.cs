using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace CoCaro.Data.Models
{
    public partial class User
    {
        public User()
        {
            GameUserId1Navigations = new HashSet<Game>();
            GameUserId2Navigations = new HashSet<Game>();
            Messages = new HashSet<Message>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        public string Username { get; set; }
        [StringLength(100)]
        public string Password { get; set; }
        public int? GoogleId { get; set; }
        public int? FacebookId { get; set; }
        public int? RoleId { get; set; }
        public bool? IsOnline { get; set; }

        [InverseProperty(nameof(Game.UserId1Navigation))]
        public virtual ICollection<Game> GameUserId1Navigations { get; set; }
        [InverseProperty(nameof(Game.UserId2Navigation))]
        public virtual ICollection<Game> GameUserId2Navigations { get; set; }
        [InverseProperty(nameof(Message.User))]
        public virtual ICollection<Message> Messages { get; set; }
    }
}
