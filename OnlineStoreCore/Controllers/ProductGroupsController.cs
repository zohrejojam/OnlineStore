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
    [Route("api/MaterialGroups")]
    [ApiController]
    public class ProductGroupsController : ControllerBase
    {
        private readonly ProductGroupService _service;

        public ProductGroupsController(ProductGroupService service)
        {
            _service = service;
        }
        public IQueryable<ProductGroup> Get()
        {
            return _service.Get();
        }

        public void Add(ProductGroup materialGroup)
        {
            _service.Add(materialGroup);
        }
    }
}