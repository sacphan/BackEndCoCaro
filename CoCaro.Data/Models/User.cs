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
        public int? RoleId { get; set; }
        public int? Cup { get; set; }
        public double? RateWin { get; set; }
        public int? TotalGame { get; set; }
        public bool? IsBlock { get; set; }
        [StringLength(200)]
        public string Email { get; set; }
        [StringLength(200)]
        public string FullName { get; set; }
        [StringLength(50)]
        public string FacebookId { get; set; }
        [StringLength(50)]
        public string GoogleId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreateDated { get; set; }

        [InverseProperty(nameof(Game.UserId1Navigation))]
        public virtual ICollection<Game> GameUserId1Navigations { get; set; }
        [InverseProperty(nameof(Game.UserId2Navigation))]
        public virtual ICollection<Game> GameUserId2Navigations { get; set; }
        [InverseProperty(nameof(Message.User))]
        public virtual ICollection<Message> Messages { get; set; }
    }
}
