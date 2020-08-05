using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineStoreCore.Models;
using OnlineStoreCore.Services;

namespace OnlineStoreCore.Controllers
{
    [Route("api/StoreHouse")]
    [ApiController]
    public class WareHouseController : ControllerBase
    {
        private readonly WarehouseService _service;

        public WareHouseController(WarehouseService service)
        {
            _service = service;
        }

        public IQueryable<Warehouse> Get()
        {
            return _service.Get();
        }

        public void RegistrationMaterialIntoStoreHouse([FromBody]Warehouse storeHouse)
        {
            _service.RegistrationMaterialIntoStoreHouse(storeHouse);
        }
    }
}