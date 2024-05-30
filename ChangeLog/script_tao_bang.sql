-- public.faculty definition

-- Drop table

-- DROP TABLE public.faculty;

CREATE TABLE public.faculty (
	faculty_id uuid NOT NULL,
	faculty_code text NULL,
	faculty_name text NULL,
	created_by text NULL,
	created_date date NULL,
	modified_by text NULL,
	modified_date date NULL,
	CONSTRAINT faculty_pkey PRIMARY KEY (faculty_id)
);


-- public.province_city definition

-- Drop table

-- DROP TABLE public.province_city;

CREATE TABLE public.province_city (
	province_city_id uuid NULL,
	province_city_code text NULL,
	province_city_name text NULL
);


-- public."role" definition

-- Drop table

-- DROP TABLE public."role";

CREATE TABLE public."role" (
	role_id uuid NOT NULL,
	role_name text NULL,
	created_by text NULL,
	created_date date NULL,
	modified_by text NULL,
	modified_date date NULL,
	role_code int4 NULL,
	CONSTRAINT role_pkey PRIMARY KEY (role_id)
);


-- public.semester definition

-- Drop table

-- DROP TABLE public.semester;

CREATE TABLE public.semester (
	semester_id uuid NOT NULL,
	semester_code text NULL,
	semester_name text NULL,
	created_by text NULL,
	created_date date NULL,
	modified_by text NULL,
	modified_date date NULL,
	CONSTRAINT semester_pkey PRIMARY KEY (semester_id)
);


-- public.classes definition

-- Drop table

-- DROP TABLE public.classes;

CREATE TABLE public.classes (
	classes_id uuid NOT NULL,
	faculty_id uuid NOT NULL,
	classes_code text NULL,
	classes_name text NULL,
	created_by text NULL,
	created_date date NULL,
	modified_by text NULL,
	modified_date date NULL,
	CONSTRAINT classes_pkey PRIMARY KEY (classes_id),
	CONSTRAINT fk_classes_faculty FOREIGN KEY (faculty_id) REFERENCES public.faculty(faculty_id)
);


-- public.student definition

-- Drop table

-- DROP TABLE public.student;

CREATE TABLE public.student (
	student_id uuid NOT NULL,
	classes_id uuid NOT NULL,
	student_code text NULL,
	student_name text NULL,
	birthday date NULL,
	gender text NULL,
	address text NULL,
	phone_number text NULL,
	email text NULL,
	image text NULL,
	created_by text NULL,
	created_date date NULL,
	modified_by text NULL,
	modified_date date NULL,
	admission_year date NULL,
	graduation_year date NULL,
	CONSTRAINT student_pkey PRIMARY KEY (student_id),
	CONSTRAINT fk_student_classes FOREIGN KEY (classes_id) REFERENCES public.classes(classes_id)
);


-- public.subject definition

-- Drop table

-- DROP TABLE public.subject;

CREATE TABLE public.subject (
	subject_id uuid NOT NULL,
	semester_id uuid NOT NULL,
	subject_code text NULL,
	subject_name text NULL,
	number_tc int4 NULL,
	created_by text NULL,
	created_date date NULL,
	modified_by text NULL,
	modified_date date NULL,
	CONSTRAINT subject_pkey PRIMARY KEY (subject_id),
	CONSTRAINT fk_subject_semester FOREIGN KEY (semester_id) REFERENCES public.semester(semester_id)
);


-- public.teacher definition

-- Drop table

-- DROP TABLE public.teacher;

CREATE TABLE public.teacher (
	teacher_id uuid NOT NULL,
	faculty_id uuid NOT NULL,
	teacher_code text NULL,
	teacher_name text NULL,
	birthday date NULL,
	gender text NULL,
	address text NULL,
	phone_number text NULL,
	email text NULL,
	image text NULL,
	created_by text NULL,
	created_date date NULL,
	modified_by text NULL,
	modified_date date NULL,
	CONSTRAINT teacher_pkey PRIMARY KEY (teacher_id),
	CONSTRAINT fk_teacher_faculty FOREIGN KEY (faculty_id) REFERENCES public.faculty(faculty_id)
);


-- public."user" definition

-- Drop table

-- DROP TABLE public."user";

CREATE TABLE public."user" (
	user_id uuid NOT NULL,
	user_name text NOT NULL,
	pass_word text NOT NULL,
	role_id uuid NOT NULL,
	status int4 NULL,
	created_by text NULL,
	created_date date NULL,
	modified_by text NULL,
	modified_date date NULL,
	CONSTRAINT users_pkey PRIMARY KEY (user_id),
	CONSTRAINT fk_user_role FOREIGN KEY (role_id) REFERENCES public."role"(role_id)
);


-- public.class_registration definition

-- Drop table

-- DROP TABLE public.class_registration;

CREATE TABLE public.class_registration (
	class_registration_id uuid NOT NULL,
	subject_id uuid NOT NULL,
	class_registration_code text NULL,
	class_registration_name text NULL,
	created_by text NULL,
	created_date date NULL,
	modified_by text NULL,
	modified_date date NULL,
	teacher_id uuid NULL,
	CONSTRAINT class_registration_pkey PRIMARY KEY (class_registration_id),
	CONSTRAINT fk_class_registration_subject FOREIGN KEY (subject_id) REFERENCES public.subject(subject_id),
	CONSTRAINT fk_class_registration_teacher FOREIGN KEY (teacher_id) REFERENCES public.teacher(teacher_id)
);


-- public.class_registration_detail definition

-- Drop table

-- DROP TABLE public.class_registration_detail;

CREATE TABLE public.class_registration_detail (
	class_registration_detail_id uuid NOT NULL,
	class_registration_id uuid NULL,
	student_id uuid NULL,
	student_code text NULL,
	student_name text NULL,
	CONSTRAINT class_registration_detail_pkey PRIMARY KEY (class_registration_detail_id),
	CONSTRAINT fk_class_registration_detail_class_registration FOREIGN KEY (class_registration_id) REFERENCES public.class_registration(class_registration_id)
);


-- public.score definition

-- Drop table

-- DROP TABLE public.score;

CREATE TABLE public.score (
	score_id uuid NOT NULL,
	student_id uuid NOT NULL,
	teacher_id uuid NOT NULL,
	class_registration_id uuid NOT NULL,
	score_attendance float8 NULL,
	score_test float8 NULL,
	score_exam float8 NULL,
	score_average float8 NULL,
	created_by text NULL,
	created_date date NULL,
	modified_by text NULL,
	modified_date date NULL,
	CONSTRAINT score_pkey PRIMARY KEY (score_id),
	CONSTRAINT fk_score_class_registration FOREIGN KEY (class_registration_id) REFERENCES public.class_registration(class_registration_id)
);