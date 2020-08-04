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
    [Route("api/Material")]
    [ApiController]
    public class MaterialController : ControllerBase
    {
        private MaterialService _service;
        public MaterialController(MaterialService service)
        {
            _service = service;
        }
        /// <summary>
        /// تعریف کالا
        /// </summary>
        /// <param name="material">کالا</param>
        /// <returns></returns>
        // POST: api/Materials
        [HttpPost]
        public void DefinitionMaterial([FromBody]Material material)
        {
            _service.DefinitionMaterial(material);
        }


        // GET: api/Materials
        public IQueryable<Material> GetMaterials()
        {
            return _service.GetMaterials();
        }
    }
}
