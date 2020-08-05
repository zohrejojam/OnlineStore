using System;
using System.Linq;
using OnlineStoreCore.DataLayer;
using OnlineStoreCore.Models;
using OnlineStoreCore.Resources;

namespace OnlineStoreCore.Services
{
    public class ProductGroupService
    {
        private readonly DataBaseContext DbContext;
        public ProductGroupService(DataBaseContext context)
        {
            DbContext = context;
        }
       
        public IQueryable<ProductGroup> Get()
        {
            return DbContext.ProductGroups;
        }
        
        public void Add(ProductGroup materialGroup)
        {
            try
            {
                DbContext.ProductGroups.Add(materialGroup);
                DbContext.SaveChanges();
            }
            catch (System.Exception)
            {
                throw new Exception(Messages.ErrorOccured);
            }
        }
    }
}
