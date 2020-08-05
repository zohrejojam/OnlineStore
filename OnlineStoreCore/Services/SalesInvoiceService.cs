using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Microsoft.EntityFrameworkCore;
using OnlineStoreCore.DataLayer;
using OnlineStoreCore.Models;
using OnlineStoreCore.Resources;

namespace OnlineStoreCore.Services
{
    public class SalesInvoiceService
    {
        private readonly DataBaseContext DbContext;
        private readonly AccountingDocumentService _documentService;
        public SalesInvoiceService(DataBaseContext context,AccountingDocumentService documentService)
        {
            DbContext = context;
            _documentService = documentService;
        }

        public void Add(SalesInvoice salesInvoice)
        {
            using (TransactionScope transScope = new TransactionScope())
            {
                try
                {
                    int minInventory = 0;
                    //بدست آوردن حداقل موجودی برای این کالا
                    Warehouse currentWareHouse = DbContext.Warehouses.FirstOrDefault(p => p.Id == salesInvoice.WarehouseId);
                    if (currentWareHouse != null)
                    {
                        Product currentMaterial = DbContext.Products.FirstOrDefault(p => p.Id == currentWareHouse.Id);
                        minInventory = currentMaterial != null ? currentMaterial.MinimumInventory : 0;
                    }

                    //تعداد کالا بیشتر از حداقل موجودی نباشد
                    if (salesInvoice.Count <= 0 ||
                        salesInvoice.Count > minInventory)
                        throw new Exception(Messages.MaterialNumberMustBeMoreThanMinInventory);


                    DbContext.SalesInvoices.Add(salesInvoice);
                    DbContext.SaveChanges();

                    #region Decrease Material Number
                    //کسر کردن تعداد کالای فروخته شده از انبار
                    bool decreeseSuccessed = this.DecreaseMaterialCount(salesInvoice.WarehouseId, salesInvoice.Count);
                    if (!decreeseSuccessed)
                    {
                        transScope.Dispose();
                        throw new Exception(Messages.DecreseMaterialError);
                    }
                    #endregion

                    #region Save Accounting Document Automatically
                    //ثبت سند حسابداری
                    bool createDocumentSuccessed = this.RegistrationAccountingDocument(salesInvoice.Date,
                        salesInvoice.Amount, salesInvoice.Count, salesInvoice.Id);

                    if (!createDocumentSuccessed)
                    {
                        transScope.Dispose();
                        throw new Exception(Messages.AccountingDocumentError);
                    }
                    #endregion

                    transScope.Complete();
                }
                catch (TransactionException)
                {
                    transScope.Dispose();
                    throw new Exception(Messages.ErrorOccured);

                }
            }
        }

        public IList<SalesInvoice> Get()
        {
            return DbContext.SalesInvoices.ToList();
        }

        public bool DecreaseMaterialCount(int storeHouseId, int salesInvoiceNumber)
        {
            try
            {
                Warehouse currentStoreHouse = DbContext.Warehouses.Find(storeHouseId);
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

        public bool RegistrationAccountingDocument(DateTime date, decimal salesInvoiceAmount, int salesInvoiceNumber, int salesInvoiceId)
        {
            try
            {
                AccountingDocument obj = new AccountingDocument();
                obj.Date = date;
                obj.Number = _documentService.CreateDocumentNumber();//شماره سند
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
