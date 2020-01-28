using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TicketBookingApp.Models
{
    public partial class dbInterviewContext : DbContext
    {
        public dbInterviewContext()
        {
        }

        public dbInterviewContext(DbContextOptions<dbInterviewContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Buyer> Buyer { get; set; }
        public virtual DbSet<Event> Event { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=dbinterview9.database.windows.net;Database=dbInterview;User Id=dbinterview9user;password=Hxes-Ku2Eqc6)x8u;Trusted_Connection=False;MultipleActiveResultSets=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Buyer>(entity =>
            {
                entity.Property(e => e.BuyerName)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.TesterKey)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.Buyer)
                    .HasForeignKey(d => d.EventId)
                    .HasConstraintName("FK_Guess_Contest");
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.TimeoutInSeconds).HasDefaultValueSql("((300))");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
