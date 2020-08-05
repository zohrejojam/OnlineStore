using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineStoreCore.Models;
using OnlineStoreCore.Services;

namespace OnlineStoreCore.Controllers
{
    [Route("api/SalesInvoice")]
    [ApiController]
    public class SalesInvoiceController : ControllerBase
    {
        private readonly SalesInvoiceService _service;

        public SalesInvoiceController(SalesInvoiceService service)
        {
            _service = service;
        }
        public void Add([FromBody]SalesInvoice salesInvoice)
        {
            _service.Add(salesInvoice);
        }

        public IQueryable<SalesInvoice> Get()
        {
            return _service.Get();
        }

        public bool DecreaseMaterialCount(int storeHouseId, int salesInvoiceNumber)
        {
            return _service.DecreaseMaterialCount(storeHouseId, salesInvoiceNumber);
        }

        public bool RegistrationAccountingDocument(DateTime date, decimal salesInvoiceAmount, int salesInvoiceNumber, int salesInvoiceId)
        {
            return _service.RegistrationAccountingDocument(date, salesInvoiceNumber, salesInvoiceNumber, salesInvoiceId);
        }
    }
}