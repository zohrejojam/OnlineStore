using System;
using System.Collections.Generic;

namespace OnlineStoreCore.Models
{
    public class SalesInvoice
    {
        public int Id { get; set; }
        public int WarehouseId { get; set; }
        public virtual Warehouse Warehouse { get; set; }
        public int Count { get; set; }
        public DateTime Date { get; set; }
        public string OutputInvoiceNumber { get; set; }
        public string CustomerName { get; set; }
        public decimal Amount { get; set; }
        public ICollection<AccountingDocument> AccountingDocuments { get; set; }
    }
}