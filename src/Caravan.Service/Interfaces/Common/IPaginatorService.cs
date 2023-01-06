using Caravan.Service.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caravan.Service.Interfaces.Common
{
    public interface IPaginatorService
    {
        void ToPaginator(PaginationMetaData metaData);
    }
}
