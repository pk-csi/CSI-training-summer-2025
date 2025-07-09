using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movies.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Infrastructure.Persistance.Configurations
{
    public class ActorConfiguration : IEntityTypeConfiguration<Actor>
    {
        public void Configure(EntityTypeBuilder<Actor> builder)
        {
            builder.ToTable("Actors");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(100);
            builder.HasMany(x => x.MovieActors)
                .WithOne(x => x.Actor)
                .HasForeignKey(x => x.ActorId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
