using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreCore.IServices
{
    public interface IAccountingDocumentService
    {
        string CreateDocumentNumber();
        IQueryable<MaterialListDto> GetStoreHouseInventory(int? page, int? count, string filter, string sortColumns);
    }

    public class MaterialListDto
    {
        public int Id { get; set; }
        public string MaterialCode { get; set; }
        public string MaterialTitle { get; set; }
        public string MaterialGroupName { get; set; }
        public int MinInventory { get; set; }
        public int Count { get; set; }
        public string Status { get; set; }
    }
}
