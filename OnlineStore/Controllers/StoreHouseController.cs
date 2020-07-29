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
using System.Web.Mvc;
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
        public IHttpActionResult RegistrationMaterialIntoStoreHouse(int materialId,
                                                                    int number,
                                                                    DateTime date,
                                                                    string inputInvoiceNumber)
        {
            StoreHouse storeHouse = new StoreHouse();
            try
            {
                if (materialId == 0 || number==0 || date==DateTime.MinValue || string.IsNullOrEmpty(inputInvoiceNumber))
                    return BadRequest(ModelState);

                storeHouse.MaterialId = materialId;
                storeHouse.Number = number;
                storeHouse.Date = date;
                storeHouse.InputInvoiceNumber = inputInvoiceNumber;

                db.StoreHouses.Add(storeHouse);
                db.SaveChanges();

                return CreatedAtRoute("DefaultApi", new { id = storeHouse.StoreHouseId }, storeHouse);
            }
            catch (Exception)
            {
                return BadRequest(ModelState);
            }
          
        }

        /// <summary>
        /// فهرست موجودی انبار
        /// </summary>
        /// <param name="page">صفحه ی چند</param>
        /// <param name="count">چه تعداد رکورد در یک صفحه</param>
        /// <param name="filterStr">رشته برای فیلتر</param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public IQueryable<ListOfMaterial> StoreHouseInventory(int? page,int? count,string filterStr)
        {
            var takePage = page ?? 1;
            var takeCount = count ?? 5;
            filterStr = filterStr.ToLower().Trim();
            var query = db.StoreHouses.Include(p => p.Material).Include(p => p.Material.MaterialGroup);
            if (filterStr != "" && filterStr != null)
            {
                query = query.Where(p => p.Material.MaterialCode.ToLower().Contains(filterStr) ||
                                     p.Material.MaterialTitle.ToLower().Contains(filterStr) ||
                                     p.Material.MaterialGroup.MaterialGroupName.ToLower().Contains(filterStr) ||
                                     p.Material.MinInventory.ToString() == filterStr ||
                                     p.Number.ToString() == filterStr);
            }

            return query
                .Select(p => new ListOfMaterial
                {
                    MaterialCode = p.Material.MaterialCode,
                    MaterialTitle = p.Material.MaterialTitle,
                    MaterialGroupName = p.Material.MaterialGroup.MaterialGroupName,
                    MinInventory = p.Material.MinInventory,
                    Number = p.Number,
                    Status = p.Number == 0 ? "ناموجود" : (p.Number <= p.Material.MinInventory ? "آماده سفارش" : "عادی")

                })
                //For Paging
                .Skip((takePage - 1) * takeCount)
                  .Take(takeCount);
        }


    }
}