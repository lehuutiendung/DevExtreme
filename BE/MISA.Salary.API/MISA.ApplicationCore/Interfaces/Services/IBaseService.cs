using MISA.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Interfaces.Services
{
    public interface IBaseService<Entity>
    {
        /// <summary>
        /// Lấy tất cả cho Entity
        /// </summary>
        /// <returns></returns>
        ServiceResult GetAll();

        /// <summary>
        /// Lấy thông tin của một entity
        /// </summary>
        /// <param name="entityId">Id của entity (Khóa chính)</param>
        /// <returns>Thông tin của entity</returns>
        ServiceResult GetById(Guid entityId);

        /// <summary>
        /// Bộ lọc theo Tên, trạng thái, CCTC
        /// </summary>
        /// <param name="fullName"></param>
        /// <param name="entityCode"></param>
        /// <param name="phoneNumber"></param>
        /// <returns>
        ///     totalRecord
        ///     {entity}
        /// </returns>
        ServiceResult Filter(int pageSize, int pageNumber, String filter, int? status, String departmentName);

        /// <summary>
        /// Xóa nhiều
        /// </summary>
        /// <param name="listId"></param>
        /// <returns></returns>
        /// CreatedBy: LHTDUNG - 21/09/2021
        ServiceResult DeleteMultiple(List<Guid> listId);

        /// <summary>
        /// Thêm mới entity
        /// </summary>
        /// <param name="entity">Thông tin của entity</param>
        /// <returns> ServiceResult : kết quả xử lý qua nghiệp vụ</returns>
        /// CreatedBy: LHTDUNG - 21/09/2021
        ServiceResult Add(Entity entity);

        /// <summary>
        /// Cập nhật thông tin entity
        /// </summary>
        /// <param name="entityId">Khóa chính - Id của entity</param>
        /// <param name="entity">Thông tin entity</param>
        /// <returns> ServiceResult : kết quả xử lý qua nghiệp vụ</returns>
        /// CreatedBy: LHTDUNG - 21/09/2021
        ServiceResult Update(Guid entityId, Entity entity);

    }
}
