using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using NaviServer.Code;
using NaviServer.Models;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace NaviServer.Data
{
    public partial class NaviContext : DbContext
    {
        public NaviContext()
        {
        }

        public NaviContext(DbContextOptions<NaviContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Coordinates> Coordinates { get; set; }
        public virtual DbSet<Credentials> Credentials { get; set; }
        public virtual DbSet<Movement> Movement { get; set; }
        public virtual DbSet<Planet> Planet { get; set; }
        public virtual DbSet<Player> Player { get; set; }
        public virtual DbSet<Ship> Ship { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLazyLoadingProxies()
                              .UseMySql(Util.DBConnectionString(), x => x.ServerVersion("10.6.5-mariadb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coordinates>(entity =>
            {
                entity.ToTable("coordinates");

                entity.HasIndex(e => e.CoordinatesId)
                    .HasName("coordinates_id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.CoordinatesId).HasColumnName("coordinates_id");

                entity.Property(e => e.PosX).HasColumnName("pos_x");

                entity.Property(e => e.PosY).HasColumnName("pos_y");
            });

            modelBuilder.Entity<Credentials>(entity =>
            {
                entity.HasKey(e => e.PlayerId)
                    .HasName("PRIMARY");

                entity.ToTable("credentials");

                entity.HasIndex(e => e.PlayerId)
                    .HasName("player_id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Username)
                    .HasName("username_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.PlayerId).HasColumnName("player_id");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasColumnType("varchar(64)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.HasOne(d => d.Player)
                    .WithOne(p => p.Credentials)
                    .HasForeignKey<Credentials>(d => d.PlayerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_credentials_player");
            });

            modelBuilder.Entity<Movement>(entity =>
            {
                entity.ToTable("movement");

                entity.HasIndex(e => e.CoordinatesFromId)
                    .HasName("fk_movement_coordinates_from_idx");

                entity.HasIndex(e => e.CoordinatesToId)
                    .HasName("fk_movement_coodinates_to_idx");

                entity.HasIndex(e => e.MovementId)
                    .HasName("movement_id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.ShipId)
                    .HasName("ship_id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.MovementId).HasColumnName("movement_id");

                entity.Property(e => e.CoordinatesFromId).HasColumnName("coordinates_from_id");

                entity.Property(e => e.CoordinatesToId).HasColumnName("coordinates_to_id");

                entity.Property(e => e.ShipId).HasColumnName("ship_id");

                entity.Property(e => e.Progress).HasColumnName("progress");

                entity.HasOne(d => d.CoordinatesFrom)
                    .WithMany(p => p.MovementCoordinatesFrom)
                    .HasForeignKey(d => d.CoordinatesFromId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_movement_coordinates_from");

                entity.HasOne(d => d.CoordinatesTo)
                    .WithMany(p => p.MovementCoordinatesTo)
                    .HasForeignKey(d => d.CoordinatesToId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_movement_coodinates_to");

                entity.HasOne(d => d.Ship)
                    .WithOne(p => p.Movement)
                    .HasForeignKey<Movement>(d => d.ShipId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_movement_ship");
            });

            modelBuilder.Entity<Planet>(entity =>
            {
                entity.ToTable("planet");

                entity.HasIndex(e => e.CoordinatesId)
                    .HasName("coordinates_id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.PlanetId)
                    .HasName("planet_id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.PlanetId).HasColumnName("planet_id");

                entity.Property(e => e.CoordinatesId).HasColumnName("coordinates_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.HasOne(d => d.Coordinates)
                    .WithOne(p => p.Planet)
                    .HasForeignKey<Planet>(d => d.CoordinatesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_planet_coordinates");
            });

            modelBuilder.Entity<Player>(entity =>
            {
                entity.ToTable("player");

                entity.HasIndex(e => e.PlayerId)
                    .HasName("player_id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.ShipId)
                    .HasName("ship_id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.PlayerId).HasColumnName("player_id");

                entity.Property(e => e.IsAdmin)
                    .HasColumnName("is_admin")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.ShipId).HasColumnName("ship_id");

                entity.HasOne(d => d.Ship)
                    .WithOne(p => p.Player)
                    .HasForeignKey<Player>(d => d.ShipId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_player_ship");
            });

            modelBuilder.Entity<Ship>(entity =>
            {
                entity.ToTable("ship");

                entity.HasIndex(e => e.CoordinatesId)
                    .HasName("fk_ship_coordinates_idx");

                entity.HasIndex(e => e.ShipId)
                    .HasName("ship_id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.ShipId).HasColumnName("ship_id");

                entity.Property(e => e.CoordinatesId).HasColumnName("coordinates_id");

                entity.HasOne(d => d.Coordinates)
                    .WithMany(p => p.Ship)
                    .HasForeignKey(d => d.CoordinatesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_ship_coordinates");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
