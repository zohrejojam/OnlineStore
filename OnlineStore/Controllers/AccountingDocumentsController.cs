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
        //[System.Web.Http.HttpGet]
        // GET: api/AccountingDocuments
        [System.Web.Http.HttpGet]
        public IQueryable<ListOfMaterial> StoreHouseInventory(int? page,  int? count, 
                                                              string filterStr, 
                                                              string SortcolumnName)
        {
            var takePage = page ?? 1;
            var takeCount = count ?? 5;
            filterStr = filterStr!="" && filterStr!=null?filterStr.ToLower().Trim():"";
            SortcolumnName = SortcolumnName != "" && SortcolumnName != null? SortcolumnName.ToLower().Trim():"";
            var query = db.StoreHouses.Include(p => p.Material).Include(p => p.Material.MaterialGroup);

            //for filter
            if (filterStr != "" && filterStr != null)
            {
                query = query.Where(p => p.Material.MaterialCode.ToLower().Contains(filterStr) ||
                                     p.Material.MaterialTitle.ToLower().Contains(filterStr) ||
                                     p.Material.MaterialGroup.MaterialGroupName.ToLower().Contains(filterStr) ||
                                     p.Material.MinInventory.ToString() == filterStr ||
                                     p.Count.ToString() == filterStr);
            }
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
            if (SortcolumnName!="" && SortcolumnName!=null)
            {
                string[] sortList = SortcolumnName.Split(',');
                foreach (string item in sortList)
                {
                    switch (item)
                    {
                        case "materialcode":
                            result = result.OrderBy(p => p.MaterialCode);
                            break;

                        case "materialcode_desc":
                            result = result.OrderByDescending(p => p.MaterialCode);
                            break;

                        case "materialtitle":
                            result = result.OrderBy(p => p.MaterialTitle);
                            break;

                        case "materialtitle_desc":
                            result = result.OrderByDescending(p => p.MaterialTitle);
                            break;

                        case "materialgroupname":
                            result = result.OrderBy(p => p.MaterialGroupName);
                            break;

                        case "materialgroupname_desc":
                            result = result.OrderByDescending(p => p.MaterialGroupName);
                            break;

                        case "mininventory":
                            result = result.OrderBy(p => p.MinInventory);
                            break;

                        case "mininventory_desc":
                            result = result.OrderByDescending(p => p.MinInventory);
                            break;

                        case "count":
                            result = result.OrderBy(p => p.Count);
                            break;

                        case "count_desc":
                            result = result.OrderByDescending(p => p.Count);
                            break;
                        default:
                            break;
                    }

                }
            }
            return result;
               

        }

    }
}