using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.ApplicationCore.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.Salary.API.Controllers
{
    public class BaseController<Entity> : ControllerBase
    {
        IBaseService<Entity> _baseService;
        public BaseController(IBaseService<Entity> baseService)
        {
            _baseService = baseService;
        }

        #region API lấy tất cả bản ghi

        /// <summary>
        /// API lấy tất cả bản ghi
        /// </summary>
        /// <returns>Danh sách các bản ghi</returns>
        /// Created by LHTDung - 21/09/2021 
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var _serviceResult = _baseService.GetAll();
                //Trả về cho client
                if (_serviceResult.IsValid)
                {
                    var response = StatusCode(200, _serviceResult.Data);
                    return response;
                }
                else
                {
                    return NoContent();
                }
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
        #endregion

        #region API lấy thông tin chi tiết 1 bản ghi

        /// <summary>
        /// API lấy thông tin chi tiết 1 bản ghi
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns>Chi tiết 1 bản ghi</returns>
        /// Created by: LHTDung - 21/09/2021
        [HttpGet("{entityId}")]
        public IActionResult GetById(Guid entityId)
        {
            try
            {
                var _serviceResult = _baseService.GetById(entityId);
                //Trả về cho client
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
        #endregion

        #region API phân trang và bộ lọc theo tên, mã nhân viên, số điện thoại

        /// <summary>
        /// API thực hiện phân trang, bộ lọc
        /// </summary>
        /// <param name="pageSize">Số bản ghi trong 1 trang</param>
        /// <param name="pageNumber">Trang hiện tại</param>
        /// <param name="filter">Họ tên/ Mã/ Số điện thoại</param>
        /// <param name="departmentName">Id phòng ban</param>
        /// <returns>Danh sách bản ghi chứa trong trang / theo bộ lọc</returns>
        /// Created by: LHTDung - 27/08/2021
        [HttpGet("Filter")]
        public virtual IActionResult Filter(int pageSize, int pageNumber, String filter, int? status, String departmentName)
        {
            try
            {
                var _serviceResult = _baseService.Filter(pageSize, pageNumber, filter, status, departmentName);
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
        #endregion

        #region API thêm mới bản ghi

        /// <summary>
        /// Thêm mới 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// Created by: LHTDung - 27/08/2021
        [HttpPost]
        public virtual IActionResult Add(Entity entity)
        {
            try
            {
                var serviceResult = _baseService.Add(entity);
                if (serviceResult.IsValid == true)
                {
                    return StatusCode(201, serviceResult.Data); ;
                }
                else
                {
                    var errorObj = new
                    {
                        devMsg = serviceResult.Data,
                        userMsg = serviceResult.Data,
                        errorCode = "",
                        moreInfo = "",
                        traceId = ""
                    };
                    return BadRequest(errorObj);
                }

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
        #endregion

        #region API xóa nhiều bản ghi
        /// <summary>
        /// Xóa nhiều bản ghi
        /// </summary>
        /// <param name="listId">Danh sách chứa các id</param>
        /// <returns></returns>
        /// Created by: LHTDung - 21/09/2021
        [HttpDelete]
        public IActionResult DeleteMultiple(List<Guid> listId)
        {
            try
            {
                var _serviceResult = _baseService.DeleteMultiple(listId);

                // Trả về cho client
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
        #endregion

        #region API sửa thông tin nhân viên

        /// <summary>
        /// Sửa bản ghi
        /// </summary>
        /// <param name="entityId">id bản ghi</param>
        /// <param name="entity">đối tượng sửa đổi</param>
        /// <returns></returns>
        /// Created by: LHTDung - 27/08/2021
        [HttpPut("{entityId}")]
        public IActionResult Edit(Guid entityId, Entity entity)
        {
            try
            {
                var _serviceResult = _baseService.Update(entityId, entity);
                if (_serviceResult.IsValid == true)
                {
                    //if ((int)_serviceResult.Data == 400)
                    //{
                    //    var errorObj = new
                    //    {
                    //        devMsg = MISA.ApplicationCore.Resources.ResourcesCommon.Duplicate_ErrorMsg.Replace("{}", ""),
                    //        userMsg = MISA.ApplicationCore.Resources.ResourcesCommon.Duplicate_ErrorMsg.Replace("{}", ""),
                    //        errorCode = "",
                    //        moreInfo = "",
                    //        traceId = ""
                    //    };
                    //    return StatusCode(400, errorObj);
                    //}
                    //4. Trả về
                    var response = StatusCode(200, _serviceResult.Data);
                    return response;
                }
                else
                {
                    var errorObj = new
                    {
                        devMsg = _serviceResult.Data,
                        userMsg = _serviceResult.Data,
                        errorCode = "",
                        moreInfo = "",
                        traceId = ""
                    };
                    return BadRequest(errorObj);
                }

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
        #endregion

    }
}
