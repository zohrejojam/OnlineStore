using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using OnlineStoreCore.IServices;
using OnlineStoreCore.Models;

namespace OnlineStoreCore.Controllers
{
    [Route("api/Material")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
       
        [HttpPost]
        public void Add([FromBody]Product material)
        {
            _productService.Add(material);
        }

        public IList<Product> Get()
        {
            return _productService.Get();
        }
    }
}
