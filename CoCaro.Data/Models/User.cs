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
            Messages = new HashSet<Message>();
            PlayHistoryUserId1Navigations = new HashSet<PlayHistory>();
            PlayHistoryUserId2Navigations = new HashSet<PlayHistory>();
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

        [InverseProperty(nameof(Message.User))]
        public virtual ICollection<Message> Messages { get; set; }
        [InverseProperty(nameof(PlayHistory.UserId1Navigation))]
        public virtual ICollection<PlayHistory> PlayHistoryUserId1Navigations { get; set; }
        [InverseProperty(nameof(PlayHistory.UserId2Navigation))]
        public virtual ICollection<PlayHistory> PlayHistoryUserId2Navigations { get; set; }
    }
}
