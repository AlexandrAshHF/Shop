using CursVN.Persistance.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CursVN.Persistance.Configurations
{
    internal class ParameterConfiguration : IEntityTypeConfiguration<ParameterEntity>
    {
        public void Configure(EntityTypeBuilder<ParameterEntity> builder)
        {
            builder.ToTable("Parameters")
                .HasKey(x => x.Id);
        }
    }
}
