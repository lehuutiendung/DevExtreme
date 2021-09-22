using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.Salary.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : BaseFacilityController<Employee>
    {
        public EmployeesController(IBaseFacilityService<Employee> baseFacilityService) : base(baseFacilityService)
        {

        }
    }
}
