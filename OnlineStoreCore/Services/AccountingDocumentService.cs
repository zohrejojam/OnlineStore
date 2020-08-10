using System;
using System.Linq;
using OnlineStoreCore.IServices;
using OnlineStoreCore.DataLayer;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace OnlineStoreCore.Services
{
    public class AccountingDocumentService : IAccountingDocumentService
    {
        private readonly DataBaseContext DbContext;

        public AccountingDocumentService(DataBaseContext context)
        {
            DbContext = context;
        }

        public string CreateDocumentNumber()
        {
            return DateTime.Now.ToString("yyyyMMdd") +
                   DateTime.Now.Hour.ToString() +
                   DateTime.Now.Minute.ToString() +
                   DateTime.Now.Second.ToString();
        }

        public IList<MaterialListDto> GetStoreHouseInventory(int? page, int? count, string filter, string sortColumns)
        {
            int takePage = page ?? 1;
            int takeCount = count ?? 5;

            var query = DbContext.Products.Include(p => p.Warehouse);

            IQueryable<MaterialListDto> result = query
               .Select(p => new MaterialListDto
               {
                   Id = p.WareHouseId,
                   MaterialCode = p.Code,
                   MaterialTitle = p.Title,
                   MaterialGroupName = p.Group.Name,
                   MinInventory = p.MinimumInventory,
                   Count = p.Warehouse.Count,
                   Status = p.Warehouse.Count == 0 ? "ناموجود" : (p.Warehouse.Count <= p.MinimumInventory ? "آماده سفارش" : "عادی")

               });
            result = this.Paging(result, takePage, takeCount);
            result = this.FilterList(result, filter);
            result = this.SortList(result, sortColumns);
            return result.ToList();
        }

        public IQueryable<MaterialListDto> Paging(IQueryable<MaterialListDto> query, int page, int count)
        {
            return query.OrderBy(p => p.Id)
               .Skip((page - 1) * count)
                 .Take(count);
        }

        public IQueryable<MaterialListDto> SortList(IQueryable<MaterialListDto> query, string SortColumnName)
        {
            return Extensions.OrderBy<MaterialListDto>(query, SortColumnName, true);
        }

        public IQueryable<MaterialListDto> FilterList(IQueryable<MaterialListDto> query, string filterString)
        {

            filterString = filterString != "" && filterString != null ? filterString.ToLower().Trim() : "";
            if (filterString != "" && filterString != null)
                throw new Exception(Resources.Messages.ErrorOccured);

            return query.Where(p => p.MaterialCode.ToLower().Contains(filterString) ||
                                 p.MaterialTitle.ToLower().Contains(filterString) ||
                                 p.MaterialGroupName.ToLower().Contains(filterString) ||
                                 p.MinInventory.ToString() == filterString ||
                                 p.Count.ToString() == filterString);
        }
    }
}
