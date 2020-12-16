using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace CoCaro.Data.Models
{
    [Table("Board")]
    public partial class Board
    {
        public Board()
        {
            Messages = new HashSet<Message>();
            PlayHistories = new HashSet<PlayHistory>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        public int? Status { get; set; }

        [InverseProperty(nameof(Message.Board))]
        public virtual ICollection<Message> Messages { get; set; }
        [InverseProperty(nameof(PlayHistory.Board))]
        public virtual ICollection<PlayHistory> PlayHistories { get; set; }
    }
}
