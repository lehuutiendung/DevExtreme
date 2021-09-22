using Dapper;
using Microsoft.Extensions.Configuration;
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
    public class BaseFacilityRepository<FacilityEntity> : IBaseFacilityRepository<FacilityEntity>
    {
        // Khai báo thông tin kết nối database
        protected IDbConnection _dbConnection;
        public readonly string _connectionString;

        public BaseFacilityRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SalaryDatabase");
        }

        #region Lấy tất cả bản ghi 
        /// <summary>
        /// Tương tác database thực hiện lấy tất cả bản ghi
        /// </summary>
        /// <returns></returns>
        public List<object> GetAll()
        {
            // Khởi tạo đối tượng kết nối với database
            using (_dbConnection = new MySqlConnection(_connectionString))
            {
                // Lấy dữ liệu
                var className = typeof(FacilityEntity).Name;
                var sqlCommand = $"Proc_Get{className}s";
                
                var resultObject = _dbConnection.Query<object>(sqlCommand, commandType:CommandType.StoredProcedure);
                return (List<object>)resultObject;
            }
        }
        #endregion


        #region Tương tác database lấy bản ghi theo id
        /// <summary>
        /// Tương tác database lấy bản ghi theo id
        /// </summary>
        /// <param name="facilityEntityId"></param>
        /// <returns></returns>
        public object GetById(Guid facilityEntityId)
        {
            using (_dbConnection = new MySqlConnection(_connectionString))
            {
                //Lấy dữ liệu
                var className = typeof(FacilityEntity).Name;
                var sqlCommand = $"Proc_Get{className}_ById";
               
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add($"@{className}IdParam", facilityEntityId);

                //Trả về object
                var result = _dbConnection.QueryFirstOrDefault<object>(sqlCommand, param: dynamicParameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        #endregion
    }
}
