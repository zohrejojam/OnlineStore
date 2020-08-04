using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineStoreCore.DataLayer;
using OnlineStoreCore.Models;

namespace OnlineStoreCore.Services
{
    public class MaterialGroupService
    {
        private DataBaseContext DbContext;
        public MaterialGroupService(DataBaseContext context)
        {
            DbContext = context;
        }
       
        public IQueryable<MaterialGroup> GetMaterialGroups()
        {
            return DbContext.MaterialGroups;
        }

        
        public void AddMaterialGroup(MaterialGroup materialGroup)
        {
            try
            {
                DbContext.MaterialGroups.Add(materialGroup);
                DbContext.SaveChanges();
            }
            catch (System.Exception)
            {
                throw new Exception(Messages.ErrorOccured);
            }
        }
    }
}
