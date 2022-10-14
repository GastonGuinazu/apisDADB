using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace API.Data
{
    public partial class tpi_dabdContext : DbContext
    {
        public tpi_dabdContext()
        {
        }

        public tpi_dabdContext(DbContextOptions<tpi_dabdContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Carta> Cartas { get; set; } = null!;
        public virtual DbSet<CartasCroupier> CartasCroupiers { get; set; } = null!;
        public virtual DbSet<CartasJugada> CartasJugadas { get; set; } = null!;
        public virtual DbSet<CartasJugador> CartasJugadors { get; set; } = null!;
        public virtual DbSet<CartasSinJugar> CartasSinJugars { get; set; } = null!;
        public virtual DbSet<Sesione> Sesiones { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;user=root;database=tpi_dabd;password=gasti123", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.30-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Carta>(entity =>
            {
                entity.ToTable("cartas");

                entity.Property(e => e.Id)
                    .HasMaxLength(5)
                    .HasColumnName("id");

                entity.Property(e => e.Carta1)
                    .HasMaxLength(25)
                    .HasColumnName("carta");

                entity.Property(e => e.Valor).HasColumnName("valor");
            });

            modelBuilder.Entity<CartasCroupier>(entity =>
            {
                entity.HasKey(e => e.CodCroupier)
                    .HasName("PRIMARY");

                entity.ToTable("cartas_croupier");

                entity.HasIndex(e => e.IdCarta, "idCarta");

                entity.HasIndex(e => e.IdUsuario, "idUsuario");

                entity.Property(e => e.CodCroupier).HasColumnName("codCroupier");

                entity.Property(e => e.IdCarta)
                    .HasMaxLength(5)
                    .HasColumnName("idCarta");

                entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");

                entity.HasOne(d => d.IdCartaNavigation)
                    .WithMany(p => p.CartasCroupiers)
                    .HasForeignKey(d => d.IdCarta)
                    .HasConstraintName("cartas_croupier_ibfk_2");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.CartasCroupiers)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("cartas_croupier_ibfk_1");
            });

            modelBuilder.Entity<CartasJugada>(entity =>
            {
                entity.HasKey(e => e.CodJugadas)
                    .HasName("PRIMARY");

                entity.ToTable("cartas_jugadas");

                entity.HasIndex(e => e.IdCarta, "idCarta");

                entity.HasIndex(e => e.IdUsuario, "idUsuario");

                entity.Property(e => e.CodJugadas).HasColumnName("codJugadas");

                entity.Property(e => e.IdCarta)
                    .HasMaxLength(5)
                    .HasColumnName("idCarta");

                entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");

                entity.HasOne(d => d.IdCartaNavigation)
                    .WithMany(p => p.CartasJugada)
                    .HasForeignKey(d => d.IdCarta)
                    .HasConstraintName("cartas_jugadas_ibfk_2");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.CartasJugada)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("cartas_jugadas_ibfk_1");
            });

            modelBuilder.Entity<CartasJugador>(entity =>
            {
                entity.HasKey(e => e.CodJugador)
                    .HasName("PRIMARY");

                entity.ToTable("cartas_jugador");

                entity.HasIndex(e => e.IdCarta, "idCarta");

                entity.HasIndex(e => e.IdUsuario, "idUsuario");

                entity.Property(e => e.CodJugador).HasColumnName("codJugador");

                entity.Property(e => e.IdCarta)
                    .HasMaxLength(5)
                    .HasColumnName("idCarta");

                entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");

                entity.HasOne(d => d.IdCartaNavigation)
                    .WithMany(p => p.CartasJugadors)
                    .HasForeignKey(d => d.IdCarta)
                    .HasConstraintName("cartas_jugador_ibfk_2");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.CartasJugadors)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("cartas_jugador_ibfk_1");
            });

            modelBuilder.Entity<CartasSinJugar>(entity =>
            {
                entity.HasKey(e => e.CodSinJugar)
                    .HasName("PRIMARY");

                entity.ToTable("cartas_sin_jugar");

                entity.HasIndex(e => e.IdCarta, "idCarta");

                entity.HasIndex(e => e.IdUsuario, "idUsuario");

                entity.Property(e => e.CodSinJugar).HasColumnName("codSinJugar");

                entity.Property(e => e.IdCarta)
                    .HasMaxLength(5)
                    .HasColumnName("idCarta");

                entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");

                entity.HasOne(d => d.IdCartaNavigation)
                    .WithMany(p => p.CartasSinJugars)
                    .HasForeignKey(d => d.IdCarta)
                    .HasConstraintName("cartas_sin_jugar_ibfk_2");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.CartasSinJugars)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("cartas_sin_jugar_ibfk_1");
            });

            modelBuilder.Entity<Sesione>(entity =>
            {
                entity.HasKey(e => e.IdSesion)
                    .HasName("PRIMARY");

                entity.ToTable("sesiones");

                entity.HasIndex(e => e.IdUsuario, "idUsuario");

                entity.Property(e => e.IdSesion).HasColumnName("idSesion");

                entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Sesiones)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("sesiones_ibfk_1");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PRIMARY");

                entity.ToTable("usuarios");

                entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");

                entity.Property(e => e.Pass)
                    .HasMaxLength(50)
                    .HasColumnName("pass");

                entity.Property(e => e.Usuario1)
                    .HasMaxLength(50)
                    .HasColumnName("usuario");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
