using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Interfaces.Repository;
using MISA.ApplicationCore.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Services
{
    public class ComponentService : BaseFacilityService<Component>, IComponentService
    {
        IComponentRepository _componentRepository;
        ServiceResult _serviceResult;

        public ComponentService(IBaseFacilityRepository<Component> baseFacilityRepository, IComponentRepository componentRepository) : base(baseFacilityRepository)
        {
            _componentRepository = componentRepository;
            _serviceResult = new ServiceResult();
        }

        /// <summary>
        /// Service thực hiện gọi phân trang, filter
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="filter"></param>
        /// <param name="componentType"></param>
        /// LHTDung - 23/09/2021
        /// <returns></returns>
        public ServiceResult Filter(int pageSize, int pageNumber, string filter, string componentType)
        {
            _serviceResult.Data = _componentRepository.Filter(pageSize, pageNumber, filter, componentType);
            return _serviceResult;
        }
    }
}
