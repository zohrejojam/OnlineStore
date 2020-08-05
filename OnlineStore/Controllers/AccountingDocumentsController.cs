using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using OnlineStore.DataLayer;
using OnlineStore.Models;

namespace OnlineStore.Controllers
{
    public class AccountingDocumentsController : ApiController
    {
        private DataBaseContext db = new DataBaseContext();

        /// <summary>
        /// فهرست موجودی انبار
        /// </summary>
        /// <param name="page">صفحه ی چند</param>
        /// <param name="count">چه تعداد رکورد در یک صفحه</param>
        /// <param name="filterStr">رشته برای فیلتر</param>
        /// <param name="SortcolumnName">رشته برای سورت ستون ها</param>
        /// <returns></returns>
       // Get: api/AccountingDocuments
        [HttpGet]
        public IQueryable<ListOfMaterial> StoreHouseInventory(int? page,  int? count,string filterString, string SortcolumnName)
        {
            var takePage = page ?? 1;
            var takeCount = count ?? 5;
            
            var query = db.StoreHouses.Include(p => p.Material).Include(p => p.Material.MaterialGroup);
            
             var result = query
                .Select(p => new ListOfMaterial
                {
                    MaterialId = p.MaterialId,
                    MaterialCode = p.Material.MaterialCode,
                    MaterialTitle = p.Material.MaterialTitle,
                    MaterialGroupName = p.Material.MaterialGroup.MaterialGroupName,
                    MinInventory = p.Material.MinInventory,
                    Count = p.Count,
                    Status = p.Count == 0 ? "ناموجود" : (p.Count <= p.Material.MinInventory ? "آماده سفارش" : "عادی")

                })
                //For Paging
                .OrderBy(p => p.MaterialId)
                .Skip((takePage - 1) * takeCount)
                  .Take(takeCount);
            //for sort
            result = this.FilterList(result, filterString);
            result = this.SortList(result, SortcolumnName);
            return result;
               
        }

        public IQueryable<ListOfMaterial> FilterList(IQueryable<ListOfMaterial> query,string filterString)
        {
            filterString = filterString != "" && filterString != null ? filterString.ToLower().Trim() : "";
            //for filter
            if (filterString != "" && filterString != null)
            {
                 query = query.Where(p => p.MaterialCode.ToLower().Contains(filterString) ||
                                     p.MaterialTitle.ToLower().Contains(filterString) ||
                                     p.MaterialGroupName.ToLower().Contains(filterString) ||
                                     p.MinInventory.ToString() == filterString ||
                                     p.Count.ToString() == filterString);
            }
            return query;
        }

        public IQueryable<ListOfMaterial> SortList(IQueryable<ListOfMaterial> query, string SortcolumnName)
        {
            SortcolumnName = SortcolumnName != "" && SortcolumnName != null ? SortcolumnName.ToLower().Trim() : "";
            if (SortcolumnName != "" && SortcolumnName != null)
            {
                string[] sortList = SortcolumnName.Split(',');
                foreach (string item in sortList)
                {
                    switch (item)
                    {
                        case "materialcode":
                            query = query.OrderBy(p => p.MaterialCode);
                            break;

                        case "materialcode_desc":
                            query = query.OrderByDescending(p => p.MaterialCode);
                            break;

                        case "materialtitle":
                            query = query.OrderBy(p => p.MaterialTitle);
                            break;

                        case "materialtitle_desc":
                            query = query.OrderByDescending(p => p.MaterialTitle);
                            break;

                        case "materialgroupname":
                            query = query.OrderBy(p => p.MaterialGroupName);
                            break;

                        case "materialgroupname_desc":
                            query = query.OrderByDescending(p => p.MaterialGroupName);
                            break;

                        case "mininventory":
                            query = query.OrderBy(p => p.MinInventory);
                            break;

                        case "mininventory_desc":
                            query = query.OrderByDescending(p => p.MinInventory);
                            break;

                        case "count":
                            query = query.OrderBy(p => p.Count);
                            break;

                        case "count_desc":
                            query = query.OrderByDescending(p => p.Count);
                            break;
                        default:
                            break;
                    }
                }
            }
            return query;
        }

    }
}