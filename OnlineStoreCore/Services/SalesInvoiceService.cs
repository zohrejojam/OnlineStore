using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.EntityFrameworkCore;
using OnlineStoreCore.DataLayer;
using OnlineStoreCore.Models;

namespace OnlineStoreCore.Services
{
    public class SalesInvoiceService
    {
        private DataBaseContext DbContext;
        private AccountingDocumentService _documentService;
        public SalesInvoiceService(DataBaseContext context,AccountingDocumentService documentService)
        {
            DbContext = context;
            _documentService = documentService;
        }

        /// <summary>
        /// فروش کالا و ثبت فاکتور فروش
        /// </summary>
        /// <param name="salesInvoice"></param>
        /// <returns></returns>
       
        public void CreateSalesInvoice(SalesInvoice salesInvoice)
        {
            using (TransactionScope transScope = new TransactionScope())
            {
                try
                {
                    int minInventory = 0;
                    //بدست آوردن حداقل موجودی برای این کالا
                    StoreHouse currentStoreHouse = DbContext.StoreHouses.FirstOrDefault(p => p.StoreHouseId == salesInvoice.StoreHouseId);
                    if (currentStoreHouse != null)
                    {
                        Material currentMaterial = DbContext.Materials.FirstOrDefault(p => p.MaterialId == currentStoreHouse.MaterialId);
                        minInventory = currentMaterial != null ? currentMaterial.MinInventory : 0;
                    }

                    //تعداد کالا بیشتر از حداقل موجودی نباشد
                    if (salesInvoice.Count <= 0 ||
                        salesInvoice.Count > minInventory)
                        throw new Exception(Messages.MaterialNumberMustBeMoreThanMinInventory);


                    DbContext.SalesInvoices.Add(salesInvoice);
                    DbContext.SaveChanges();

                    #region Decrease Material Number
                    //کسر کردن تعداد کالای فروخته شده از انبار
                    bool decreeseSuccessed = this.DecreaseMaterialCount(salesInvoice.StoreHouseId, salesInvoice.Count);
                    if (!decreeseSuccessed)
                    {
                        transScope.Dispose();
                        throw new Exception(Messages.DecreseMaterialError);
                    }
                    #endregion

                    #region Save Accounting Document Automatically
                    //ثبت سند حسابداری
                    bool createDocumentSuccessed = this.RegistrationAccountingDocument(salesInvoice.Date,
                        salesInvoice.Amount, salesInvoice.Count, salesInvoice.SalesInvoiceId);

                    if (!createDocumentSuccessed)
                    {
                        transScope.Dispose();
                        throw new Exception(Messages.AccountingDocumentError);
                    }
                    #endregion

                    transScope.Complete();
                }
                catch (TransactionException e)
                {
                    transScope.Dispose();
                    throw new Exception(Messages.ErrorOccured);

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
            return DbContext.SalesInvoices;
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
                StoreHouse currentStoreHouse = DbContext.StoreHouses.Find(storeHouseId);
                //اگر کالای موجود در انبار کمتر از مقدار سفارش باشد
                if (currentStoreHouse.Count < salesInvoiceNumber)
                    return false;
                if (currentStoreHouse.Count > 0 && currentStoreHouse.Count > salesInvoiceNumber)
                {
                    currentStoreHouse.Count -= salesInvoiceNumber;
                    DbContext.Entry(currentStoreHouse).State = EntityState.Modified;
                }
                DbContext.SaveChanges();
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
        public bool RegistrationAccountingDocument(DateTime date, decimal salesInvoiceAmount, int salesInvoiceNumber, int salesInvoiceId)
        {
            try
            {
                AccountingDocument obj = new AccountingDocument();
                obj.DocumentDate = date;
                obj.DocumentNumber = _documentService.CreateDocumentNumber();//شماره سند
                obj.Amount = salesInvoiceAmount * salesInvoiceNumber;
                obj.SalesInvoiceId = salesInvoiceId;

                DbContext.AccountingDocuments.Add(obj);
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}
