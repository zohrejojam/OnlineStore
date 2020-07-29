using System.Linq;
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

      
        // POST: api/MaterialGroups
        [ResponseType(typeof(MaterialGroup))]
        public IHttpActionResult AddMaterialGroup(MaterialGroup materialGroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MaterialGroups.Add(materialGroup);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = materialGroup.MaterialGroupId }, materialGroup);
        }

     

    }
}