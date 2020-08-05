using OnlineStoreCore.DataLayer;
using OnlineStoreCore.Models;
using OnlineStoreCore.Resources;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineStoreCore.Services
{
    public class WarehouseService
    {
        private readonly DataBaseContext DbContext;
        public WarehouseService(DataBaseContext context)
        {
            DbContext = context;
        }

        public IList<Warehouse> Get()
        {
            return DbContext.Warehouses.ToList();
        }

        public void RegistrationMaterialIntoStoreHouse(Warehouse storeHouse)
        {
            try
            {
                DbContext.Warehouses.Add(storeHouse);
                DbContext.SaveChanges();
            }
            catch (Exception)
            {
                throw new Exception(Messages.ErrorOccured);
            }
        }
    }
}
