using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace CoCaro.Data.Models
{
    public partial class CoCaroContext : DbContext
    {
        public CoCaroContext()
        {
        }

        public CoCaroContext(DbContextOptions<CoCaroContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Board> Boards { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<PlayHistory> PlayHistories { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=103.1.210.65;Initial Catalog=CoCaro;Integrated Security=False;Persist Security Info=False;User ID=testDB;Password=1234Bbbb!!@@");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Message>(entity =>
            {
                entity.HasOne(d => d.Board)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.BoardId)
                    .HasConstraintName("FK_Messages_Board");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Messages_Users");
            });

            modelBuilder.Entity<PlayHistory>(entity =>
            {
                entity.Property(e => e.Result).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Board)
                    .WithMany(p => p.PlayHistories)
                    .HasForeignKey(d => d.BoardId)
                    .HasConstraintName("FK_PlayHistory_Board");

                entity.HasOne(d => d.UserId1Navigation)
                    .WithMany(p => p.PlayHistoryUserId1Navigations)
                    .HasForeignKey(d => d.UserId1)
                    .HasConstraintName("FK_PlayHistory_User1");

                entity.HasOne(d => d.UserId2Navigation)
                    .WithMany(p => p.PlayHistoryUserId2Navigations)
                    .HasForeignKey(d => d.UserId2)
                    .HasConstraintName("FK_PlayHistory_User2");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
