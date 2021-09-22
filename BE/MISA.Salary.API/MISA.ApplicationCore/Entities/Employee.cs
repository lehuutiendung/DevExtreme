using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Entities
{
    public class Employee : BaseEntity
    {
        #region Property
        /// <summary>
        /// Khóa chính EmployeeId
        /// Created: LHTDung
        /// Date: 21/9/2021
        /// </summary>
        public Guid EmployeeId { get; set; }

        /// <summary>
        /// Mã nhân viên
        /// Created: LHTDung
        /// Date: 21/9/2021
        /// </summary>

        public string EmployeeCode { get; set; }

        /// <summary>
        /// Họ và tên
        /// Created: LHTDung
        /// Date: 21/9/2021
        /// </summary>

        public string FullName { get; set; }

        /// <summary>
        /// Id đơn vị
        /// Created: LHTDung
        /// Date: 21/9/2021
        /// </summary>
        public Guid? DepartmentId { get; set; }

        /// <summary>
        /// Chức danh
        /// Created: LHTDung
        /// Date: 21/9/2021
        /// </summary>
        public Guid? PositionId { get; set; }

        #endregion
    }
}
