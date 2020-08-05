using OnlineStoreCore.Models;
using System.Collections.Generic;

namespace OnlineStoreCore.Services
{
    public interface ISalesInvoiceService
    {
        void Add(SalesInvoice salesInvoice);
        IList<SalesInvoice> Get();
    }
}
