using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ProjectManager.WebAPI.Models;

namespace ProjectManager.WebAPI.Data;

public partial class ProjectManagerDbContext : DbContext
{
    public ProjectManagerDbContext()
    {
    }

    public ProjectManagerDbContext(DbContextOptions<ProjectManagerDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CompletedProject> CompletedProjects { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<ProjectsUsersView> ProjectsUsersViews { get; set; }

    public virtual DbSet<Role?> Roles { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserView> UserViews { get; set; }

    public virtual Task<int> UpdateRoleAsync(int idRole, string name)
    {
        return Database.ExecuteSqlInterpolatedAsync($"EXEC UpdateRole {idRole}, {name}");
    }
    public virtual Task<int> UpdateStatusAsync(int idRole, string title)
    {
        return Database.ExecuteSqlInterpolatedAsync($"EXEC UpdateStatus {idRole}, {title}");
    }
    public virtual async Task<List<Project>> GetProjectsByStatus(int idStatus)
    {
        return await Projects.FromSqlInterpolated($"SELECT * FROM GetProjectsByStatus({idStatus})")
            .ToListAsync();
    }

    public virtual async Task<Role?> GetRoleById(int idRole)
    {
        return await Roles.FromSqlInterpolated($"SELECT * FROM GetRoleById({idRole})")
            .FirstOrDefaultAsync();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ProjectManager");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CompletedProject>(entity =>
        {
            entity.HasKey(e => e.IdCompletedProject).HasName("PK_HistoryProject");

            entity.ToTable("CompletedProject", tb => tb.HasTrigger("trg_HistoryProject_Insert"));

            entity.Property(e => e.CompletionDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date");
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.Title).HasMaxLength(50);
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.IdProject);

            entity.ToTable("Project", tb => tb.HasTrigger("trg_MoveProjectToHistory"));

            entity.Property(e => e.AddDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DeadlineDate).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.IdStatus).HasDefaultValueSql("((1))");
            entity.Property(e => e.Title).HasMaxLength(50);

            entity.HasOne(d => d.IdStatusNavigation).WithMany(p => p.Projects)
                .HasForeignKey(d => d.IdStatus)
                .HasConstraintName("FK_Project_Status");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Projects)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Project_User");
        });

        modelBuilder.Entity<ProjectsUsersView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ProjectsUsersView");

            entity.Property(e => e.Login).HasMaxLength(20);
            entity.Property(e => e.Title).HasMaxLength(50);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRole);

            entity.ToTable("Role");

            entity.Property(e => e.IdRole).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(30);
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.IdStatus);

            entity.ToTable("Status");

            entity.Property(e => e.IdStatus).ValueGeneratedNever();
            entity.Property(e => e.Title).HasMaxLength(30);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser);

            entity.ToTable("User");

            entity.HasIndex(e => e.Login, "UQ_User").IsUnique();

            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Login).HasMaxLength(20);
            entity.Property(e => e.Password).HasMaxLength(64);

            entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdRole)
                .HasConstraintName("FK_User_Role");
        });

        modelBuilder.Entity<UserView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("UserView");

            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Login).HasMaxLength(20);
            entity.Property(e => e.Post).HasMaxLength(30);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
