using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using OnlineStore.Models;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace OnlineStore.DataLayer
{
    public class DataBaseContext:DbContext
    {
         public DataBaseContext() : base("name=OnlineStoreContext")
        {
        }

        //static DataBaseContext()
        //{
        //    Database.SetInitializer(new DropCreateDatabaseIfModelChanges<DataBaseContext>());
        //}


        public DbSet<Material> Materials { get; set; }
        public DbSet<MaterialGroup> MaterialGroups { get; set; }
        public DbSet<AccountingDocument> AccountingDocuments { get; set; }
        public DbSet<SalesInvoice> SalesInvoices { get; set; }
        public DbSet<StoreHouse> StoreHouses { get; set; }
        
    }
}