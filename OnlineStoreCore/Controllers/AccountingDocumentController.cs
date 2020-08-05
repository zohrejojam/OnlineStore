using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineStoreCore.IServices;
using OnlineStoreCore.Models;
using OnlineStoreCore.Services;

namespace OnlineStoreCore.Controllers
{
    [Route("api/AccountingDocument")]
    [ApiController]
    public class AccountingDocumentController : ControllerBase
    {
        private readonly AccountingDocumentService _service;

        public AccountingDocumentController(AccountingDocumentService service)
        {
            _service = service;
        }

        public IQueryable<MaterialListDto> GetStoreHouseInventory(int? page, int? count, string filterString, string sortColumnName)
        {
            return _service.GetStoreHouseInventory(page, count, filterString, sortColumnName);
        }
    }
}