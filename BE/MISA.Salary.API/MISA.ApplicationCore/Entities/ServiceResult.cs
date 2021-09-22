using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Entities
{
    public class ServiceResult
    {
        /// <summary>
        /// Kiểm soát kết quả trả về đúng hay sai
        /// Created: LHTDung
        /// Date: 21/9/2021
        /// </summary>
        public bool IsValid { get; set; } = true;

        /// <summary>
        /// Data mà kết quả trả về
        /// Created: LHTDung
        /// Date: 21/9/2021
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// Thông báo
        /// Created: LHTDung
        /// Date: 21/9/2021
        /// </summary>
        public string Messenger { get; set; }
    }
}
