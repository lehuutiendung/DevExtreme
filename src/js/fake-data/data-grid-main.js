const data = {
    dataSource: [
        {
            ID: 0,
            PolicyName: "Chính sách CCTC LTMinh; Nhà máy Hưng Yên",
            EmployeeApply: "Tất cả các nhân viên trong đơn vị",
            PositionApply: "Tất cả các vị trí trong đơn vị",
            DepartmentApply: "CCTC LTMinh; Nhà máy Hưng Yên - Vĩnh Phúc",
            Status: "Đang áp dụng"
        },
        {
            ID: 1,
            PolicyName: "Chính sách Phòng Nhân sự - Tất cả các vị trí trong đơn vị",
            EmployeeApply: "Tất cả các nhân viên trong đơn vị",
            PositionApply: "Tất cả các vị trí trong đơn vị",
            DepartmentApply: "Phòng Nhân sự",
            Status: "Đang áp dụng"
        },
        {
            ID: 2,
            PolicyName: "Chính sách Văn phòng 10 - Tất cả các vị trí trong đơn vị",
            EmployeeApply: "Tất cả các nhân viên trong đơn vị",
            PositionApply: "Tất cả các vị trí trong đơn vị",
            DepartmentApply: "Văn phòng 10",
            Status: "Ngừng áp dụng"
        },
        {
            ID: 3,
            PolicyName: "Chính sách Văn phòng trung tâm - Tất cả các vị trí trong đơn vị",
            EmployeeApply: "Tất cả các nhân viên trong đơn vị",
            PositionApply: "Tất cả các vị trí trong đơn vị",
            DepartmentApply: "Văn phòng trung tâm",
            Status: "Ngừng áp dụng"
        },
        {
            ID: 4,
            PolicyName: "Chính sách các khoản mặc định của hệ thống (Ltminh test)",
            EmployeeApply: "Tất cả các nhân viên trong đơn vị",
            PositionApply: "Tất cả các vị trí trong đơn vị",
            DepartmentApply: "Công ty TNHH Kim Đồng (dự án C&B)",
            Status: "Đang áp dụng"
        }
    ],
    listHeader: [
        {
            DataField: "PolicyName",
            Caption: "Tên chính sách",
            Width: 400,
            Fixed: true,
        },
        {
            DataField: "EmployeeApply",
            Caption: "Nhân viên áp dụng",
            Width: 250,
            Fixed: false,
        },
        {
            DataField: "PositionApply",
            Caption: "Vị trí áp dụng",
            Width: 220,
            Fixed: false,
        },
        {
            DataField: "DepartmentApply",
            Caption: "Đơn vị áp dụng",
            Width: 300,
            Fixed: false,
        },
        {
            DataField: "Status",
            Caption: "Trạng thái",
            Width: 200,
            Fixed: false,
        }
    ],
}

export default data;
