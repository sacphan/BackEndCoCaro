using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace CoCaro.Data.Models
{
    public partial class Message
    {
        [Key]
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? GameId { get; set; }
        [Column("Message")]
        [StringLength(1000)]
        public string Message1 { get; set; }
        public int? Turn { get; set; }
        public int? TimeOfTurn { get; set; }

        [ForeignKey(nameof(GameId))]
        [InverseProperty("Messages")]
        public virtual Game Game { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("Messages")]
        public virtual User User { get; set; }
    }
}
