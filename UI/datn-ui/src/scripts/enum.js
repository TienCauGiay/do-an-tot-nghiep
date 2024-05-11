const MSEnum = {
    STATUS: {
        // API đã xử lý yêu cầu thành công và trả về kết quả
        OK: 200,

        // Một tài nguyên mới đã được tạo thành công
        CREATED: 201,

        // Yêu cầu không hợp lệ
        BAD_REQUEST: 400,

        // Không có quyền truy cập vào tài nguyên hoặc không có thông tin chứng thực
        UNAUTHORIZED: 401,

        // Tài nguyên yêu cầu không được tìm thấy trên máy chủ
        NOT_FOUND: 404,

        // Xảy ra lỗi nội bộ trên máy chủ khi xử lý yêu cầu và không thể hoàn thành nó
        INTERNAL_SERVER_ERROR: 500
    },

    CHECK_STATUS: {
        // Kiểm tra trạng thái có phải 200 không
        isResponseStatusOk: (status) => {
            return status === 200;
        },

        // Kiểm tra trạng thái có phải 201 không
        isResponseStatusCreated: (status) => {
            return status === 201;
        },

        // Kiểm tra trạng thái có phải 400 không
        isResponseStatusBadRequest: (status) => {
            return status === 400;
        },

        // Kiểm tra trạng thái có phải 401 không
        isResponseStatusUnauthorized: (status) => {
            return status === 401;
        },

        // Kiểm tra trạng thái có phải 404 không
        isResponseStatusNotFound: (status) => {
            return status === 404;
        },

        // Kiểm tra trạng thái có phải 500 không
        isResponseStatusServerError: (status) => {
            return status === 500;
        }
    },

    // Khai báo dữ liệu phân trang mặc định
    RECORD: {
        // trang hiện tại 
        CURRENT_PAGE: 1,
        // Số trang hiển thị
        MAX_VISIBLE_PAGE: 3,
        // Số bản ghi trên trang
        RECORD_DEFAULT : 20,
        // Các option phân trang
        RECORD_OPTIONS : [10, 20, 30, 50, 100],
        // Chỉ số của trang đang được chọn
        INDEX_SELECTED_DEFAULT: 1,
    },

    // Giới tính
    GENDER: {
        // Nam
        Male: 0,
        // Nữ
        Female: 1,
        // Khác
        Other: 2
    },

    // Trạng thái của form detail của các obj
    FORM_MODE: {
        // Thêm
        Add: 1,
        // Sửa
        Edit: 2,
        // Xem
        View: 3
    },

    // Trạng thái button
    STATUS_BUTTON: {
        // Cất
        Save: 1,
        // Cất và thêm
        SaveAndAdd: 2,
    },

    // Mã ASCII
    KEY_CODE:{
        // Phím di chuyển lên
        DOWN: 40,
        // Phím di chuyển xuống
        UP: 38,
        // Phím Enter
        ENTER: 13,
        // Dấu trừ
        MINUS: 45,
        // Xóa 1 kí tự
        BACK_SPACE: 8,
    },

    // Check 1 trạng thái checkbox có là khách hàng trong form employee
    CUSTOMER: 1,
    // Check 1 trạng thái checkbox có là nhà cung cấp trong form employee
    PROVIDER: 1,

    // Trạng thái đúng sai
    BOOL: {
        TRUE: 1,
        FALSE: 0
    },
    
    // Đối tượng tài khoản trong form system account detail
    OBJ_ACCOUNT: {
        // Khách hàng
        Customer: 1,
        // Nhà cung cấp
        Provider: 2,
        // Nhân viên
        Employee: 3
    },

    // Trạng thái
    STATE: {
        // Sử dụng
        Using: 1,
        // Ngưng sử dụng
        StopUsing: 0
    },

    // Quy định trạng thái có cập nhật trạng thái sử dụng của các node con trong bảng tài khoản không
    UPDATE_CHILDREN: {
        // Có
        TRUE: 1,
        // Không
        FALSE: 0,
    },

    // Quy định loại phiếu (thu hay chi)
    RECEIPT_TYPE: {
        // Phiếu thu
        Receive: 0,
        // Phiếu chi
        Payment: 1,
    },

    // Quy định trạng thái khi sửa 1 danh sách đối tượng, xem nó mới được thêm, sửa, hay bị xóa đi
    STATUS_FLAG:{
        // Không thay đổi
        NoChange: 0,
        // Thêm mới
        Add: 1,
        // Sửa
        Update: 2,
        // Xóa
        Delete: 3,
    },

    PERMISSION: {
        // Không xác định
        None: 0,
        // Quản trị viên
        Admin: 1,
        // Sinh viên
        Student: 2,
        // Giảng viên
        Teacher: 3,
    },

    FILTER_OPTION: {
        // Không xác định
        None: 0,
        // Lọc theo lớp
        Class: 1, 
        // Lọc theo giới tính
        Gender: 2, 
        // Lọc theo tỉnh
        Address: 3, 
        // Lọc theo khoa 
        Faculty: 4,
        // Lọc theo mã sinh viên
        StudentCode: 5,
        // Lọc theo tên sinh viên
        StudentName: 6,
        // Lọc theo tên môn học
        SubjectName: 7,
        // Lọc theo tên giảng viên
        TeacherName: 8,
    },
} 

export default MSEnum;
