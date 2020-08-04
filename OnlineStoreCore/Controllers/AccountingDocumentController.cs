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
    [Route("api/AccountingDocument")]
    [ApiController]
    public class AccountingDocumentController : ControllerBase
    {
        private AccountingDocumentService _service;
        public AccountingDocumentController(AccountingDocumentService service)
        {
            _service = service;
        }
        public IQueryable<ListOfMaterial> StoreHouseInventory(int? page, int? count, string filterString, string SortColumnName)
        {
            return _service.StoreHouseInventory(page, count, filterString, SortColumnName);
        }

        public IQueryable<ListOfMaterial> FilterList(IQueryable<ListOfMaterial> query, string filterString)
        {
            return _service.FilterList(query, filterString);
        }

        public IQueryable<ListOfMaterial> SortList(IQueryable<ListOfMaterial> query, string SortColumnName)
        {
            return _service.SortList(query, SortColumnName);
        }
    }
}