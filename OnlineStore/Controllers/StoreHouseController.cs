using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using OnlineStore.DataLayer;
using OnlineStore.Models;

namespace OnlineStore.Controllers
{
    public class StoreHouseController : ApiController
    {
        private DataBaseContext db = new DataBaseContext();


        // GET: api/StoreHouse
        public IQueryable<StoreHouse> GetStoreHouses()
        {
            return db.StoreHouses;
        }

        /// <summary>
        /// ثبت ورود کالا
        /// </summary>
        /// <param name="materialId">آی دی کالا که از کومبو کالا دریافت میشود</param>
        /// <param name="number">تعداد</param>
        /// <param name="date">تاریخ</param>
        /// <param name="inputInvoiceNumber">شماره فاکتور</param>
        /// <returns></returns>
        // POST: api/StoreHouse
        [ResponseType(typeof(StoreHouse))]
        public IHttpActionResult RegistrationMaterialIntoStoreHouse(StoreHouse storeHouse)
        {
            try
            {
                db.StoreHouses.Add(storeHouse);
                db.SaveChanges();

                return CreatedAtRoute("DefaultApi", new { id = storeHouse.StoreHouseId }, storeHouse);
            }
            catch (Exception ee)
            {
                return BadRequest(ModelState);
            }
          
        }

       


    }
}