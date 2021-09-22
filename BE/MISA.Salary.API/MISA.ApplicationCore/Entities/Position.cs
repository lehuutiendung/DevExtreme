using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Entities
{
    public class Position : BaseEntity
    {
        #region Property
        /// <summary>
        /// Khóa chính PositionId
        /// Created: LHTDung
        /// Date: 21/9/2021
        /// </summary>
        public Guid PositionId { get; set; }

        /// <summary>
        /// Tên vị trí áp dụng
        /// Created: LHTDung
        /// Date: 21/9/2021
        /// </summary>
        public string PositionName { get; set; }

        #endregion

    }
}
