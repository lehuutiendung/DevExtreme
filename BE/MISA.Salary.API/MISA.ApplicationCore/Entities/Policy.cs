using MISA.ApplicationCore.MISAAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Entities
{
    public class Policy : BaseEntity
    {
        #region Property
        /// <summary>
        /// Khóa chính PolicyId
        /// Created: LHTDung
        /// Date: 21/9/2021
        /// </summary>
        public Guid PolicyId { get; set; }

        /// <summary>
        /// Tên chính sách
        /// Created: LHTDung
        /// Date: 21/9/2021
        /// </summary>
        [MISARequired("Tên chính sách")]

        public string PolicyName { get; set; }

        /// <summary>
        /// Id của vị trí áp dụng
        /// Created: LHTDung
        /// Date: 21/9/2021
        /// </summary>
        public Guid? PositionId { get; set; }

        /// <summary>
        /// Id đơn vị áp dụng
        /// Created: LHTDung
        /// Date: 21/9/2021
        /// </summary>
        [MISARequired("Mã đơn vị")]
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Id nhân viên áp dụng
        /// Created: LHTDung
        /// Date: 21/9/2021
        /// </summary>

        public Guid? EmployeeId { get; set; }

        /// <summary>
        /// Id thành phần
        /// Created: LHTDung
        /// Date: 21/9/2021
        /// </summary>
        public Guid? ComponentId { get; set; }

        /// <summary>
        /// Trạng thái
        /// Created: LHTDung
        /// Date: 21/9/2021
        /// </summary>

        public int? Status { get; set; }
        #endregion
    }
}
