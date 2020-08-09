using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using OnlineStoreCore.Models;
using OnlineStoreCore.Services;

namespace OnlineStoreCore.Controllers
{
    [Route("api/SalesInvoice")]
    [ApiController]
    public class SalesInvoiceController : ControllerBase
    {
        private readonly ISalesInvoiceService _salesInvoiceService;

        public SalesInvoiceController(ISalesInvoiceService salesInvoiceService)
        {
            _salesInvoiceService = salesInvoiceService;
        }
        public void Add([FromBody]SalesInvoice salesInvoice)
        {
            _salesInvoiceService.Add(salesInvoice);
        }

        public IList<SalesInvoice> Get()
        {
            return _salesInvoiceService.Get();
        }
    }
}