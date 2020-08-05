using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStoreCore.Models;

namespace OnlineStoreCore.DataLayer.Mappings
{
    public class AccountingDocumentMap : IEntityTypeConfiguration<AccountingDocument>
    {
        public void Configure(EntityTypeBuilder<AccountingDocument> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Number)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Date)
                .IsRequired()
                .HasColumnType("DateType");

            builder.Property(p => p.Amount)
                .IsRequired()
                .HasColumnType("decimal(5,2)");

            builder.HasOne<SalesInvoice>(p => p.SalesInvoice)
                .WithMany(p => p.AccountingDocuments)
                .HasForeignKey(p => p.SalesInvoiceId);


        }
    }
}
