import COMPONENT_CATEGORY from '@/scripts/resource_component/rc_category.js'
import SYSTEM_ACCOUNT from '@/scripts/resource_component/rc_account.js'
import PROVIDER from '@/scripts/resource_component/rc_provider.js'
import RECEIPT_PAYMENT from '@/scripts/resource_component/rc_receipt_payment.js'
import CASH from '@/scripts/resource_component/rc_cash.js'

const MSResource = {
    'vn-VI':{
        VALIDATE:{
            "EmployeeCode": "Mã nhân viên không được phép để trống",
            "FullName": "Họ tên không được phép để trống",
            "DepartmentName": "Đơn vị không được phép để trống",
            "DateOfBirth": "Ngày sinh lớn hơn ngày hiện tại",
            "IdentityDate": "Ngày cấp lớn hơn ngày hiện tại",
            "Email": "Email không đúng định dạng",
            "IdentityNumber": "Số chứng minh không đúng",
        },
        MAXLENGTH:{
            "EmployeeCode": {Limit: 20, Warning : "Mã nhân viên tối đa 20 kí tự"},
            "FullName": {Limit: 100, Warning : "Họ tên tối đa 100 kí tự"},
            "IdentityNumber": {Limit: 25, Warning : "Số chứng minh tối đa 25 kí tự"},
            "IdentityPlace": {Limit: 255, Warning : "Nơi cấp tối đa 255 kí tự"},
            "PositionName": {Limit: 255, Warning : "Chức danh tối đa 255 kí tự"},
            "Address": {Limit: 255, Warning : "Địa chỉ tối đa 255 kí tự"},
            "PhoneNumber": {Limit: 50, Warning : "Điện thoại di động tối đa 50 kí tự"},
            "PhoneLandline": {Limit: 50, Warning : "Điện thoại cố định tối đa 50 kí tự"},
            "Email": {Limit: 100, Warning : "Email tối đa 100 kí tự"},
            "BankAccount": {Limit: 50, Warning : "Số tài khoản tối đa 50 kí tự"},
            "BankName": {Limit: 255, Warning : "Tên ngân hàng tối đa 255 kí tự"},
            "BankBranch": {Limit: 255, Warning : "Chi nhánh tối đa 255 kí tự"},
        },
        TOOLTIP:{
            ALERT: "Thông báo",
            REFRESH: "Lấy lại dữ liệu",
            EXCEL: "Xuất dữ liệu",
            SETTING: "Các tiện ích và thiết lập",
            SETTING_MAIN: "Tùy chỉnh giao diện",
            DOWNLOAD: "Tải tệp excel",
            MESSENGER: "Trao đổi giữa DN và KTDV", 
            IDENTITY_NUMBER: "Số chứng minh nhân dân",
            BANK_BRANCH: "Chi nhánh tài khoản ngân hàng",
            PHONE_NUMBER: "Điện thoại di động",
            PHONE_LANDLINE: "Điện thoại cố định",
            HELP: "Giúp (F1)",
            EXIST: "Đóng (ESC)",
            SAVE: "Cất (Ctrl + S)",
            SAVE_AND_ADD:"Cất và thêm (Ctrl + Shift + S)"
        },
        Student_Column:{
            StudentCode: "MÃ SINH VIÊN",
            StudentName: "TÊN SINH VIÊN",
            ClassesCode: "MÃ LỚP",
            ClassesName: "TÊN LỚP",
            Birthday: "NGÀY SINH",
            Gender: "GIỚI TÍNH",
            Address: "ĐỊA CHỈ",
            PhoneNumber: "SỐ ĐIỆN THOẠI",
            Email: "EMAIL",
            Feature: "CHỨC NĂNG"
        },
        TEXT_CONTENT:{
            ManagementStudent: "Quản lí sinh viên",


            FILE_NAME: "Danh_sach_nhan_vien",
            NAME_COMPANY_SELECTED: "TRƯỜNG ĐẠI HỌC GIAO THÔNG VẬN TẢI",
            NAME_ACCOUNT_LOGIN: "Bùi Ngọc Tiến",
            ACCOUNTANT: "TRANG CHỦ",
            NO_DATA: "Không có dữ liệu",
            ITEM_SIDEBAR:{
                ManagementStudent: "Quản lí sinh viên",
                ManagementTeacher: "Quản lí giảng viên",
                ManagementScore: "Quản lí điểm",
                Statistic: "Báo cáo thống kê",
                ManagementAccount: "Quản lí tài khoản",
            },
            EMPLOYEE: "Nhân viên",
            ALL_CATEGORY: "Tất cả danh mục",
            UTILITIES: "Tiện ích",
            IMPORT_EXCEL: "Nhập từ excel",
            UTILITIES_SYNCHRONIZED: "Đồng bộ với AMIS hệ thống",
            EXCUTE_BATCH: "Thực hiện hàng loạt",
            ADD: "Thêm",
            PLACEHOLDER_SEARCH: "Tìm theo mã, tên nhân viên",
            PLACEHOLDER_SEARCH_HEADER: "Nhập từ khóa tìm kiếm",
            SUCCESS_DELETE : "Xóa thành công.",
            SUCCESS_CTEATE : "Thêm thành công.",
            SUCCESS_UPDATE : "Sửa thành công.",
            SUCCESS_DUPLICATE : "Nhân bản thành công.",
            SPLIT_DATE : "T",
            GENDER:{
                Male: "Nam",
                Female: "Nữ",
                Other: "Khác"
            },
            PAGE:{
                PREVIOUS: "previous",
                NEXT: "next",
                NUMBER: "number"
            },
            UPDATE:"Sửa",
            DUPLICATE: "Nhân bản",
            DELETE: "Xóa",
            TRANFER_ACCOUNT: "Chuyển tài khoản hạch toán",
            STOP_USING: "Ngưng sử dụng",
            TOTAL:"Tổng số",
            RECORD: "bản ghi",
            RECORD_ON_PAGE: "bản ghi trên 1 trang",
            PAGING_PRE: "Trước",
            PAGING_NEXT: "Sau",
            SCROLL:{
                START:"start",
                END:"end",
            },
            FUNCTION: "function",
            titleDialogSystemError: "Lỗi hệ thống!!!",
            contentDialogSystemError: "Vui lòng liên hệ Buif  để được trợ giúp."
        },
        FORM:{
            AddStudent:"Thêm sinh viên",
            UpdateStudent:"Sửa sinh viên", 
            StudentCode:"Mã sinh viên",
            StudentName:"Họ tên",
            Classes:"Lớp",
            Birthday:"Ngày sinh",
            Gender:"Giới tính",
            PlaceholderClasses:"-- Chọn lớp --", 
            Address:"Địa chỉ",
            PhoneNumber:"Điện thoại",
            Email:"Email",
            BANK_ACCOUNT:"Tài khoản ngân hàng",
            BANK_NAME:"Tên ngân hàng",
            BANK_BRANCH:"Chi nhánh",
            NOT_FOUND: "Không tìm thấy",
        },
        DIALOG:{
            TITLE:{
                CLOSE_RECORD: "Cất bản ghi?",
                CONFIRM_DELETE: "Xác nhận xóa?",
                DATA_INVALID: "Dữ liệu không hợp lệ!",
            },
            CONTENT:{
                CONFIRM_DELETE_PRE: "Bạn có thực sự muốn xóa nhân viên <",
                CONFIRM_DELETE_MULTIPLE: "Bạn có thực sự muốn xóa những nhân viên đã chọn không?",
                DATA_CHANGE: "Dữ liệu đã bị thay đổi. Bạn có muốn cất không?",
                EXIST_PRE: "Mã nhân viên",
                EXIST_END: "đã tồn tại trong hệ thống, vui lòng kiểm tra lại.",
                EXIST_DETAIL_END: "đã tồn tại trong hệ thống.",
                END: "> không?"
            },
        },
        BUTTON:{
            YES:"Có",
            NO:"Không",
            CANCEL:"Hủy",
            SAVE:"Cất",
            SAVE_AND_ADD:"Cất và Thêm",
            AGREE: "Đồng ý",
            CLOSE: "Đóng",
        },
        TOAST:{
            SUCCESS:"Thành công!",
            UNDO:"",
        },
        CATEGORY: COMPONENT_CATEGORY,
        ACCOUNT: SYSTEM_ACCOUNT,
        PROVIDER: PROVIDER,
        RECEIPT_PAYMENT: RECEIPT_PAYMENT,
        CASH: CASH,
    },
    'en-EN':{
        TEXT_CONTENT:{
            SUCCESS_DELETE : "Successful delele",
            SUCCESS_CTEATE : "Successful add",
            SUCCESS_UPDATE : "Successful update",
            SPLIT_DATE : "T",
            GENDER:{
                Male: "Male",
                Female: "Female",
                Other: "Other"
            },
            CODE_NOT_NULL: "Code cannot be blank.",
            NAME_NOT_NULL: "Fullname cannot be blank.",
            UNIT_NOT_NULL: "Unit cannot be blank.",
            CODE_END_MUST_NUMBER: "Code must end be number",
            DOB_ISVALID: "Date of birth is valid",
            CMNDDATE_ISVALID: "Invalid ID card issuance date",
            PAGE:{
                PREVIOUS: "previous",
                NEXT: "next",
                NUMBER: "number"
            }
        },
    },
    REGEX:{
        END_MUST_NUMBER:/\d$/,
        EMAIL:/^[^\s@]+@[^\s@]+\.[^\s@]+$/,
    }
}

export default MSResource