using MISA.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Interfaces.Services
{
    public interface IBaseFacilityService<FacilityEntity>
    {
        /// <summary>
        /// Lấy tất cả 
        /// Created: LHTDung
        /// Date: 21/9/2021
        /// </summary>
        /// <returns></returns>
        ServiceResult GetAll();

        /// <summary>
        /// Lấy theo id
        /// Created: LHTDung
        /// Date: 21/9/2021
        /// </summary>
        /// <param name="facilityEntityId"></param>
        /// <returns></returns>
        ServiceResult GetById(Guid facilityEntityId);
    }
}
