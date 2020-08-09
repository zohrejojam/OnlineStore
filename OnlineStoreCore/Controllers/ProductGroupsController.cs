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
        private readonly IProductGroupService _productGroupService;

        public ProductGroupsController(IProductGroupService productGroupService)
        {
            _productGroupService = productGroupService;
        }
        public IList<ProductGroup> Get()
        {
            return _productGroupService.Get();
        }

        public void Add(ProductGroup materialGroup)
        {
            _productGroupService.Add(materialGroup);
        }
    }
}