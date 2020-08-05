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
        private readonly ISalesInvoiceService _service;

        public SalesInvoiceController(ISalesInvoiceService service)
        {
            _service = service;
        }
        public void Add([FromBody]SalesInvoice salesInvoice)
        {
            _service.Add(salesInvoice);
        }

        public IList<SalesInvoice> Get()
        {
            return _service.Get();
        }
    }
}