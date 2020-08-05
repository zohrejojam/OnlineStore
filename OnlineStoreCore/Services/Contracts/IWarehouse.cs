using OnlineStoreCore.Models;
using System.Collections.Generic;

namespace OnlineStoreCore.Services
{
    public interface IWarehouseService
    {
        IList<Warehouse> Get();
        void RegistrationMaterialIntoStoreHouse(Warehouse storeHouse);
    }
}
