using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using OnlineStoreCore.Models;
using OnlineStoreCore.Services;

namespace OnlineStoreCore.Controllers
{
    [Route("api/MaterialGroups")]
    [ApiController]
    public class ProductGroupsController : ControllerBase
    {
        private readonly IProductGroupService _service;

        public ProductGroupsController(IProductGroupService service)
        {
            _service = service;
        }
        public IList<ProductGroup> Get()
        {
            return _service.Get();
        }

        public void Add(ProductGroup materialGroup)
        {
            _service.Add(materialGroup);
        }
    }
}