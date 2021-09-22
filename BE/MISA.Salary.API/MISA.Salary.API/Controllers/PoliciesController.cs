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
    public class PoliciesController : BaseController<Policy>
    {
        IPolicyService _policyService;
        public PoliciesController(IPolicyService policyService, IBaseService<Policy> baseService) : base(baseService)
        {
            _policyService = policyService;
        }
    }
}
