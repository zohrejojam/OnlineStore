using Microsoft.EntityFrameworkCore;
using OnlineStoreCore.DataLayer.Mappings;
using OnlineStoreCore.Models;

namespace OnlineStoreCore.DataLayer
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductGroup> ProductGroups { get; set; }
        public DbSet<AccountingDocument> AccountingDocuments { get; set; }
        public DbSet<SalesInvoice> SalesInvoices { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductGroupMap());
            modelBuilder.ApplyConfiguration(new ProductMap());
            modelBuilder.ApplyConfiguration(new AccountingDocumentMap());
            modelBuilder.ApplyConfiguration(new SalesInvoiceMap());
            modelBuilder.ApplyConfiguration(new ProductMap());
        }
    }
}