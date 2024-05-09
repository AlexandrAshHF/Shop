using CursVN.Persistance.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CursVN.Persistance.Configurations
{
    internal class OrderProductConfiguration : IEntityTypeConfiguration<OrderProduct>
    {
        public void Configure(EntityTypeBuilder<OrderProduct> builder)
        {
            builder.ToTable("OrderProduct")
                .HasKey(x => new { x.OrderId, x.ProductId });

            builder.HasOne(x => x.Order)
                .WithMany(x => x.ProductsOrders)
                .HasForeignKey(x => x.OrderId);

            builder.HasOne(x => x.Product)
                .WithMany(x => x.ProductsOrders)
                .HasForeignKey(x => x.ProductId);
        }
    }
}
