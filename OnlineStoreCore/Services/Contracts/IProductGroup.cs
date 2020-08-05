using OnlineStoreCore.Models;
using System.Collections.Generic;

namespace OnlineStoreCore.Services
{
    public interface IProductGroupService
    {
        IList<ProductGroup> Get();
        void Add(ProductGroup materialGroup);
    }
}
