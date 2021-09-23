using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Interfaces.Repository;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Infrastructure.Repository
{
    public class ComponentRepository : BaseFacilityRepository<Component>, IComponentRepository
    {
        public ComponentRepository(IConfiguration configuration) : base(configuration)
        {

        }
        /// <summary>
        /// Tương tác thực hiện phân trang, tìm kiếm "Thành phần"
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="filter"></param>
        /// <param name="componentType"></param>
        /// LHTDung - 23/09/2021
        /// <returns></returns>
        public object Filter(int pageSize, int pageNumber, string filter, string componentType)
        {
            //Khởi tạo đối tượng kết nối với database
            using (_dbConnection = new MySqlConnection(_connectionString))
            {
                DynamicParameters dynamicParameters = new DynamicParameters();

                //Tính toán giá trị Offset
                int offSet = pageSize * (pageNumber - 1);

                //Câu lệnh truy vấn phân trang và filter theo các tiêu chí
                var sqlCommand = $"Proc_GetComponentFilterPaging";

                dynamicParameters.Add($"@PageSize", pageSize);
                dynamicParameters.Add($"@PageNumber", pageNumber);
                dynamicParameters.Add($"@Filter", $"%{filter}%");
                dynamicParameters.Add($"@ComponentType", $"%{componentType}%");
                dynamicParameters.Add($"@TotalPage", dbType: DbType.Int32, direction: ParameterDirection.Output);
                dynamicParameters.Add($"@TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);

                var entity = _dbConnection.Query<object>(sqlCommand, param: dynamicParameters, commandType: CommandType.StoredProcedure);
                var totalPage = dynamicParameters.Get<int>("@TotalPage");
                var totalRecord = dynamicParameters.Get<int>("@TotalRecord");
                return new { TotalPage = totalPage, TotalRecord = totalRecord, Data = entity };
            }
        }
    }
}
