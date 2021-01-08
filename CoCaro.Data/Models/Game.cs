using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace CoCaro.Data.Models
{
    [Table("Game")]
    public partial class Game
    {
        [Key]
        public int Id { get; set; }
        public int? UserId1 { get; set; }
        public int? BoardId { get; set; }
        [Column(TypeName = "date")]
        public DateTime? CreateDate { get; set; }
        public int? Result { get; set; }
        public int? CoinBet { get; set; }
        public int? UserId2 { get; set; }

        [ForeignKey(nameof(BoardId))]
        [InverseProperty("Games")]
        public virtual Board Board { get; set; }
        [ForeignKey(nameof(UserId1))]
        [InverseProperty(nameof(User.GameUserId1Navigations))]
        public virtual User UserId1Navigation { get; set; }
        [ForeignKey(nameof(UserId2))]
        [InverseProperty(nameof(User.GameUserId2Navigations))]
        public virtual User UserId2Navigation { get; set; }
    }
}
