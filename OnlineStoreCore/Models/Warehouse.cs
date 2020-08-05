using System;
using System.Collections.Generic;

namespace OnlineStoreCore.Models
{
    public class Warehouse
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public DateTime Date { get; set; }
        public string InputInvoiceNumber { get; set; }
        public ICollection<SalesInvoice> SalesInvoices { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}