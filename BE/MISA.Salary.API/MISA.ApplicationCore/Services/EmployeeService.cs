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
    public class EmployeeService : BaseFacilityService<Employee>, IEmployeeService
    {
        ServiceResult _serviceResult;
        public EmployeeService(IBaseFacilityRepository<Employee> baseFacilityRepository) : base(baseFacilityRepository)
        {
            _serviceResult = new ServiceResult();
        }
    }
}
