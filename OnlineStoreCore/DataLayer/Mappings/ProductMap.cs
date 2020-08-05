using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStoreCore.Models;

namespace OnlineStoreCore.DataLayer.Mappings
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.Code)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(o => o.Title)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(o => o.MinimumInventory)
                   .IsRequired();

            builder.HasOne<ProductGroup>(s => s.Group)
                   .WithMany(g => g.Products)
                   .HasForeignKey(s => s.GroupId);

            builder.HasOne<Warehouse>(p => p.Warehouse)
                .WithMany(p => p.Products)
                .HasForeignKey(p => p.WareHouseId);
        }
    }
}
