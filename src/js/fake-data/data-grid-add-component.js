// Fake data của bảng "Thêm thành phần"
const data = {
    dataSource: [
        {
            ID: 0,
            ComponentCode: "PHU_CAP_DI_LAI",
            ComponentName: "Phụ cấp đi lại",
            ComponentType: "Khác",
            Nature: "Thu nhập",
            Value: "",
            Description: "Mô tả"
        },
        {
            ID: 1,
            ComponentCode: "PHAT_DI_SOM_VE_MUON",
            ComponentName: "Phạt đi sớm về muộn",
            ComponentType: "Khác",
            Nature: "Thu nhập",
            Value: "",
            Description: "Mô tả"
        },
        {
            ID: 2,
            ComponentCode: "THANH_TIEN",
            ComponentName: "Thành tiền",
            ComponentType: "Thông tin nhân viên",
            Nature: "Thu nhập",
            Value: "NGAY_CONG*SO_SAN_PHAM_THUC_TE*HE_SO*DON_GIA",
            Description: "Mô tả"
        },
        {
            ID: 3,
            ComponentCode: "LUONG_SAN_PHAM",
            ComponentName: "Lương sản phẩm",
            ComponentType: "Thông tin nhân viên",
            Nature: "Thu nhập",
            Value: "HE_SO*DON_GIA*SO_SAN_PHAM_THUC_TE",
            Description: "Mô tả"
        },
        {
            ID: 4,
            ComponentCode: "GIAM_TRU_GIA_CANH",
            ComponentName: "Giảm trừ gia cảnh",
            ComponentType: "Thông tin nhân viên",
            Nature: "Thu nhập",
            Value: "11000000+4400000*SO_NGUOI_PHU_THUOC",
            Description: "Mô tả"
        },
        {
            ID: 5,
            ComponentCode: "TY_LE_HUONG_LUONG",
            ComponentName: "Tỷ lệ hưởng lương",
            ComponentType: "Thông tin nhân viên",
            Nature: "Thu nhập",
            Value: "",
            Description: "Mô tả"
        },
        {
            ID: 6,
            ComponentCode: "THUE_TNCN",
            ComponentName: "Thuế TNCN",
            ComponentType: "Thông tin nhân viên",
            Nature: "Thu nhập",
            Value: "",
            Description: "Mô tả"
        },  
    ],
    listHeader: [
        {
            DataField: "ComponentCode",
            Caption: "Mã thành phần",
            Width: 400,
            Fixed: true,
        },
        {
            DataField: "ComponentName",
            Caption: "Tên thành phần",
            Width: 200,
            Fixed: false,
        },
        {
            DataField: "ComponentType",
            Caption: "Loại thành phần",
            Width: 200,
            Fixed: false,
        },
        {
            DataField: "ComponentValue",
            Caption: "Giá trị",
            Width: 400,
            Fixed: false,
        },
    ],
}

export default data;