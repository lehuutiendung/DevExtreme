using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Entities
{
    public class Department : BaseEntity
    {
        /// <summary>
        /// Khóa chính của đơn vị
        /// Created: LHTDung
        /// Date: 21/9/2021
        /// </summary>
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Tên đơn vị
        /// Created: LHTDung
        /// Date: 21/9/2021
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// Khóa chính của đơn vị cha
        /// Created: LHTDung
        /// Date: 21/9/2021
        /// </summary>
        public Guid ParentId { get; set; }

    }
}
