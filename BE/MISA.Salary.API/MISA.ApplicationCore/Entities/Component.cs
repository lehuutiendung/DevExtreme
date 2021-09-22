using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Entities
{
    public class Component : BaseEntity
    {
        #region Property
        /// <summary>
        /// Khóa chính của thành phần
        /// Created: LHTDung
        /// Date: 21/9/2021
        /// </summary>
        public Guid ComponentId { get; set; }

        /// <summary>
        /// Mã thành phần
        /// Created: LHTDung
        /// Date: 21/9/2021
        /// </summary>
        public string ComponentCode { get; set; }

        /// <summary>
        /// Tên thành phần
        /// Created: LHTDung
        /// Date: 21/9/2021
        /// </summary>
        public string ComponentName { get; set; }

        /// <summary>
        /// Loại thành phần
        /// Created: LHTDung
        /// Date: 21/9/2021
        /// </summary>
        public string ComponentType { get; set; }

        /// <summary>
        /// Giá trị thành phần (Công thức tính)
        /// Created: LHTDung
        /// Date: 21/9/2021
        /// </summary>
        public string ComponentValue { get; set; }

        #endregion
    }
}
