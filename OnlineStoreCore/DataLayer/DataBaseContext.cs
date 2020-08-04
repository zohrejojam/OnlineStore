using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.EntityFrameworkCore;
using OnlineStoreCore.Models;

namespace OnlineStoreCore.DataLayer
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options)
       : base(options)
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