using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStoreCore.Models;

namespace OnlineStoreCore.DataLayer.Mappings
{
    public class SalesInvoiceMap : IEntityTypeConfiguration<SalesInvoice>
    {
        public void Configure(EntityTypeBuilder<SalesInvoice> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Count)
                .IsRequired();

            builder.Property(p => p.Date)
                .IsRequired();

            builder.Property(p => p.OutputInvoiceNumber)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.CustomerName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Amount)
               .IsRequired();

            builder.HasOne(p => p.Warehouse)
                .WithMany(p => p.SalesInvoices)
                .HasForeignKey(p => p.WarehouseId);
        }
    }
}
