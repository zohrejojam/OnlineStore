using System;
using System.Collections.Generic;
using System.Linq;
using OnlineStoreCore.DataLayer;
using OnlineStoreCore.Models;
using OnlineStoreCore.Resources;

namespace OnlineStoreCore.Services
{
    public class ProductGroupService:IProductGroupService
    {
        private readonly DataBaseContext DbContext;
        public ProductGroupService(DataBaseContext context)
        {
            DbContext = context;
        }
       
        public IList<ProductGroup> Get()
        {
            return DbContext.ProductGroups.ToList();
        }
        
        public void Add(ProductGroup productGroup)
        {
            try
            {
                DbContext.ProductGroups.Add(productGroup);
                DbContext.SaveChanges();
            }
            catch (System.Exception)
            {
                throw new Exception(Messages.ErrorOccured);
            }
        }
    }
}
