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
    public class StoreHouseController : ControllerBase
    {
        private StoreHouseService _service;
        public StoreHouseController(StoreHouseService service)
        {
            _service = service;
        }

        public IQueryable<StoreHouse> GetStoreHouses()
        {
            return _service.GetStoreHouses();
        }

        public void RegistrationMaterialIntoStoreHouse([FromBody]StoreHouse storeHouse)
        {
            _service.RegistrationMaterialIntoStoreHouse(storeHouse);
        }
    }
}