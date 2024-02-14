const SYSTEM_ACCOUNT = {
    FILE_NAME: "Danh_sach_he_thong_tai_khoan",
    title: "Hệ thống tài khoản",
    textAccount: "tài khoản",
    placeholderSearch: "Tìm kiếm theo số, tên tài khoản",
    expand: "Mở rộng",
    unExpand: "Thu gọn",
    tranferAccount: "Chuyển tài khoản hạch toán",
    colTableName: {
        accountNumber: "Số tài khoản",
        accountName: "Tên tài khoản",
        nature: "Tính chất",
        accountNameEnglish: "Tên tiếng anh",
        explain: "Diễn giải",
        state: "Trạng thái",
        feature: "Chức năng"
    },
    valueState:{
        useAccount: "Sử dụng",
        usingAccount: "Đang sử dụng",
        stopUsingAccount: "Ngưng sử dụng"
    } ,
    deleteFailed : "Xóa không thành công",
    contentDeleteFailed : "Không thể xóa danh mục cha nếu chưa xóa danh mục con.",
    contentDelete: "Bạn có thực sự muốn xóa tài khoản <",
    textLogicToggleUsing: "Tài khoản cha đang ở trạng thái ngừng sử dụng. Bạn không thể thiết lập trạng thái sử dụng cho tài khoản con.",
    form: {
        accountProperty: ["AccountNumber", "AccountName", "AccountNameEnglish", "ParentId", "Nature", "Explain"],
        listNature: [
            { Id: 1, Nature: "Dư nợ" },
            { Id: 2, Nature: "Dư có" },
            { Id: 3, Nature: "Lưỡng tính" },
            { Id: 4, Nature: "Không có số dư" },
        ],
        listUserObject: [
            { UserObject: 1, UserObjectName: "Khách hàng" },
            { UserObject: 2, UserObjectName: "Nhà cung cấp" },
            { UserObject: 3, UserObjectName: "Nhân viên" },
        ],
        textProperty:{
            accountNumber: "Số tài khoản",
            accountName: "Tên tài khoản",
            accountNameEnglish: "Tên tiếng anh",
            accountGeneral: "Tài khoản tổng hợp",
            nature: "Tính chất",
            explain: "Diễn giải",
            foreignCurrencyAccounting: "Có hạch toán ngoại tệ",
            trackDetail: "Theo dõi chi tiết theo",
            obj: "Đối tượng",
            bankAccount: "Tài khoản ngân hàng",
            objTHCP: "Đối tượng THCP",
            construction: "Công trình",
            order: "Đơn đặt hàng",
            contractSale: "Hợp đồng bán",
            contractPurchase: "Hợp đồng mua",
            itemCP: "Khoản mục CP",
            unit: "Đơn vị",
            rawCode: "Mã thống kê",
            customer: "Khách hàng",
            provider: "Nhà cung cấp",
            employee: "Nhân viên",
        },
        validateNotNull: {
            "AccountNumber": "Số tài khoản không được để trống.",
            "AccountName": "Tên tài khoản không được để trống.",
            "Nature": "Tính chất không được để trống.",
            "ParentId": "Tài khoản tổng hợp không tồn tại"
        },
        maxLength: {
            "AccountNumber": {Limit: 20, Warning : "Số tài khoản tối đa 20 kí tự."},
            "AccountName": {Limit: 255, Warning : "Tên tài khoản tối đa 255 kí tự."},
            "AccountNameEnglish": {Limit: 255, Warning : "Tên tiếng anh tối đa 255 kí tự."},
            "ParentId": {Limit: 36, Warning : "Tài khoản tổng hợp tối đa 36 kí tự."},
            "Nature": {Limit: 255, Warning : "Tính chất tối đa 255 kí tự."},
            "Explain": {Limit: 10000, Warning : "Diễn giải tối đa 10000 kí tự"},
        },
        minLength:{
            "AccountNumber": {MinLength: 3, Warning: "Số tài khoản tối thiểu 3 kí tự."}
        },
        titleAdd: "Thêm tài khoản",
        titleEdit: "Sửa tài khoản",
    }
}

export default SYSTEM_ACCOUNT;