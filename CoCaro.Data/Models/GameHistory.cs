using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace CoCaro.Data.Models
{
    [Table("GameHistory")]
    public partial class GameHistory
    {
        [Key]
        public int Id { get; set; }
        public int? GameId { get; set; }
        public int? PlayerId { get; set; }
        public int? Turn { get; set; }
        public int? TimeOfTurn { get; set; }
    }
}
