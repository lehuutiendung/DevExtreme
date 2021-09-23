using MISA.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Interfaces.Services
{
    public interface IComponentService : IBaseFacilityService<Component>
    {
        /// <summary>
        /// Bộ lọc theo tên, loại thành phần
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="filter"></param>
        /// <param name="componentType"></param>
        /// <returns></returns>
        ServiceResult Filter(int pageSize, int pageNumber, String filter, String componentType);
    }
}
