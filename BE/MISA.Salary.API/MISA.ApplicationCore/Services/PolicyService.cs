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
    public class PolicyService : BaseService<Policy>, IPolicyService
    {
        IPolicyRepository _policyRepository;
        ServiceResult _serviceResult;

        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        public PolicyService(IBaseRepository<Policy> baseRepository, IPolicyRepository policyRepository) : base(baseRepository)
        {
            _policyRepository = policyRepository;
            _serviceResult = new ServiceResult();
        }

    }
}
