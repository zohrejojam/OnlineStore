using OnlineStoreCore.Models;
using System.Collections.Generic;

namespace OnlineStoreCore.IServices
{
    public interface IProductService
    {
        bool IsUniqueCode(string code);
        bool IsUniqueTitleInGroup(string title, int groupId);
        void Add(Product material);
        IList<Product> Get();
    }
}
