using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using OnlineStore.DataLayer;
using OnlineStore.Models;

namespace OnlineStore.Controllers
{
    public class MaterialGroupsController : ApiController
    {
        private DataBaseContext db = new DataBaseContext();

        // GET: api/MaterialGroups
        public IQueryable<MaterialGroup> GetMaterialGroups()
        {
            return db.MaterialGroups;
        }

     
    }
}