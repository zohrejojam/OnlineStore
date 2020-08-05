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
        private readonly IWarehouseService _service;

        public WarehouseController(IWarehouseService service)
        {
            _service = service;
        }

        public IList<Warehouse> Get()
        {
            return _service.Get();
        }

        public void RegistrationMaterialIntoStoreHouse([FromBody]Warehouse storeHouse)
        {
            _service.RegistrationMaterialIntoStoreHouse(storeHouse);
        }
    }
}