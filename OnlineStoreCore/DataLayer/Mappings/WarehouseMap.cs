using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStoreCore.Models;

namespace OnlineStoreCore.DataLayer.Mappings
{
    public class WarehouseMap : IEntityTypeConfiguration<Warehouse>
    {
        public void Configure(EntityTypeBuilder<Warehouse> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Count)
                .IsRequired();

            builder.Property(p => p.InputInvoiceNumber)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Date)
                .IsRequired()
                .HasColumnType("DateTime");
        }
    }
}
