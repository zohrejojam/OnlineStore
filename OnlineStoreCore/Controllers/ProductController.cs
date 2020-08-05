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
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }
       
        [HttpPost]
        public void Add([FromBody]Product material)
        {
            _service.Add(material);
        }

        public IList<Product> Get()
        {
            return _service.Get();
        }
    }
}
