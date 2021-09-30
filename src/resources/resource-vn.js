module.exports = {
    CTRL_S: "Lưu ( Ctrl + S)",
    ESC: "Đóng ( ESC)",
    CTRL_SHIFT_S: "Áp dụng ( Ctrl + Shift + S)",

    DuplicateText: "Nhân bản",
    DeleteText: "Xóa",
    UPDATE_TEXT: "Cập nhật",

    ButtonIconAddText: "Thêm mới",
    ButtonIconAddComponentText: "Thêm thành phần",
    ButtonIconApplyText: "Ngưng áp dụng",
    ButtonIconActiveApplyText: "Đang áp dụng",
    ButtonIconDeleteText: "Xóa",

    ChooseComponent: "Chọn thành phần",
    ButtonCancelText: "Hủy bỏ",
    ButtonCancelText_2: "Hủy",
    ButtonAgreeText: "Đồng ý",
    ButtonDontSaveText: "Không lưu",
    ButtonSaveText: "Lưu",
    ButtonEditText: "Sửa",
    ButtonApplyText: "Áp dụng",
    ButtonFilterCancel: "Bỏ lọc",
    Choosed: "Đã chọn",
    UnChecked: "Bỏ chọn",
    ValidateEmpty: "Không được để trống.",
    BorderValidate: "1px solid #ff6161",
    BorderNormal: "1px solid #e0e0e0",
    BorderActive: "1px solid #34b057",

    //Popup
    Notificate: "Thông báo",
    POPUP_EXIT_EDIT: "Bạn có muốn lưu lại các thông tin vừa nhập không?",
    POPUP_ADD: "Bạn phải khai báo ít nhất một thành phần lương trong chính sách này. Vui lòng kiểm tra lại",
    POPUP_DELETE: "Bạn có chắc muốn xóa chính sách lương ",
    POPUP_DELETE_MULTIPLE: "Bạn có chắc chắn muốn xóa chính sách lương đã chọn không?",
    POPUP_STOPAPPLY_MULTIPLE: "Bạn có chắc chắn muốn chuyển trạng thái các chính sách lương đã chọn sang ngừng áp dụng không?",
    POPUP_APPLY_MULTIPLE: "Bạn có chắc chắn muốn chuyển trạng thái các chính sách lương đã chọn sang đang áp dụng không?",
    POPUP_EXIT_EDIT_TYPE: 0,            //Popup cảnh báo với chỉnh sửa
    POPUP_ADD_TYPE: 1,                  //Popup cảnh báo với thêm mới
    POPUP_DELETE_TYPE: 2,               //Popup cảnh báo với xóa đơn
    POPUP_DELETE_MULTI_TYPE: 3,         //Popup cảnh báo với xóa nhiều 
    POPUP_STOPAPPLY_TYPE: 4,            //Popup cảnh báo với ngừng áp dụng
    POPUP_ACTIVEAPPLY_TYPE: 5,          //Popup cảnh báo với ngừng áp dụng 1 chính sách
    POPUP_STOPAPPLY_MULTI_TYPE: 6,      //Popup cảnh báo với ngừng áp dụng nhiều chính sách
    POPUP_APPLY_MULTI_TYPE: 7,          //Popup cảnh báo với đang áp dụng nhiều chính sách
    CLOSE: "Đóng",

    //Popover
    BACK_TEXT: "Quay lại",

    // Class của thư viện DevExtreme
    ClassFocus: "dx-state-focused",
    ClassEmpty: "dx-texteditor-empty",
    // Class custome
    BorderErrorValidate: 'border-error-validate',


    GeneralInformation: "THÔNG TIN CHUNG",
    SalaryComponent: "THÀNH PHẦN LƯƠNG",
    DepartmentApply: "Đơn vị áp dụng",
    PositionApply: "Vị trí áp dụng",
    EmployeeApply: "Nhân viên áp dụng",
    PolicyName: "Tên chính sách",
    StatusTextForm: "Trạng thái",
    FilterText: "Bộ lọc",
    SETTING_COLUMN: "Tùy chỉnh cột",

    // Thành phần lương
    ComponentNameTitle: "Tên thành phần",
    ComponentCodeTitle: "Mã thành phần",
    FormulaTitle: "Công thức",

    AllDepartment: "Tất cả đơn vị",
    AllPositionApply: "Tất cả các vị trí trong đơn vị",
    AllEmployeeApply: "Tất cả các nhân viên trong đơn vị",

    // URL API
    URL_DEPARTMENT_GETALL: "https://localhost:44330/api/Departments",
    URL_POSITION_GETALL: "https://localhost:44330/api/Positions",
    URL_EMPLOYEE_GETALL: "https://localhost:44330/api/Employees",
    URL_COMPONENT_GETALL: "https://localhost:44330/api/Components",
    URL_POLICY_GETALL: "https://localhost:44330/api/Policies",
    URL_POLICY_COMMON: "https://localhost:44330/api/Policies",

    POSITION: "Position",
    EMPLOYEE: "Employee",

    Status: [
        {Text: "Tất cả trạng thái", Value: ""},
        {Text: "Đang áp dụng", Value: 1},
        {Text: "Ngừng áp dụng", Value: 2}, 
    ],
    PagingNumber: [
        {Text: "15", Value: 15},
        {Text: "25", Value: 25},
        {Text: "50", Value: 50},
        {Text: "100", Value: 100},
    ],
    ComponentTypeDropdown: [
        {Text: "Tất cả thành phần", Value: ""},
        {Text: "Sản phẩm", Value: 1},
        {Text: "Thông tin nhân viên", Value: 2}, 
        {Text: "Khác", Value: 3}, 
    ],  

    // Loại dropdown:
    DropdownStatus: 0,          //0: Dropdown cho trạng thái
    DropdownPaging: 1,          //1: Dropdown cho phân trang
    
    // Chế độ màn hình
    MainScreen: 0,              //Màn hình chính hiển thị danh sách
    AddScreen: 1,               //Màn hình "Thêm chính sách lương" (sau khi click vào "thêm mới").
    ViewScreen: 2,              //Màn hình "Xem chi tiết"
    EditScreen: 3,              //Màn hình "Sửa"
    
    //Loại button icon
    ButtonIconAdd: 0,           //Button Icon Thêm mới
    ButtonIconApply: 1,         //Button Icon Ngưng áp dụng
    ButtonIconDelete: 2,        //Button Icon Xóa
    ButtonIconActiveApply: 3,   //Button Icon Đang áp dụng

    //Loại toast messenger
    TOAST_SUCCESS: 0,               //Toast thực hiện thành công

}