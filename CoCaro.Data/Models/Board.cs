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
            Games = new HashSet<Game>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        public int? Status { get; set; }
        [StringLength(10)]
        public string Password { get; set; }
        public int? TimeOfTurn { get; set; }
        public int? Owner { get; set; }

        [InverseProperty(nameof(Game.Board))]
        public virtual ICollection<Game> Games { get; set; }
    }
}
