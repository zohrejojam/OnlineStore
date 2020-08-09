using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using OnlineStoreCore.Models;
using OnlineStoreCore.Services;

namespace OnlineStoreCore.Controllers
{
    [Route("api/StoreHouse")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private readonly IWarehouseService _warehouseService;

        public WarehouseController(IWarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
        }

        public IList<Warehouse> Get()
        {
            return _warehouseService.Get();
        }

        public void RegistrationMaterialIntoStoreHouse(Warehouse storeHouse)
        {
            _warehouseService.RegistrationMaterialIntoStoreHouse(storeHouse);
        }
    }
}