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
    public class MaterialGroupsController : ControllerBase
    {
        private MaterialGroupService _service;
        public MaterialGroupsController(MaterialGroupService service)
        {
            _service = service;
        }
        public IQueryable<MaterialGroup> GetMaterialGroups()
        {
            return _service.GetMaterialGroups();
        }

        public void AddMaterialGroup(MaterialGroup materialGroup)
        {
            _service.AddMaterialGroup(materialGroup);
        }
    }
}