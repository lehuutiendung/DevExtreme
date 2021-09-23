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
    public class ComponentsController : BaseFacilityController<Component>
    {
        IComponentService _componentService;
        public ComponentsController(IComponentService componentService, IBaseFacilityService<Component> baseFacilityService) : base(baseFacilityService)
        {
            _componentService = componentService;
        }

        [HttpGet("FilterComponent")]
        public IActionResult Filter(int pageSize, int pageNumber, String filter, String componentType)
        {
            try
            {
                var _serviceResult = _componentService.Filter(pageSize, pageNumber, filter, componentType);
                var response = StatusCode(200, _serviceResult.Data);
                return response;
            }
            catch (Exception ex)
            {
                var errorObj = new
                {
                    devMsg = ex.Message,
                    userMsg = MISA.ApplicationCore.Resources.ResourcesCommon.Exception_ErrorMsg,
                    errorCode = "",
                    moreInfo = "",
                    traceId = ""
                };
                return StatusCode(500, errorObj);
            }
        }
    }
}
