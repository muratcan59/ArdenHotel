using ArdenHotel.Entities;
using ArdenHotel.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArdenHotel.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Category>().Property(e => e.CategoryDescription).HasMaxLength(250);

            builder.Entity<Comment>().ToTable("Comment");
            builder.Entity<Comment>().HasKey(e => e.CommentID);
            builder.Entity<Comment>().HasIndex(e => new { e.CommentID, e.Email }).IsUnique();

            builder.Entity<CommentLikeStatus>().ToTable("CommentLikeStatus");
            builder.Entity<CommentLikeStatus>().HasKey(e => e.CommentLikeStatusID);

            builder.Entity<Room>().ToTable("Room");
            builder.Entity<Room>().HasKey(e => e.RoomID);

            builder.Entity<Media>(entity =>
            {
                entity.HasKey(c => c.MediaID);
            });
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<CommentLikeStatus> CommentLikeStatuses { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Media> Medias { get; set; }

    }
}
