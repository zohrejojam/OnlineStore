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
    public class StoreHouseController : ApiController
    {
        private DataBaseContext db = new DataBaseContext();


        /// <summary>
        /// ثبت ورود کالا
        /// </summary>
        /// <param name="storeHouse">اطلاعات مورد برای ثب کالا در انبار</param>
        /// <returns></returns>
        // POST: api/StockRooms
        [ResponseType(typeof(StoreHouse))]
        
        public IHttpActionResult RegistrationMaterialIntoStoreHouse(StoreHouse storeHouse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.StoreHouses.Add(storeHouse);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = storeHouse.StoreHouseId }, storeHouse);
        }



    }
}