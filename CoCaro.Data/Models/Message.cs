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
        public int? BoardId { get; set; }
        [Column("Message")]
        [StringLength(1000)]
        public string Message1 { get; set; }

        [ForeignKey(nameof(BoardId))]
        [InverseProperty("Messages")]
        public virtual Board Board { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("Messages")]
        public virtual User User { get; set; }
    }
}
