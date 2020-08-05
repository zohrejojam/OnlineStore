using System;

namespace OnlineStoreCore.Models
{
    public class AccountingDocument
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Number { get; set; }
        public int SalesInvoiceId { get; set; }
        public virtual SalesInvoice SalesInvoice { get; set; }
        public decimal Amount { get; set; }
    }
}