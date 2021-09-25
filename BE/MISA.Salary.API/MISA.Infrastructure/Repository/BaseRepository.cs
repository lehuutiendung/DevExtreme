using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.ApplicationCore.Interfaces.Repository;
using MISA.ApplicationCore.MISAAttribute;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Infrastructure.Repository
{
    public class BaseRepository<Entity> : IBaseRepository<Entity>
    {
        // Khai báo thông tin kết nối database
        protected IDbConnection _dbConnection;
        public readonly string _connectionString;

        public BaseRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SalaryDatabase");
        }

        #region Tương tác với database thực hiện lấy tất cả bản ghi
        /// <summary>
        /// Tương tác database lấy tất cả bản ghi
        /// </summary>
        /// <typeparam name="Entity"></typeparam>
        /// LHTDung - 23/09/2021
        /// <returns></returns>
        public List<object> GetAll<Entity>()
        {
            // Khởi tạo đối tượng kết nối với database
            using (_dbConnection = new MySqlConnection(_connectionString))
            {
                // Lấy dữ liệu
                var className = typeof(Entity).Name;
                var sqlCommand = $"Proc_Get{className}s";

                var resultObject = _dbConnection.Query<object>(sqlCommand, commandType: CommandType.StoredProcedure);
                return (List<object>)resultObject;
            }
        }
        #endregion

        #region Tương tác database lấy bản ghi theo id
        /// <summary>
        /// Tương tác database lấy bản ghi theo id
        /// </summary>
        /// <param name="entityId"></param>
        /// LHTDung - 23/09/2021
        /// <returns></returns>
        public object GetById(Guid entityId)
        {
            using (_dbConnection = new MySqlConnection(_connectionString))
            {
                //Lấy dữ liệu
                var className = typeof(Entity).Name;
                var sqlCommand = $"Proc_Get{className}_ById";

                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add($"@{className}Id", entityId);

                //Trả về object khách hàng (nhân viên)
                var result = _dbConnection.QueryFirstOrDefault<object>(sqlCommand, param: dynamicParameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        #endregion

        #region Tương tác với database thực hiện thêm mới
        /// <summary>
        /// Tương tác thực hiện thêm mới bản ghi
        /// </summary>
        /// <param name="entity"></param>
        /// Created: LHTDung - 23/9/2021
        /// <returns></returns>
        public int Add(Entity entity)
        {
            var className = typeof(Entity).Name;
            // Khởi tạo đối tượng kết nối với database
            using (_dbConnection = new MySqlConnection(_connectionString))
            {
                // Thêm dữ liệu
                var columnName = string.Empty;
                var columnValue = string.Empty;

                //Khai bao dynamic parameters
                var dynamicParam = new DynamicParameters();
                // Parameter cho {className}Code
                var dynamicParameters = new DynamicParameters();

                //Đọc từng property của object
                var properties = entity.GetType().GetProperties();

                //Duyệt từng property
                foreach (var item in properties)
                {
                    var propertyNotMap = item.GetCustomAttributes(typeof(MISANotMap), true);
                    if (propertyNotMap.Length == 0)
                    {
                        //Lấy tên của prop
                        var itemName = item.Name;

                        //Lấy value của prop
                        var itemValue = item.GetValue(entity);

                        if (itemName == $"{className}Id" && item.PropertyType == typeof(Guid))
                        {
                            // Sinh mã Guid cho EmployeeId
                            itemValue = Guid.NewGuid();
                        }

                        //Thêm param tương ứng với mỗi property
                        dynamicParam.Add($"@{itemName}", itemValue);

                        columnName += $"{itemName},";
                        columnValue += $"@{itemName},";
                    }

                }

                //Xóa dấu phẩy cuối cùng trong các dãy string columnName, columnValue
                columnName = columnName.Remove(columnName.Length - 1, 1);
                columnValue = columnValue.Remove(columnValue.Length - 1, 1);

                var sqlCommand = $"Proc_Insert{className}";
                var rowEffects = _dbConnection.Execute(sqlCommand, param: dynamicParam, commandType: CommandType.StoredProcedure);
                return rowEffects;
            }
        }
        #endregion

        #region Tương tác với database thực hiện cập nhật bản ghi
        /// <summary>
        /// Tương tác với database thực hiện cập nhật bản ghi
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="entity"></param>
        /// LHTDung - 23/09/2021
        /// <returns></returns>
        public int Update(Guid entityId, Entity entity)
        {
            var className = typeof(Entity).Name;
            // Khởi tạo đối tượng kết nối với database
            using (_dbConnection = new MySqlConnection(_connectionString))
            {
                //3. Thêm dữ liệu
                var columnName = string.Empty;
                var columnValue = string.Empty;

                //Khai bao dynamic parameters
                var dynamicParam = new DynamicParameters();

                //Lưu vào List các cặp giá trị (key,value)
                var queryValue = new List<string>();

                //Đọc từng property của object
                var properties = entity.GetType().GetProperties();

                //Duyệt từng property
                foreach (var item in properties)
                {
                    var propertyNotMap = item.GetCustomAttributes(typeof(MISANotMap), true);
                    if (propertyNotMap.Length == 0)
                    {
                        //Lấy tên của prop
                        var itemName = item.Name;

                        //Lấy value của prop
                        var itemValue = item.GetValue(entity);

                        //Thêm param tương ứng với mỗi property
                        dynamicParam.Add($"@{itemName}", itemValue);
                        queryValue.Add($"{itemName} = @{itemName}");
                    }
                }

                dynamicParam.Add($"@{className}Id", entityId);
                // Thực hiện truy vấn, thêm mới bản ghi
                var sqlCommand = $"Proc_Update{className}";

                var rowEffects = _dbConnection.Execute(sqlCommand, param: dynamicParam, commandType: CommandType.StoredProcedure);
                return rowEffects;
            }
        }
        #endregion

        #region Tương tác với database thực hiện xóa nhiều bản ghi
        /// <summary>
        /// Tương tác thực hiện xóa nhiều bản ghi
        /// </summary>
        /// <param name="listId"></param>
        /// LHTDung - 23/09/2021
        /// <returns></returns>
        public int DeleteMultiple(List<Guid> listId)
        {

            using (_dbConnection = new MySqlConnection(_connectionString))
            {
                _dbConnection.Open();
                //Biến kiểm soát roll back database khi có truy vấn không thành công
                var transaction = _dbConnection.BeginTransaction();
                //Lấy dữ liệu
                var className = typeof(Entity).Name;
                var arrayValue = new List<string>();

                foreach (var item in listId)
                {
                    var formatItem = "\'" + item + "\'";
                    arrayValue.Add(formatItem);
                };

                //Thực hiện truy vấn xóa dữ liệu
                var sqlCommand = $"DELETE FROM {className} WHERE {className}Id IN ({String.Join(", ", arrayValue.ToArray())})";
                var entityEffect = _dbConnection.Execute(sqlCommand, transaction: transaction);
                transaction.Commit();
                return entityEffect;
            }
        }
        #endregion

        #region Tương tác với database thực hiện filter, phân trang
        /// <summary>
        /// Phân trang, filter tìm kiếm theo Tên, Trạng thái, CCTC
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="filter"></param>
        /// <param name="status"></param>
        /// <param name="department"></param>
        /// Created: LHTDung - 23/09/2021
        /// <returns></returns>
        public object Filter(int pageSize, int pageNumber, string filter, int? status, string department)
        {
            //Khởi tạo đối tượng kết nối với database
            using (_dbConnection = new MySqlConnection(_connectionString))
            {
                DynamicParameters dynamicParameters = new DynamicParameters();

                //Tính toán giá trị Offset
                int offSet = pageSize * (pageNumber - 1);

                //Câu lệnh truy vấn phân trang và filter theo các tiêu chí
                var className = typeof(Entity).Name;
                var sqlCommand = $"Proc_Get{className}FilterPaging";

                dynamicParameters.Add($"@PageSize", pageSize);
                dynamicParameters.Add($"@PageNumber", pageNumber);
                dynamicParameters.Add($"@Filter", $"%{filter}%");
                dynamicParameters.Add($"@Status", status);
                dynamicParameters.Add($"@DepartmentName", $"%{department}%");
                dynamicParameters.Add($"@TotalPage", dbType: DbType.Int32, direction: ParameterDirection.Output);
                dynamicParameters.Add($"@TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);

                var entity = _dbConnection.Query<object>(sqlCommand,param: dynamicParameters, commandType: CommandType.StoredProcedure);
                var totalPage = dynamicParameters.Get<int>("@TotalPage");
                var totalRecord = dynamicParameters.Get<int>("@TotalRecord");
                return new { TotalPage = totalPage, TotalRecord = totalRecord, Data = entity };
            }
        }
        #endregion

    }
}
