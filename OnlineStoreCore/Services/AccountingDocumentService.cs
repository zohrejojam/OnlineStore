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
            var takePage = page ?? 1;
            var takeCount = count ?? 5;

            var query = DbContext.Products.Include(p=>p.Warehouse);

            List<MaterialListDto> result = query
               .Select(p => new MaterialListDto
               {
                   Id = p.WareHouseId,
                   MaterialCode = p.Code,
                   MaterialTitle = p.Title,
                   MaterialGroupName = p.Group.Name,
                   MinInventory = p.MinimumInventory,
                   Count = p.Warehouse.Count,
                   Status = p.Warehouse.Count == 0 ? "ناموجود" : (p.Warehouse.Count <= p.MinimumInventory ? "آماده سفارش" : "عادی")

               })
               .OrderBy(p => p.Id)
               .Skip((takePage - 1) * takeCount)
                 .Take(takeCount).ToList();
            result = this.FilterList(result, filter);
            result = this.SortList(result, sortColumns);
            return result.ToList();

        }

        public List<MaterialListDto> FilterList(List<MaterialListDto> query, string filterString)
        {
            List<MaterialListDto> result=new List<MaterialListDto>();
            filterString = filterString != "" && filterString != null ? filterString.ToLower().Trim() : "";
            if (filterString != "" && filterString != null)
            {
                result = query.Where(p => p.MaterialCode.ToLower().Contains(filterString) ||
                                    p.MaterialTitle.ToLower().Contains(filterString) ||
                                    p.MaterialGroupName.ToLower().Contains(filterString) ||
                                    p.MinInventory.ToString() == filterString ||
                                    p.Count.ToString() == filterString).ToList();
            }
            return result;
        }

        public List<MaterialListDto> SortList(List<MaterialListDto> query, string SortColumnName)
        {
            SortColumnName = SortColumnName != "" && SortColumnName != null ? SortColumnName.ToLower().Trim() : "";
            if (SortColumnName != "" && SortColumnName != null)
            {
                string[] sortList = SortColumnName.Split(',');
                foreach (string item in sortList)
                {
                    switch (item)
                    {
                        case "materialcode":
                            query = query.OrderBy(p => p.MaterialCode).ToList();
                            break;

                        case "materialcode_desc":
                            query = query.OrderByDescending(p => p.MaterialCode).ToList();
                            break;

                        case "materialtitle":
                            query = query.OrderBy(p => p.MaterialTitle).ToList();
                            break;

                        case "materialtitle_desc":
                            query = query.OrderByDescending(p => p.MaterialTitle).ToList();
                            break;

                        case "materialgroupname":
                            query = query.OrderBy(p => p.MaterialGroupName).ToList();
                            break;

                        case "materialgroupname_desc":
                            query = query.OrderByDescending(p => p.MaterialGroupName).ToList();
                            break;

                        case "mininventory":
                            query = query.OrderBy(p => p.MinInventory).ToList();
                            break;

                        case "mininventory_desc":
                            query = query.OrderByDescending(p => p.MinInventory).ToList();
                            break;

                        case "count":
                            query = query.OrderBy(p => p.Count).ToList();
                            break;

                        case "count_desc":
                            query = query.OrderByDescending(p => p.Count).ToList();
                            break;
                        default:
                            break;
                    }
                }
            }
            return query.ToList();
        }

    }
}
