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
using System.Transactions;

namespace OnlineStore.Controllers
{
    public class SalesInvoicesController : ApiController
    {
        private DataBaseContext db = new DataBaseContext();
        /// <summary>
        /// فروش کالا و ثبت فاکتور فروش
        /// </summary>
        /// <param name="salesInvoice"></param>
        /// <returns></returns>
        // POST: api/SalesInvoices
        [ResponseType(typeof(SalesInvoice))]
        public IHttpActionResult CreateSalesInvoice(SalesInvoice salesInvoice)
        {
            using (TransactionScope transScope = new TransactionScope())
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                try
                {
                    //تعداد کالا بیشتر از حداقل موجودی نباشد
                    if (salesInvoice.Number <= 0 ||
                        salesInvoice.Number > salesInvoice.StoreHouse.Material.MinInventory)
                        return BadRequest(Messages.MaterialNumberMustBeMoreThanMinInventory);


                    db.SalesInvoices.Add(salesInvoice);
                    db.SaveChanges();

                    #region Decrease Material Number
                    //کسر کردن تعداد کالای فروخته شده از انبار
                    bool decreeseSuccessed = this.DecreaseMaterialCount(salesInvoice.StoreHouseId, salesInvoice.Number);
                    if (!decreeseSuccessed)
                    {
                        transScope.Dispose();
                        return BadRequest(Messages.ErrorOccured);
                    }
                    #endregion

                    #region Save Accounting Document Automatically
                    //ثبت سند حسابداری
                    bool createDocumentSeccessed = this.RegitrationAccountingDocument(salesInvoice.Date,
                        salesInvoice.Amount, salesInvoice.Number, salesInvoice.SalesInvoiceId);

                    if (!createDocumentSeccessed)
                    {
                        transScope.Dispose();
                        return BadRequest(Messages.ErrorOccured);
                    }
                    #endregion

                    transScope.Complete();
                    return CreatedAtRoute("DefaultApi", new { id = salesInvoice.SalesInvoiceId }, salesInvoice);
                }
                catch (TransactionException e)
                {
                    transScope.Dispose();
                    return BadRequest(Messages.ErrorOccured);

                }
            }
        }

        /// <summary>
        /// گرفتن اطلاعات فروش
        /// </summary>
        /// <returns></returns>
        // GET: api/SalesInvoices
        public IQueryable<SalesInvoice> GetSalesInvoices()
        {
            return db.SalesInvoices;
        }

        /// <summary>
        /// کسر کردن تعداد کالای انبار هنگام فروش
        /// </summary>
        /// <param name="storeHouseId">آی دی انبار</param>
        /// <param name="salesInvoiceNumber">تعداد کالای فروش رفته</param>
        /// <returns></returns>
        public bool DecreaseMaterialCount(int storeHouseId, int salesInvoiceNumber)
        {
            try
            {
                StoreHouse currentStoreHouse = db.StoreHouses.Find(storeHouseId);
                if (currentStoreHouse.Number > 0)
                {
                    currentStoreHouse.Number -= salesInvoiceNumber;
                    db.Entry(currentStoreHouse).State = EntityState.Modified;
                }
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// ثبت سند حسابداری
        /// </summary>
        /// <param name="date">تاریخ سند</param>
        /// <param name="salesInvoiceAmount">مبلغ یک کالا</param>
        /// <param name="salesInvoiceNumber">تعداد کالا</param>
        /// <param name="salesInvoiceId">شماره فاکتور فروش</param>
        /// <returns></returns>
        public bool RegitrationAccountingDocument(DateTime date, decimal salesInvoiceAmount, int salesInvoiceNumber, int salesInvoiceId)
        {
            try
            {
                AccountingDocument obj = new AccountingDocument();
                obj.DocumentDate = date;
                obj.DocumentNumber = obj.CreateDocumentNumber();//شماره سند
                obj.Amount = salesInvoiceAmount * salesInvoiceNumber;
                obj.SalesInvoiceId = salesInvoiceId;
                
                db.AccountingDocuments.Add(obj);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}