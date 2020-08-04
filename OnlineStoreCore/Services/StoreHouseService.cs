using OnlineStoreCore.DataLayer;
using OnlineStoreCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStoreCore.Services
{
    public class StoreHouseService
    {
        private DataBaseContext DbContext;
        public StoreHouseService(DataBaseContext context)
        {
            DbContext = context;
        }

        public IQueryable<StoreHouse> GetStoreHouses()
        {
            return DbContext.StoreHouses;
        }

        /// <summary>
        /// ثبت ورود کالا
        /// </summary>
        /// <param name="materialId">آی دی کالا که از کومبو کالا دریافت میشود</param>
        /// <param name="number">تعداد</param>
        /// <param name="date">تاریخ</param>
        /// <param name="inputInvoiceNumber">شماره فاکتور</param>
        /// <returns></returns>
        
        public void RegistrationMaterialIntoStoreHouse(StoreHouse storeHouse)
        {
            try
            {
                DbContext.StoreHouses.Add(storeHouse);
                DbContext.SaveChanges();

            }
            catch (Exception ee)
            {
                throw new Exception(Messages.ErrorOccured);
            }

        }

    }
}
