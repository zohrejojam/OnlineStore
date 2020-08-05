using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using OnlineStoreCore.IServices;

namespace OnlineStoreCore.Controllers
{
    [Route("api/AccountingDocument")]
    [ApiController]
    public class AccountingDocumentController : ControllerBase
    {
        private readonly IAccountingDocumentService _service;

        public AccountingDocumentController(IAccountingDocumentService service)
        {
            _service = service;
        }

        public IList<MaterialListDto> GetStoreHouseInventory(int? page, int? count, string filterString, string sortColumnName)
        {
            return _service.GetStoreHouseInventory(page, count, filterString, sortColumnName);
        }
    }
}