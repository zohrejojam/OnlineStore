using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.IServices
{
    public interface IMaterial
    {
        bool IsUniqueMaterialCode(string materialCode);
        bool IsUniqueMaterialTitleInGroup(string materialtitle, int materialGroupId);
    }
}
