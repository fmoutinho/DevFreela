﻿using DevFreela.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence
{
    public class DevFreelaDbContext : DbContext
    {
        public DevFreelaDbContext(DbContextOptions<DevFreelaDbContext> options) : base(options)
        {

        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<UserSkill> UserSkills { get; set; }
        public DbSet<ProjectComment> ProjectComments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Skill>(e =>
            {
                e.HasKey(s => s.Id);
            });

            modelBuilder.Entity<UserSkill>(e =>
            {
                e.HasKey(us => us.Id);

                e.HasOne(u => u.Skill)
                    .WithMany(u => u.UserSkills)
                    .HasForeignKey(s => s.IdSkill)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ProjectComment>(e =>
            {
                e.HasKey(pc => pc.Id);

                e.HasOne( pc => pc.Project)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(pc => pc.IdProject)
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(pc => pc.User)
                    .WithMany(u => u.Comments)
                    .HasForeignKey(pc => pc.IdUser)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<User>(e =>
            {
                e.HasKey(pc => pc.Id);

                e.HasMany(u => u.Skills)
                .WithOne(us => us.User)
                .HasForeignKey(us => us.IdUser)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Project>(e =>
            {
                e.HasKey(p => p.Id);
                e.Property(e => e.TotalCost).HasColumnType("decimal(18,2)");

                e.HasOne(p => p.Freelancer)
                    .WithMany(f => f.FreelanceProjects)
                    .HasForeignKey (p => p.IdFreelancer)
                    .OnDelete (DeleteBehavior.Restrict);

                e.HasOne(p => p.Client)
                    .WithMany(c => c.OwnedProjects)
                    .HasForeignKey(p => p.IdClient)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
