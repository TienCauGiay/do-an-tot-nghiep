/**
 * Table phân quyền người dùng
 */
create table role(
	role_id uuid primary key,
	role_code int,
	role_name text,
	created_by text,
	created_date date,
	modified_by text,
	modified_date date
); 

/**
 * Table tài khoản đăng nhập người dùng
 */
create table user(
	user_id uuid primary key,
	user_name text not null,
	pass_word text not null,
	role_id uuid not null,
	status int,
	created_by text,
	created_date date,
	modified_by text,
	modified_date date,
	CONSTRAINT fk_user_role FOREIGN KEY(role_id) REFERENCES role(role_id)
);

/**
 * Table thông tin học kỳ
 */
create table semester(
	semester_id uuid primary key,
	semester_code text,
	semester_name text,
	created_by text,
	created_date date,
	modified_by text,
	modified_date date
);

/**
 * Table thông tin môn học
 */
create table subject(
	subject_id uuid primary key,
	semester_id uuid not null,
	subject_code text,
	subject_name text,
	number_tc int,
	created_by text,
	created_date date,
	modified_by text,
	modified_date date,
	CONSTRAINT fk_subject_semester FOREIGN KEY(semester_id) REFERENCES semester(semester_id)
);

/**
 * Table thông tin khoa
 */
create table faculty(
	faculty_id uuid primary key,
	faculty_code text,
	faculty_name text,
	created_by text,
	created_date date,
	modified_by text,
	modified_date date
);

/**
 * Table thông tin lớp học
 */
create table classes(
	classes_id uuid primary key,
	faculty_id uuid not null,
	classes_code text,
	classes_name text,
	created_by text,
	created_date date,
	modified_by text,
	modified_date date,
	CONSTRAINT fk_classes_faculty FOREIGN KEY(faculty_id) REFERENCES faculty(faculty_id)
);

/**
 * Table thông tin sinh viên
 */
create table student(
	student_id uuid primary key,
	classes_id uuid not null,
	student_code text,
	student_name text,
	birthday date,
	gender text,
	address text,
	phone_number text,
	email text,
	image text,
	admission_year date,
	graduation_year date,
	created_by text,
	created_date date,
	modified_by text,
	modified_date date,
	CONSTRAINT fk_student_classes FOREIGN KEY(classes_id) REFERENCES classes(classes_id)
);

/**
 * Table thông tin giảng viên
 */
create table teacher(
	teacher_id uuid primary key,
	faculty_id uuid not null,
	teacher_code text,
	teacher_name text,
	birthday date,
	gender text,
	address text,
	phone_number text,
	email text,
	image text,
	created_by text,
	created_date date,
	modified_by text,
	modified_date date,
	CONSTRAINT fk_teacher_faculty FOREIGN KEY(faculty_id) REFERENCES faculty(faculty_id)
);

/**
 * Table Lớp học phần
 */
create table class_registration(
	class_registration_id uuid primary key,
	subject_id uuid not null,
	class_registration_code text,
	class_registration_name text,
	created_by text,
	created_date date,
	modified_by text,
	modified_date date, 
	CONSTRAINT fk_class_registration_subject FOREIGN KEY(subject_id) REFERENCES subject(subject_id)
);

/**
 * Table Thông tin điểm số
 */
create table score(
	score_id uuid primary key,
	student_id uuid not null,
	teacher_id uuid not null,
	class_registration_id uuid not null,
	score_attendance  float,
	score_test float,
	score_exam float,
	score_average float,
	created_by text,
	created_date date,
	modified_by text,
	modified_date date,
	-- CONSTRAINT fk_score_student FOREIGN KEY(student_id) REFERENCES student(student_id),
	-- CONSTRAINT fk_score_teacher FOREIGN KEY(teacher_id) REFERENCES teacher(teacher_id),
	CONSTRAINT fk_score_class_registration FOREIGN KEY(class_registration_id) REFERENCES class_registration(class_registration_id)
); 

INSERT into "role" VALUES 
('70ca8ff8-1a05-4e17-808a-693601d9f291', N'admin', N'Quản trị viên', 'Bùi Ngọc Tiến', now(), '', now()),
('e0ba5c53-0428-4a10-987b-13153c6f6943', N'student', N'Sinh viên', 'Bùi Ngọc Tiến', now(), '', now()),
('29ce9061-8846-458c-9af8-ec056e960c39', N'teacher', N'Giảng viên viên', 'Bùi Ngọc Tiến', now(), '', now());

select * from "role" r ;

INSERT into "user" VALUES 
(uuid_generate_v4(), N'admin', N'1', '70ca8ff8-1a05-4e17-808a-693601d9f291', 1, 'Bùi Ngọc Tiến', now(), '', now()),
(uuid_generate_v4(), N'bnt', N'1', 'e0ba5c53-0428-4a10-987b-13153c6f6943', 1, 'Bùi Ngọc Tiến', now(), '', now()),
(uuid_generate_v4(), N'dna', N'1', 'e0ba5c53-0428-4a10-987b-13153c6f6943', 0, 'Bùi Ngọc Tiến', now(), '', now()),
(uuid_generate_v4(), N'gvv', N'1', '29ce9061-8846-458c-9af8-ec056e960c39', 1, 'Bùi Ngọc Tiến', now(), '', now()),
(uuid_generate_v4(), N'htt', N'1', 'e0ba5c53-0428-4a10-987b-13153c6f6943', 1, 'Bùi Ngọc Tiến', now(), '', now()),
(uuid_generate_v4(), N'ndq', N'1', 'e0ba5c53-0428-4a10-987b-13153c6f6943', 1, 'Bùi Ngọc Tiến', now(), '', now()),
(uuid_generate_v4(), N'nks', N'1', '29ce9061-8846-458c-9af8-ec056e960c39', 1, 'Bùi Ngọc Tiến', now(), '', now()),
(uuid_generate_v4(), N'nth', N'1', '29ce9061-8846-458c-9af8-ec056e960c39', 1, 'Bùi Ngọc Tiến', now(), '', now()),
(uuid_generate_v4(), N'pdh', N'1', 'e0ba5c53-0428-4a10-987b-13153c6f6943', 1, 'Bùi Ngọc Tiến', now(), '', now()),
(uuid_generate_v4(), N'ptut', N'1', 'e0ba5c53-0428-4a10-987b-13153c6f6943', 1, 'Bùi Ngọc Tiến', now(), '', now()),
(uuid_generate_v4(), N'pvm', N'1', 'e0ba5c53-0428-4a10-987b-13153c6f6943', 1, 'Bùi Ngọc Tiến', now(), '', now()),
(uuid_generate_v4(), N'ttn', N'1', 'e0ba5c53-0428-4a10-987b-13153c6f6943', 1, 'Bùi Ngọc Tiến', now(), '', now());

select * from "user" u; 

INSERT into faculty VALUES 
('87f016b3-97bb-4c94-9f5a-f090720d056a', N'cd', N'Cầu đường', 'Bùi Ngọc Tiến', now(), '', now()),
('42b8e433-057f-4c0a-a6c6-b00aee581104', N'cntt', N'Công nghệ thông tin', 'Bùi Ngọc Tiến', now(), '', now()),
('c1b09d19-3df3-4176-a02a-3684a2da1ff1', N'ct', N'Công trình', 'Bùi Ngọc Tiến', now(), '', now()),
('41dbeac3-9caa-42cc-a970-1d91faeff0ad', N'ktvt', N'Kinh tế vận tải', 'Bùi Ngọc Tiến', now(), '', now()),
('476eb3c0-5690-4516-ba85-a36dd3b39567', N'lgt', N'Logistic', 'Bùi Ngọc Tiến', now(), '', now()),
('65b03530-61e5-4344-8d19-da3c3ac31e95', N'tdh', N'Tự động hóa', 'Bùi Ngọc Tiến', now(), '', now());
 
select * from faculty f; 

INSERT into classes VALUES 
(uuid_generate_v4(), '42b8e433-057f-4c0a-a6c6-b00aee581104', N'cntt1k61', N'Công nghệ thông tin 1 - K61'),
(uuid_generate_v4(), '42b8e433-057f-4c0a-a6c6-b00aee581104', N'cntt2k61', N'Công nghệ thông tin 2 - K61'),
(uuid_generate_v4(), '42b8e433-057f-4c0a-a6c6-b00aee581104', N'cntt3k61', N'Công nghệ thông tin 3 - K61'),
(uuid_generate_v4(), '42b8e433-057f-4c0a-a6c6-b00aee581104', N'cnttva', N'Công nghệ thông tin Việt - Anh - K61'),
(uuid_generate_v4(), '41dbeac3-9caa-42cc-a970-1d91faeff0ad', N'ktvt1k61', N'Kinh tế vận tải 1 - K61');

select * from classes c ;

insert into student VALUES 
(uuid_generate_v4(), '1b4f272b-6d36-4f43-a1d5-483863b6d32b', N'bnt', N'Bùi Ngọc Tiến', CAST(N'2001-05-17' AS Date), N'Nam', N'Sơn Để - Vân Du - Thạch Thanh- Thanh Hóa', N'0396827854', N'tiensd17052001@gmail.com', N'anh-dai-dien.jpg', 'Bùi Ngọc Tiến', now(), '', now()),
(uuid_generate_v4(), 'f4a4d4c6-f2e6-4444-8c05-2908e335ace4', N'dna', N'Đỗ Ngọc Anh', CAST(N'2002-10-10' AS Date), N'Nữ', N'Vân Du - Thanh Hóa', N'0328328325', N'anh@gmail.com', NULL, 'Bùi Ngọc Tiến', now(), '', now()),
(uuid_generate_v4(), '1b4f272b-6d36-4f43-a1d5-483863b6d32b', N'htt', N'Hà Tuấn Thành', CAST(N'2001-03-10' AS Date), N'Nam', N'Bá Thước - Thanh Hóa', N'038574942', N'thanh@gmail.com', N'anh-dai-dien-htt.jpg', 'Bùi Ngọc Tiến', now(), '', now()),
(uuid_generate_v4(), '1b4f272b-6d36-4f43-a1d5-483863b6d32b', N'ndq', N'Nguyễn Đức Quân', CAST(N'2001-09-07' AS Date), N'Nam', N'Thanh Hóa', N'098743943', N'quan@gmail.com', NULL, 'Bùi Ngọc Tiến', now(), '', now()),
(uuid_generate_v4(), '1b4f272b-6d36-4f43-a1d5-483863b6d32b', N'pdh', N'Phạm Duy Hưng', CAST(N'2002-06-01' AS Date), N'Nam', N'Nam Định', N'0943789344', N'hung@gmail.com', NULL, 'Bùi Ngọc Tiến', now(), '', now()),
(uuid_generate_v4(), '1b4f272b-6d36-4f43-a1d5-483863b6d32b', N'ptut', N'Phạm Thị Úy Thương', CAST(N'2002-01-01' AS Date), N'Nữ', N'Nam Đàn - Nghệ An', N'0943843943', N'thuong@gmail.com', NULL, 'Bùi Ngọc Tiến', now(), '', now()),
(uuid_generate_v4(), '1b4f272b-6d36-4f43-a1d5-483863b6d32b', N'pvm', N'Phạm Văn Minh', CAST(N'2000-01-01' AS Date), N'Nam', N'Thái Bình', N'0987932384', N'minh@gmail.com', NULL, 'Bùi Ngọc Tiến', now(), '', now()),
(uuid_generate_v4(), '1b4f272b-6d36-4f43-a1d5-483863b6d32b', N'ttn', N'Tôn Trung Nghĩa', CAST(N'2002-05-15' AS Date), N'Nam', N'Nghệ An', N'0983458654', N'nghia@gmail.com', NULL, 'Bùi Ngọc Tiến', now(), '', now());

select * from student s;

INSERT into semester VALUES 
(uuid_generate_v4(), N'hk1', N'Kỳ 1 năm nhất', 'Bùi Ngọc Tiến', now(), '', now()),
(uuid_generate_v4(), N'hk2', N'Kỳ 2 năm nhất', 'Bùi Ngọc Tiến', now(), '', now()),
(uuid_generate_v4(), N'hk3', N'Kỳ 1 năm hai', 'Bùi Ngọc Tiến', now(), '', now()),
(uuid_generate_v4(), N'hk4', N'Kỳ 2 năm hai', 'Bùi Ngọc Tiến', now(), '', now()),
(uuid_generate_v4(), N'hk5', N'Kỳ 1 năm ba', 'Bùi Ngọc Tiến', now(), '', now()),
(uuid_generate_v4(), N'hk6', N'Kỳ 2 năm ba', 'Bùi Ngọc Tiến', now(), '', now()),
(uuid_generate_v4(), N'hk7', N'Kỳ 1 năm bốn', 'Bùi Ngọc Tiến', now(), '', now()),
(uuid_generate_v4(), N'hk8', N'Kỳ 2 năm bốn', 'Bùi Ngọc Tiến', now(), '', now());

select * from semester s ;

INSERT into subject VALUES 
(uuid_generate_v4(), '64e31b35-c846-49e2-ae8b-545af9e4e975', N'ctdlgt', N'Cấu trúc dữ liệu và giải thuật', 3, 'Bùi Ngọc Tiến', now(), '', now()),
(uuid_generate_v4(), '1c7ed257-3aaf-4782-bca2-1a42e255c130', N'datn', N'Đồ án tốt nghiệp', 3, 'Bùi Ngọc Tiến', now(), '', now()),
(uuid_generate_v4(), 'c6428263-1e51-4bd8-a577-5758fc433319', N'dstt', N'Đại số tuyến tính', 2, 'Bùi Ngọc Tiến', now(), '', now()),
(uuid_generate_v4(), '37c105aa-d640-4121-8b6c-3c90e1e5f880', N'gt1', N'Giải tích 1', 3, 'Bùi Ngọc Tiến', now(), '', now()),
(uuid_generate_v4(), '5510c2ba-9ef0-453d-a62e-c9acbe5498d8', N'gt2', N'Giải tích 2', 3, 'Bùi Ngọc Tiến', now(), '', now()),
(uuid_generate_v4(), 'db6a44b9-a3eb-4b7b-ab72-1f75319ad371', N'ltapi', N'Lập trình API', 3, 'Bùi Ngọc Tiến', now(), '', now()),
(uuid_generate_v4(), '64e31b35-c846-49e2-ae8b-545af9e4e975', N'ltjv', N'Lập trình Java', 3, 'Bùi Ngọc Tiến', now(), '', now()),
(uuid_generate_v4(), '37c105aa-d640-4121-8b6c-3c90e1e5f880', N'ltnc', N'Lập trình nâng cao', 3, 'Bùi Ngọc Tiến', now(), '', now()),
(uuid_generate_v4(), 'db6a44b9-a3eb-4b7b-ab72-1f75319ad371', N'ltw', N'Lập trình WEB', 3, 'Bùi Ngọc Tiến', now(), '', now()),
(uuid_generate_v4(), '3d08eab9-46eb-488c-a5fc-06456bb16e7e', N'pttktt', N'Phân tích thiết kế thuật toán', 3, 'Bùi Ngọc Tiến', now(), '', now()),
(uuid_generate_v4(), 'db6a44b9-a3eb-4b7b-ab72-1f75319ad371', N'tacn', N'Tiếng anh chuyên ngành', 3, 'Bùi Ngọc Tiến', now(), '', now()),
(uuid_generate_v4(), 'c6428263-1e51-4bd8-a577-5758fc433319', N'thdc', N'Tin học đại cương', 3, 'Bùi Ngọc Tiến', now(), '', now()),
(uuid_generate_v4(), '3d08eab9-46eb-488c-a5fc-06456bb16e7e', N'tkcsdl', N'Thiết kế cơ sở dữ liệu', 2, 'Bùi Ngọc Tiến', now(), '', now()),
(uuid_generate_v4(), '5510c2ba-9ef0-453d-a62e-c9acbe5498d8', N'tkw', N'Thiết kế WEB', 2, 'Bùi Ngọc Tiến', now(), '', now()),
(uuid_generate_v4(), 'db6a44b9-a3eb-4b7b-ab72-1f75319ad371', N'ttvud', N'Thuật toán và ứng dụng', 3, 'Bùi Ngọc Tiến', now(), '', now());

select * from subject s ;

INSERT into teacher VALUES 
(uuid_generate_v4(), '04010b32-9b46-4e68-bac3-a465222a5c59', N'ntp', N'Nguyễn Trọng Phúc', CAST(N'1982-10-03' AS Date), N'Nam', N'Ninh Bình', N'0318731673', N'ntp@gmail.com', NULL, 'Bùi Ngọc Tiến', now(), '', now());

select * from teacher t;