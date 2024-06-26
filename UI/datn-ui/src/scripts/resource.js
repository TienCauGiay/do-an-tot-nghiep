const MSResource = {
    'vn-VI':{
        VALIDATE:{
            "student_id": "Sinh viên không tồn tại trong hệ thống",
            "student_code": "Mã sinh viên không được để trống",
            "student_name": "Họ tên sinh viên không được để trống",
            "student_not_exists": "Không có sinh viên trong lớp học phần",
            "student_has_score": "Sinh viên đã có điểm của lớp học phần",
            "birthday": "Ngày sinh lớn hơn ngày hiện tại",
            "admission_year": "Ngày nhập học lớn hơn ngày hiện tại", 
            "graduation_year": "Ngày ra trường lớn hơn ngày hiện tại", 
            "email": "Email không đúng định dạng", 
            "teacher_id": "Giảng viên không tồn tại trong hệ thống",
            "teacher_code": "Mã giảng viên không được để trống",
            "teacher_name": "Tên giảng viên không được để trống",
            "class_registration_name": "Lớp học phần không được để trống",
            "user_name": "Tài khoản không được để trống",
            "pass_word": "Mật khẩu không được để trống",
            "faculty_id": "Khoa không tồn tại trong hệ thống",
            "faculty_code": "Mã khoa không được để trống",
            "faculty_name": "Tên khoa không được để trống",
            "classes_id": "Lớp học không tồn tại trong hệ thống",
            "classes_code": "Mã lớp không được để trống",
            "classes_name": "Lớp học không được để trống",
            "subject_id": "Môn học không tồn tại trong hệ thống",
            "subject_code": "Mã môn học không được để trống",
            "subject_name": "Môn học không được để trống",
            "semester_id": "Học kỳ không tồn tại trong hệ thống",
            "semester_name": "Học kỳ không được để trống",
            "class_registration_id": "Học phần không tồn tại trong hệ thống",
            "class_registration_code": "Mã lớp học phần không được để trống", 
            "StudentNotEmpty": "Bạn phải chọn ít nhất một sinh viên",
            "phone_number":"Điện thoại phải là số",
            "role_id": "Quyền truy cập không tồn tại trong hệ thống",
            "role_name": "Quyền truy cập không được để trống", 
            "score_attendance":  "Điểm chuyên cần không được để trống",
            "score_test": "Điểm kiểm tra không được để trống",
            "score_exam":  "Điểm thi không được để trống",
            "score_average": "Điểm trung bình không được để trống",
            "number_tc": "Số tín chỉ không được để trống",
            "graduation_year_invalid": "Ngày ra trường không được bé hơn ngày nhập học",
        },
        MAXLENGTH:{
            "student_code": {Limit: 50, Warning : "Mã sinh viên tối đa 50 kí tự"},
            "student_name": {Limit: 255, Warning : "Họ tên sinh viên tối đa 255 kí tự"}, 
            "address": {Limit: 500, Warning : "Địa chỉ tối đa 500 kí tự"},
            "phone_number": {Limit: 50, Warning : "Điện thoại di động tối đa 50 kí tự"}, 
            "email": {Limit: 100, Warning : "Email tối đa 100 kí tự"}, 
            "teacher_code": {Limit: 50, Warning : "Mã giảng viên tối đa 50 kí tự"},
            "teacher_name": {Limit: 255, Warning : "Tên giảng viên tối đa 255 kí tự"},
            "score_attendance":  {Limit: 10, Warning : "Điểm số tối đa 10 kí tự"},
            "score_test":  {Limit: 10, Warning : "Điểm số tối đa 10 kí tự"},
            "score_exam":  {Limit: 10, Warning : "Điểm số tối đa 10 kí tự"},
            "score_average":  {Limit: 10, Warning : "Điểm số tối đa 10 kí tự"},
            "faculty_code": {Limit: 50, Warning : "Mã khoa tối đa 50 kí tự"},
            "faculty_name": {Limit: 255, Warning : "Tên khoa tối đa 255 kí tự"}, 
            "classes_code": {Limit: 50, Warning : "Mã lớp tối đa 50 kí tự"},
            "classes_name": {Limit: 255, Warning : "Tên lớp tối đa 255 kí tự"}, 
            "subject_code": {Limit: 50, Warning : "Mã môn học tối đa 50 kí tự"},
            "subject_name": {Limit: 255, Warning : "Tên môn học tối đa 255 kí tự"}, 
            "class_registration_code": {Limit: 50, Warning : "Mã lớp học phần tối đa 50 kí tự"},
            "class_registration_name": {Limit: 255, Warning : "Tên lớp học phần tối đa 255 kí tự"}, 
            "number_tc": {Limit: 2, Warning : "Số tín chỉ tối đa 2 kí tự"}, 
        },
        NOT_NUMBER: {
            score_attendance: "Điểm chuyên cần không hợp lệ",
            score_test: "Điểm kiểm tra không hợp lệ",
            score_exam: "Điểm thi không hợp lệ",
            score_average: "Điểm trung bình không hợp lệ",
            number_tc: "Số tín chỉ phải là số",
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
        Teacher_Column:{
            TeacherCode: "MÃ GIẢNG VIÊN",
            TeacherName: "TÊN GIẢNG VIÊN",
            SubjectCode: "MÃ MÔN HỌC",
            SubjectName: "TÊN MÔN HỌC",
            Birthday: "NGÀY SINH",
            Gender: "GIỚI TÍNH",
            Address: "ĐỊA CHỈ",
            PhoneNumber: "SỐ ĐIỆN THOẠI",
            Email: "EMAIL",
            Feature: "CHỨC NĂNG"
        },
        Score_Column:{
            SubjectCode: "MÃ MH",
            SubjectName: "TÊN MÔN HỌC",
            NumberTC: "SỐ TÍN CHỈ",
            StudentCode: "MÃ SV",
            StudentName: "TÊN SINH VIÊN",
            TeacherCode: "MÃ GV",
            TeacherName: "TÊN GIẢNG VIÊN",
            ScoreAttendance: "ĐIỂM CC",
            TitleScoreAttendance: "ĐIỂM CHUYÊN CẦN",
            ScoreTest: "ĐIỂM KT",
            TitleScoreTest: "ĐIỂM KIỂM TRA",
            ScoreExam: "ĐIỂM THI",
            ScoreAverage: "ĐIỂM TB",
            TitleScoreAverage: "ĐIỂM TRUNG BÌNH",
            EvaluateState: "TRẠNG THÁI", 
            Feature: "CHỨC NĂNG"
        },
        User_Column:{
            UserName: "TÊN ĐĂNG NHẬP",
            RoleName: "QUYỀN TRUY CẬP",
            Status: "TRẠNG THÁI",
            Feature: "CHỨC NĂNG"
        },
        Faculty_Column: {
            FacultyCode: "MÃ KHOA",
            FacultyName: "TÊN KHOA",
            Feature: "CHỨC NĂNG",
        },
        ClassRegistration_Column: {
            ClassRegistrationCode: "MÃ HỌC PHẦN",
            ClassRegistrationName: "TÊN HỌC PHẦN",
            SubjectName: "MÔN HỌC",
            TeacherName: "GIẢNG VIÊN",
            Feature: "CHỨC NĂNG",
        },
        Class_Column: {
            ClassCode: "MÃ LỚP",
            ClassName: "TÊN LỚP",
            Feature: "CHỨC NĂNG",
        },
        Subject_Column: {
            SubjectCode: "MÃ MÔN HỌC",
            SubjectName: "TÊN MÔN HỌC",
            NumberTC: "SỐ TÍN CHỈ",
            Feature: "CHỨC NĂNG",
        },
        TEXT_CONTENT:{
            ManagementStudent: "Danh sách sinh viên",
            ManagementTeacher: "Danh sách giảng viên",
            ManagementFaculty: "Danh sách khoa",
            ManagementRegistration: "Danh sách lớp học phần",
            ManagementClass: "Danh sách lớp học",
            ManagementSubject: "Danh sách môn học",
            ManagementScore: "Danh sách điểm",
            ManagementUser: "Danh sách tài khoản",
            FILE_NAME: "Danh_sach_nhan_vien",
            NAME_COMPANY_SELECTED: "TRƯỜNG ĐẠI HỌC KEVIN",
            NAME_ACCOUNT_LOGIN: "Bùi Ngọc Tiến",
            LOGOUT: "Đăng xuất",
            ACCOUNTANT: "TRANG CHỦ",
            NO_DATA: "Không có dữ liệu",
            ITEM_SIDEBAR: [  
                {
                    Text: "Quản lý sinh viên",
                    Link: "/management-student",
                    Permission: [1], 
                },
                {
                    Text: "Quản lý giảng viên",
                    Link: "/management-teacher",
                    Permission: [1], 
                },
                {
                    Text: "Quản lý khoa",
                    Link: "/management-faculty",
                    Permission: [1], 
                },
                {
                    Text: "Quản lý lớp học",
                    Link: "/management-class",
                    Permission: [1], 
                },
                {
                    Text: "Quản lý môn học",
                    Link: "/management-subject",
                    Permission: [1], 
                },
                {
                    Text: "Quản lý lớp học phần",
                    Link: "/management-registration",
                    Permission: [1], 
                },
                {
                    Text: "Quản lý điểm",
                    Link: "/management-score",
                    Permission: [1, 2, 3], 
                },
                {
                    Text: "Báo cáo thống kê",
                    Link: "/statistic",
                    Permission: [1, 3], 
                },
                {
                    Text: "Quản lý tài khoản",
                    Link: "/management-user",
                    Permission: [1, 2, 3], 
                }
            ],
            EMPLOYEE: "Nhân viên",
            ALL_CATEGORY: "Tất cả danh mục",
            UTILITIES: "Tiện ích",
            IMPORT_EXCEL: "Nhập từ excel",
            UTILITIES_SYNCHRONIZED: "Đồng bộ với AMIS hệ thống",
            EXCUTE_BATCH: "Thực hiện hàng loạt",
            SelectFilterType: "Chọn hình thức lọc",
            SelectChart: "Chọn biểu đồ",
            SelectFilterCondition: "Chọn điều kiện lọc",
            DeleteFilterCondition: "Xóa điều kiện lọc",
            ADD: "Thêm",
            PLACEHOLDER_SEARCH: "Tìm theo mã, tên",
            PLACEHOLDER_SEARCH_HEADER: "Nhập từ khóa tìm kiếm",
            SUCCESS_DELETE : "Xóa thành công.",
            SUCCESS_MARK: "Cập nhật dữ liệu thành công",    
            NoRecorDelete : "Không có bản ghi nào bị xóa",
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
            MarkGraduated: "Đánh dấu đã tốt nghiệp",
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
            contentDialogSystemError: "Vui lòng liên hệ Buif  để được trợ giúp.",
            resetPassWork: "Lấy lại mật khẩu" ,
            resetPassWorkSuccess: "Lấy lại mật khẩu thành công" ,
        },
        FORM:{
            ScoreRate: "Tỉ lệ điểm quá trình/thi",
            PlaceholderScoreRate: "Chọn tỉ lệ tính điểm",
            AddStudent:"Thêm sinh viên",
            UpdateStudent:"Cập nhật thông tin sinh viên", 
            StudentCode:"Mã sinh viên",
            StudentName:"Họ tên",
            Classes:"Lớp",
            Birthday:"Ngày sinh",
            Gender:"Giới tính",
            PlaceholderClasses:"-- Chọn lớp --", 
            Address:"Địa chỉ",
            PhoneNumber:"Điện thoại",
            Email:"Email", 
            NOT_FOUND: "Không tìm thấy",
            AddTeacher: "Thêm giảng viên",
            UpdateTeacher: "Cập nhật thông tin giảng viên",
            TeacherCode: "Mã giảng viên",
            TeacherName: "Tên giảng viên",
            Subject: "Môn giảng dạy",
            PlaceholderSubject:"-- Chọn môn --", 
            Faculty: "Khoa",
            PlaceholderFaculty:"-- Chọn khoa --", 
            AddScore: "Nhập điểm",
            UpdateScore: "Sửa điểm",
            Student: "Sinh viên",
            Teacher: "Giảng viên",
            PlaceholderStudent:"-- Chọn sinh viên --", 
            PlaceholderTeacher:"-- Chọn giảng viên --",
            ScoreAttendance: "Điểm chuyên cần",
            ScoreTest: "Điểm kiểm tra",
            ScoreExam: "Điểm thi",
            ScoreAverage: "Điểm trung bình",
            AddUser: "Thêm tài khoản",
            UpdateUser: "Cập nhật thông tin tài khoản",
            Role: "Quyền truy cập",
            UserName: "Tên đăng nhập",
            PassWord: "Mật khẩu",
            PlaceholderRole: "Phân quyền",
            Status: "Trạng thái hoạt động",
            Active: "Sử dụng",
            InActive: "Ngưng sử dụng",
            ClassRegistration: "Lớp học phần",
            PlaceholderClassRegistration: "-- Chọn lớp học phần --",
            FacultyCode: "Mã khoa",
            FacultyName: "Tên khoa",
            AddFaculty: "Thêm khoa mới",
            UpdateFaculty: "Sửa thông tin khoa",
            AddRegistration: "Thêm lớp học phần",
            UpdateRegistration: "Sửa lớp học phần",
            ClassCode: "Mã lớp",
            ClassName: "Tên lớp",
            AddClass: "Thêm lớp học",
            UpdateClass: "Sửa thông tin lớp",
            SubjectCode: "Mã môn học",
            SubjectName: "Tên môn học",
            NumberTC: "Số tín chỉ",
            AddSubject: "Thêm môn học",
            UpdateSubject: "Sửa môn học",
            Semester: "Học kỳ",
            PlaceholderSemeter: "-- Chọn học kỳ --",
            RegistrationCode: "Mã lớp học phần",
            RegistrationName: "Tên lớp học phần",
            SubjectRegistration: "Môn học",
            AdmissionYear: "Ngày nhập học",
            GraduationYear: "Ngày ra trường",
        },
        DIALOG:{
            TITLE:{
                CLOSE_RECORD: "Cất bản ghi?",
                CONFIRM_DELETE: "Xác nhận xóa?",
                DATA_INVALID: "Dữ liệu không hợp lệ!",
                LOGIN_FAILED: "Đăng nhập không thành công",
            },
            CONTENT:{
                CONFIRM_DELETE_PRE: "Bạn có thực sự muốn xóa bản ghi này không?",
                CONFIRM_DELETE_MULTIPLE: "Bạn có thực sự muốn xóa những bản ghi đã chọn không?",
                DATA_CHANGE: "Dữ liệu đã bị thay đổi. Bạn có muốn cất không?",
                EXIST_PRE: "Mã",
                EXIST_END: "đã tồn tại trong hệ thống, vui lòng kiểm tra lại.",
                EXIST_DETAIL_END: "đã tồn tại trong hệ thống.",
                END: "> không?",
                UserName: "Tên đăng nhập",
                Arise: "Dữ liệu đã có phát sinh, không thể xóa",
            },
        },
        BUTTON:{
            YES:"Có",
            NO:"Không",
            CANCEL:"Hủy",
            SAVE:"Lưu",
            SAVE_AND_ADD:"Cất và Thêm",
            AGREE: "Đồng ý",
            CLOSE: "Đóng",
        },
        TOAST:{
            SUCCESS:"Thành công!",
            UNDO:"",
        }, 
        Chart:{ 
            StatisticNumberStudent: "Biểu đồ thống kê số sinh viên đầu vào/đầu ra trong 6 năm trở lại đây",
            AvgScoreByClassRegistration: "Biểu đồ thống kê điểm trung bình sinh viên theo lớp học phần",
        },
        LOGIN: {
            LoginText: "Đăng nhập",
            UserName: "Tài khoản",
            PassWord: "Mật khẩu",
        },
        IMPORT: {
            Success: 'Nhập khẩu thành công',
            Falured: 'Nhập khẩu không thành công',
            ErrorSystem: 'Lỗi hệ thống, vui lòng liên hệ Bùi Ngọc Tiến',
        }
    }, 
    REGEX:{
        END_MUST_NUMBER:/\d$/,
        EMAIL:/^[^\s@]+@[^\s@]+\.[^\s@]+$/,
    }
}

export default MSResource