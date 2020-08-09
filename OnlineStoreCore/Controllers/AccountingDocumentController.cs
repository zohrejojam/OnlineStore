using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using OnlineStoreCore.IServices;

namespace OnlineStoreCore.Controllers
{
    [Route("api/AccountingDocument")]
    [ApiController]
    public class AccountingDocumentController : ControllerBase
    {
        private readonly IAccountingDocumentService _accountingDocumentService;

        public AccountingDocumentController(IAccountingDocumentService accountingDocumentService)
        {
            _accountingDocumentService = accountingDocumentService;
        }

        public IList<MaterialListDto> GetStoreHouseInventory(int? page, int? count, string filterString, string sortColumnName)
        {
            return _accountingDocumentService.GetStoreHouseInventory(page, count, filterString, sortColumnName);
        }
    }
}