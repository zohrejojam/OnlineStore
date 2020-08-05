using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreCore.IServices
{
    public interface IMaterialService
    {
        bool IsUniqueCode(string code);
        bool IsUniqueTitleInGroup(string title, int groupId);
    }
}
