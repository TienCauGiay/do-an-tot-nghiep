CREATE OR REPLACE FUNCTION public.func_check_exists_in_class_registration(p_class_registration_id uuid, p_teacher_id uuid, p_student_ids text)
 RETURNS TABLE(student_id uuid, student_code text, student_name text)
 LANGUAGE plpgsql
AS $function$
DECLARE  
begin
	/**
	 * Bảng tạm kết quả
	 */
	drop table if exists tmp_result;
	create temp table tmp_result
	(
		student_id uuid,
		student_code text,
		student_name text
	); 

	/*
	 * Tạo bảng tạm lưu các student_id
	 */
	drop table if exists tmp_ids;
	create temp table tmp_ids(student_id uuid);
	insert into tmp_ids (student_id)
	select unnest(string_to_array(p_student_ids, ';'))::uuid;
	
	/*
	 * Tạo bảng lưu các student_id không tồn tại trong lớp học phần
	 */
	drop table if exists tmp_student_id_not_exists;
	create temp table tmp_student_id_not_exists(student_id uuid);
	insert into tmp_student_id_not_exists(student_id)
	select 
		ti.student_id 
	from 
		tmp_ids ti
	where 
		ti.student_id not in
		(
			select
				crd.student_id	
			from 
				class_registration cr 
			inner join 
				class_registration_detail crd
			on 
				cr.class_registration_id = crd.class_registration_id 
			where 
				cr.class_registration_id = p_class_registration_id
				and cr.teacher_id = p_teacher_id
		);
	
	insert into tmp_result
	select 
		s.student_id,
		s.student_code,
		s.student_name
	from 
		student s 
	inner join 
		tmp_student_id_not_exists t
	on s.student_id = t.student_id;  
	
	RETURN QUERY select distinct * from tmp_result;
END;
$function$
;

CREATE OR REPLACE FUNCTION public.func_check_exists_in_score(p_class_registration_id uuid, p_teacher_id uuid, p_student_ids text)
 RETURNS TABLE(student_id uuid, student_code text, student_name text)
 LANGUAGE plpgsql
AS $function$
DECLARE  
begin
	/**
	 * Bảng tạm kết quả
	 */
	drop table if exists tmp_result;
	create temp table tmp_result
	(
		student_id uuid,
		student_code text,
		student_name text
	); 

	/*
	 * Tạo bảng tạm lưu các student_id
	 */
	drop table if exists tmp_ids;
	create temp table tmp_ids(student_id uuid);
	insert into tmp_ids (student_id)
	select unnest(string_to_array(p_student_ids, ';'))::uuid;
	
	/*
	 * Tạo bảng lưu các student_id không tồn tại trong lớp học phần
	 */
	drop table if exists tmp_student_id_exists;
	create temp table tmp_student_id_exists(student_id uuid);
	insert into tmp_student_id_exists(student_id)
	select 
		ti.student_id 
	from 
		tmp_ids ti
	where 
		ti.student_id in
		(
			select distinct 
				s.student_id	
			from 
				score s 
			where 
				s.class_registration_id = p_class_registration_id
				and s.teacher_id = p_teacher_id
		);
	
	insert into tmp_result
	select 
		s.student_id,
		s.student_code,
		s.student_name
	from 
		student s 
	inner join 
		tmp_student_id_exists t
	on s.student_id = t.student_id;  
	
	RETURN QUERY select distinct * from tmp_result;
END;
$function$
;

CREATE OR REPLACE FUNCTION public.func_get_by_student_id_score_view(p_student_id uuid)
 RETURNS TABLE(score_id uuid, student_id uuid, student_code text, student_name text, teacher_id uuid, teacher_code text, teacher_name text, class_registration_id uuid, class_registration_code text, class_registration_name text, subject_code text, subject_name text, number_tc integer, score_attendance double precision, score_test double precision, score_exam double precision, score_average double precision, evaluate_state integer)
 LANGUAGE plpgsql
AS $function$
DECLARE 
begin
	/**
	 * Bảng tạm kết quả
	 */
	drop table if exists tmp_result;
	create temp table tmp_result
	(
		score_id uuid, 
		student_id uuid,
		student_code text,
		student_name text,
		teacher_id uuid,
		teacher_code text,
		teacher_name text,
		class_registration_id uuid,
		class_registration_code text,
		class_registration_name text,
		subject_code text,
		subject_name text,
		number_tc int,
		score_attendance float8,
		score_test float8,
		score_exam float8,
		score_average float8,
		evaluate_state int
	);

	insert into tmp_result 
	select 
		sc.score_id,
		sc.student_id,
		st.student_code,
		st.student_name,
		sc.teacher_id,
		tc.teacher_code,
		tc.teacher_name,
		sc.class_registration_id,
		cr.class_registration_code,
		cr.class_registration_name,
		sb.subject_code,
		sb.subject_name,
		sb.number_tc,
		sc.score_attendance,
		sc.score_test,
		sc.score_exam,
		sc.score_average,
		case when sc.score_average > 4 then 1 else 2 end as evaluate_state
	from score sc
		inner join student st on sc.student_id = st.student_id 
		inner join teacher tc on sc.teacher_id = tc.teacher_id 
		inner join class_registration cr on sc.class_registration_id = cr.class_registration_id
		inner join subject sb on sb.subject_id = cr.subject_id
	where sc.student_id = p_student_id
	order by 
		sc.student_id, sb.subject_code
	;
	
	RETURN QUERY select * from tmp_result;
END;
$function$
;

CREATE OR REPLACE FUNCTION public.func_get_class_average_score()
 RETURNS TABLE(class_name text, average_score double precision)
 LANGUAGE plpgsql
AS $function$ 
BEGIN
    -- Bảng tạm kết quả
    DROP TABLE IF EXISTS tmp_result;
    CREATE TEMP TABLE IF NOT EXISTS tmp_result
    (
        class_name text,
        average_score float8
    );

    -- Chèn dữ liệu vào bảng tạm
    INSERT INTO tmp_result 
    SELECT
        cr.class_registration_name as class_name,
        AVG(s.score_average) AS average_score
    FROM
        public.score s
    inner JOIN
        public.class_registration cr ON s.class_registration_id = cr.class_registration_id
    GROUP BY
        cr.class_registration_name
    ORDER BY
        cr.class_registration_name;

    -- Trả về kết quả từ bảng tạm
    RETURN QUERY SELECT * FROM tmp_result;
END;
$function$
;

CREATE OR REPLACE FUNCTION public.func_get_filter_paging_by_role_score(p_limit integer, p_offset integer, p_text_search text, p_custom_filter text, p_user_id uuid)
 RETURNS TABLE(score_id uuid, student_id uuid, student_code text, student_name text, teacher_id uuid, teacher_code text, teacher_name text, class_registration_id uuid, class_registration_code text, class_registration_name text, subject_code text, subject_name text, number_tc integer, score_attendance double precision, score_test double precision, score_exam double precision, score_average double precision, evaluate_state integer)
 LANGUAGE plpgsql
AS $function$
DECLARE 
	v_query text;
begin 
	-- Xây dựng câu lệnh SELECT để lấy dữ liệu từ bảng tạm
	v_query := '
    SELECT 
        sc.score_id,
		sc.student_id,
		st.student_code,
		st.student_name,
		sc.teacher_id,
		tc.teacher_code,
		tc.teacher_name,
		sc.class_registration_id,
		cr.class_registration_code,
		cr.class_registration_name,
		sb.subject_code,
		sb.subject_name,
		sb.number_tc,
		sc.score_attendance,
		sc.score_test,
		sc.score_exam,
		sc.score_average,
		case when sc.score_average > 4 then 1 else 2 end as evaluate_state
    FROM score sc
    	inner join student st on sc.student_id = st.student_id 
		inner join public.user u on st.student_code = u.user_name 
		inner join teacher tc on sc.teacher_id = tc.teacher_id 
		inner join class_registration cr on sc.class_registration_id = cr.class_registration_id 
		inner join subject sb on sb.subject_id = cr.subject_id
    WHERE 
        (
			st.student_code ILIKE ''%' || p_text_search || '%''
        	OR st.student_name ILIKE ''%' || p_text_search || '%''
			OR tc.teacher_code ILIKE ''%' || p_text_search || '%''
			OR tc.teacher_name ILIKE ''%' || p_text_search || '%''
			OR sb.subject_code ILIKE ''%' || p_text_search || '%''
			OR sb.subject_name ILIKE ''%' || p_text_search || '%''
		)
        AND (' || p_custom_filter || ')
		and u.user_id = ''' || p_user_id || '''
    ORDER BY 
        st.student_code, sb.subject_code
    LIMIT 
        ' || p_limit || ' OFFSET ' || (p_offset - 1) * p_limit;  
           
	-- Thực hiện câu lệnh SELECT và trả về kết quả
    RETURN QUERY EXECUTE v_query;
END;
$function$
;

CREATE OR REPLACE FUNCTION public.func_get_filter_paging_by_role_teacher_score(p_limit integer, p_offset integer, p_text_search text, p_custom_filter text, p_user_id uuid)
 RETURNS TABLE(score_id uuid, student_id uuid, student_code text, student_name text, teacher_id uuid, teacher_code text, teacher_name text, class_registration_id uuid, class_registration_code text, class_registration_name text, subject_code text, subject_name text, number_tc integer, score_attendance double precision, score_test double precision, score_exam double precision, score_average double precision, evaluate_state integer)
 LANGUAGE plpgsql
AS $function$
DECLARE 
	v_query text;
begin 
	-- Xây dựng câu lệnh SELECT để lấy dữ liệu từ bảng tạm
	v_query := '
    SELECT 
        sc.score_id,
		sc.student_id,
		st.student_code,
		st.student_name,
		sc.teacher_id,
		tc.teacher_code,
		tc.teacher_name,
		sc.class_registration_id,
		cr.class_registration_code,
		cr.class_registration_name,
		sb.subject_code,
		sb.subject_name,
		sb.number_tc,
		sc.score_attendance,
		sc.score_test,
		sc.score_exam,
		sc.score_average,
		case when sc.score_average > 4 then 1 else 2 end as evaluate_state
    FROM score sc
		inner join teacher tc on sc.teacher_id = tc.teacher_id  
		inner join public.user u on tc.teacher_code = u.user_name  
		inner join student st on sc.student_id = st.student_id 
		inner join class_registration cr on sc.class_registration_id = cr.class_registration_id 
		inner join subject sb on sb.subject_id = cr.subject_id
    WHERE 
        (
			st.student_code ILIKE ''%' || p_text_search || '%''
        	OR st.student_name ILIKE ''%' || p_text_search || '%''
			OR tc.teacher_code ILIKE ''%' || p_text_search || '%''
			OR tc.teacher_name ILIKE ''%' || p_text_search || '%''
			OR sb.subject_code ILIKE ''%' || p_text_search || '%''
			OR sb.subject_name ILIKE ''%' || p_text_search || '%''
		)
        AND (' || p_custom_filter || ')
		and u.user_id = ''' || p_user_id || '''
    ORDER BY 
        st.student_code, sb.subject_code
    LIMIT 
        ' || p_limit || ' OFFSET ' || (p_offset - 1) * p_limit;  
           
	-- Thực hiện câu lệnh SELECT và trả về kết quả
    RETURN QUERY EXECUTE v_query;
END;
$function$
;

CREATE OR REPLACE FUNCTION public.func_get_filter_paging_by_role_user(p_limit integer, p_offset integer, p_text_search text, p_user_id uuid)
 RETURNS TABLE(user_id uuid, user_name text, pass_word text, role_id uuid, role_name text, status integer)
 LANGUAGE plpgsql
AS $function$
DECLARE  
begin
	/**
	 * Bảng tạm kết quả
	 */
	drop table if exists tmp_result;
	create temp table tmp_result
	(
		user_id uuid,
		user_name text,
		pass_word text,
		role_id uuid,
		role_name text,
		status int4
	);

	insert into tmp_result 
	select 
		u.user_id,
		u.user_name,
		u.pass_word,
		u.role_id,
		r.role_name,
		u.status
	from "user" u
	inner join "role" r on u.role_id = r.role_id 
	where 
		u.user_name ilike '%' || p_text_search || '%'
		and u.user_id = p_user_id
	order by 
		u.modified_date desc
	limit p_limit offset (p_offset - 1)* p_limit
	;
	
	RETURN QUERY select * from tmp_result;
END;
$function$
;

CREATE OR REPLACE FUNCTION public.func_get_filter_paging_class(p_limit integer, p_offset integer, p_text_search text)
 RETURNS TABLE(classes_id uuid, classes_code text, classes_name text, faculty_id uuid, faculty_name text)
 LANGUAGE plpgsql
AS $function$
DECLARE  
begin
	/**
	 * Bảng tạm kết quả
	 */
	drop table if exists tmp_result;
	create temp table tmp_result
	(
		classes_id uuid,
		classes_code text,
		classes_name text,
		faculty_id uuid,
		faculty_name text
	);

	insert into tmp_result 
	select 
		u.classes_id,
		u.classes_code,
		u.classes_name,
		f.faculty_id,
		f.faculty_name 
	from classes u
	inner join faculty f on u.faculty_id = f.faculty_id  
	where 
		u.classes_code ilike '%' || p_text_search || '%'
		or u.classes_name ilike '%' || p_text_search || '%'
	order by 
		u.modified_date desc
	limit p_limit offset (p_offset - 1)* p_limit
	;
	
	RETURN QUERY select * from tmp_result;
END;
$function$
;

CREATE OR REPLACE FUNCTION public.func_get_filter_paging_class_registration(p_limit integer, p_offset integer, p_text_search text)
 RETURNS TABLE(class_registration_id uuid, class_registration_code text, class_registration_name text, subject_id uuid, subject_name text, teacher_id uuid, teacher_name text)
 LANGUAGE plpgsql
AS $function$
DECLARE  
begin
	/**
	 * Bảng tạm kết quả
	 */
	drop table if exists tmp_result;
	create temp table tmp_result
	(
		class_registration_id uuid,
		class_registration_code text,
		class_registration_name text,
		subject_id uuid,
		subject_name text,
		teacher_id uuid,
		teacher_name text
	);

	insert into tmp_result 
	select 
		u.class_registration_id,
		u.class_registration_code,
		u.class_registration_name,
		u.subject_id,
		s.subject_name,
		u.teacher_id,
		t.teacher_name
	from class_registration u
	inner join subject s on u.subject_id = s.subject_id 
	inner join teacher t on u.teacher_id = t.teacher_id 
	where 
		u.class_registration_code ilike '%' || p_text_search || '%'
		or u.class_registration_name ilike '%' || p_text_search || '%'
		or s.subject_name ilike '%' || p_text_search || '%'
		or t.teacher_name ilike '%' || p_text_search || '%'
	order by 
		u.modified_date desc
	limit p_limit offset (p_offset - 1)* p_limit
	;
	
	RETURN QUERY select distinct * from tmp_result;
END;
$function$
;

CREATE OR REPLACE FUNCTION public.func_get_filter_paging_faculty(p_limit integer, p_offset integer, p_text_search text)
 RETURNS TABLE(faculty_id uuid, faculty_code text, faculty_name text)
 LANGUAGE plpgsql
AS $function$
DECLARE  
begin
	/**
	 * Bảng tạm kết quả
	 */
	drop table if exists tmp_result;
	create temp table tmp_result
	(
		faculty_id uuid,
		faculty_code text,
		faculty_name text
	);

	insert into tmp_result 
	select 
		u.faculty_id,
		u.faculty_code,
		u.faculty_name
	from faculty u
	where 
		u.faculty_code ilike '%' || p_text_search || '%'
		or u.faculty_name ilike '%' || p_text_search || '%'
	order by 
		u.modified_date desc
	limit p_limit offset (p_offset - 1)* p_limit
	;
	
	RETURN QUERY select * from tmp_result;
END;
$function$
;

CREATE OR REPLACE FUNCTION public.func_get_filter_paging_score(p_limit integer, p_offset integer, p_text_search text, p_custom_filter text)
 RETURNS TABLE(score_id uuid, student_id uuid, student_code text, student_name text, teacher_id uuid, teacher_code text, teacher_name text, class_registration_id uuid, class_registration_code text, class_registration_name text, subject_code text, subject_name text, number_tc integer, score_attendance double precision, score_test double precision, score_exam double precision, score_average double precision, evaluate_state integer)
 LANGUAGE plpgsql
AS $function$
DECLARE 
	v_query text;
begin 
	-- Xây dựng câu lệnh SELECT để lấy dữ liệu từ bảng tạm
    v_query := '
        SELECT 
            sc.score_id,
			sc.student_id,
			st.student_code,
			st.student_name,
			sc.teacher_id,
			tc.teacher_code,
			tc.teacher_name,
			sc.class_registration_id,
			cr.class_registration_code,
			cr.class_registration_name,
			sb.subject_code,
			sb.subject_name,
			sb.number_tc,
			sc.score_attendance,
			sc.score_test,
			sc.score_exam,
			sc.score_average,
			case when sc.score_average > 4 then 1 else 2 end as evaluate_state
        FROM score sc
        	inner join student st on sc.student_id = st.student_id 
			inner join teacher tc on sc.teacher_id = tc.teacher_id 
			inner join class_registration cr on sc.class_registration_id = cr.class_registration_id 
			inner join subject sb on sb.subject_id = cr.subject_id
        WHERE 
            (
				st.student_code ILIKE ''%' || p_text_search || '%''
            	OR st.student_name ILIKE ''%' || p_text_search || '%''
				OR tc.teacher_code ILIKE ''%' || p_text_search || '%''
				OR tc.teacher_name ILIKE ''%' || p_text_search || '%''
				OR sb.subject_code ILIKE ''%' || p_text_search || '%''
				OR sb.subject_name ILIKE ''%' || p_text_search || '%''
			)
            AND (' || p_custom_filter || ')
        ORDER BY 
            st.student_code, sb.subject_code
        LIMIT 
            ' || p_limit || ' OFFSET ' || (p_offset - 1) * p_limit; 
           
	-- Thực hiện câu lệnh SELECT và trả về kết quả
    RETURN QUERY EXECUTE v_query;
END;
$function$
;

CREATE OR REPLACE FUNCTION public.func_get_filter_paging_student(p_limit integer, p_offset integer, p_text_search text, p_custom_filter text)
 RETURNS TABLE(student_id uuid, student_code text, student_name text, classes_id uuid, classes_code text, classes_name text, birthday date, gender text, address text, phone_number text, email text)
 LANGUAGE plpgsql
AS $function$ 
DECLARE 
    v_query text;
BEGIN
    -- Xây dựng câu lệnh SELECT để lấy dữ liệu từ bảng tạm
    v_query := '
        SELECT 
            st.student_id,
            st.student_code,
            st.student_name,
            st.classes_id,
            cl.classes_code, 
            cl.classes_name,
            st.birthday,
            st.gender,
            st.address,
            st.phone_number,
            st.email
        FROM 
            student st
        LEFT JOIN 
            classes cl ON st.classes_id = cl.classes_id 
        WHERE 
            (st.student_code ILIKE ''%' || p_text_search || '%''
            OR st.student_name ILIKE ''%' || p_text_search || '%'')
            AND (' || p_custom_filter || ')
        ORDER BY 
            st.modified_date DESC
        LIMIT 
            ' || p_limit || ' OFFSET ' || (p_offset - 1) * p_limit;

    -- Thực hiện câu lệnh SELECT và trả về kết quả
    RETURN QUERY EXECUTE v_query;
END;
$function$
;

CREATE OR REPLACE FUNCTION public.func_get_filter_paging_subject(p_limit integer, p_offset integer, p_text_search text)
 RETURNS TABLE(subject_id uuid, subject_code text, subject_name text, semester_id uuid, semester_name text, number_tc integer)
 LANGUAGE plpgsql
AS $function$
DECLARE  
begin
	/**
	 * Bảng tạm kết quả
	 */
	drop table if exists tmp_result;
	create temp table tmp_result
	(
		subject_id uuid,
		subject_code text,
		subject_name text,
		semester_id uuid,
		semester_name text,
		number_tc int4
	);

	insert into tmp_result 
	select 
		u.subject_id,
		u.subject_code,
		u.subject_name,
		f.semester_id,
		f.semester_name,
		u.number_tc
	from subject u
	inner join semester f on u.semester_id = f.semester_id 
	where 
		u.subject_code ilike '%' || p_text_search || '%'
		or u.subject_name ilike '%' || p_text_search || '%'
	order by 
		u.modified_date desc
	limit p_limit offset (p_offset - 1)* p_limit
	;
	
	RETURN QUERY select * from tmp_result;
END;
$function$
;

CREATE OR REPLACE FUNCTION public.func_get_filter_paging_teacher(p_limit integer, p_offset integer, p_text_search text, p_custom_filter text)
 RETURNS TABLE(teacher_id uuid, teacher_code text, teacher_name text, faculty_id uuid, faculty_code text, faculty_name text, birthday date, gender text, address text, phone_number text, email text)
 LANGUAGE plpgsql
AS $function$
DECLARE 
    v_query text;
begin
	-- Xây dựng câu lệnh SELECT để lấy dữ liệu từ bảng tạm
	v_query := '
        SELECT 
            tc.teacher_id,
			tc.teacher_code,
			tc.teacher_name,
			tc.faculty_id,
			f.faculty_code, 
			f.faculty_name,
			tc.birthday,
			tc.gender,
			tc.address,
			tc.phone_number,
			tc.email
        FROM 
            teacher tc
        LEFT JOIN 
            faculty f on tc.faculty_id = f.faculty_id 
        WHERE 
            (tc.teacher_code ILIKE ''%' || p_text_search || '%''
            OR tc.teacher_name ILIKE ''%' || p_text_search || '%'')
            AND (' || p_custom_filter || ')
        ORDER BY 
            tc.modified_date DESC
        LIMIT 
            ' || p_limit || ' OFFSET ' || (p_offset - 1) * p_limit;

    -- Thực hiện câu lệnh SELECT và trả về kết quả
    RETURN QUERY EXECUTE v_query; 
END;
$function$
;

CREATE OR REPLACE FUNCTION public.func_get_filter_paging_user(p_limit integer, p_offset integer, p_text_search text)
 RETURNS TABLE(user_id uuid, user_name text, role_id uuid, role_name text, status integer)
 LANGUAGE plpgsql
AS $function$
DECLARE  
begin
	/**
	 * Bảng tạm kết quả
	 */
	drop table if exists tmp_result;
	create temp table tmp_result
	(
		user_id uuid,
		user_name text,
		role_id uuid,
		role_name text,
		status int4
	);

	insert into tmp_result 
	select 
		u.user_id,
		u.user_name,
		u.role_id,
		r.role_name,
		u.status
	from "user" u
	inner join "role" r on u.role_id = r.role_id 
	where 
		u.user_name ilike '%' || p_text_search || '%'
	order by 
		u.modified_date desc
	limit p_limit offset (p_offset - 1)* p_limit
	;
	
	RETURN QUERY select * from tmp_result;
END;
$function$
;

CREATE OR REPLACE FUNCTION public.func_get_statistic_number_student()
 RETURNS TABLE(label_year text, admission_year_quantity integer, graduation_year_quantity integer)
 LANGUAGE plpgsql
AS $function$ 
BEGIN
    /**
     * Bảng tạm kết quả
     */
    DROP TABLE IF EXISTS tmp_result;
    CREATE TEMP TABLE IF NOT EXISTS tmp_result
    (
        label_year text,
        admission_year_quantity int,
        graduation_year_quantity int
    );

    INSERT INTO tmp_result 
    SELECT
        CONCAT('Năm ', admission_year::text) AS label_year,
        SUM(admission_count) AS admission_year_quantity,
        SUM(graduation_count) AS graduation_year_quantity
    FROM
    (
        SELECT
            EXTRACT(YEAR FROM admission_year) AS admission_year,
            COUNT(*) AS admission_count,
            0 AS graduation_count
        FROM
            student
        WHERE
            EXTRACT(YEAR FROM admission_year) >= EXTRACT(YEAR FROM CURRENT_DATE) - 5
        GROUP BY
            EXTRACT(YEAR FROM admission_year)
        UNION ALL
        SELECT
            EXTRACT(YEAR FROM graduation_year) AS graduation_year,
            0 AS admission_count,
            COUNT(*) AS graduation_count
        FROM
            student
        WHERE
            graduation_year IS NOT NULL AND EXTRACT(YEAR FROM graduation_year) >= EXTRACT(YEAR FROM CURRENT_DATE) - 5
        GROUP BY
            EXTRACT(YEAR FROM graduation_year)
    ) AS yearly_data
    GROUP BY
        admission_year
    ORDER BY
        admission_year;

    RETURN QUERY SELECT * FROM tmp_result;
END;
$function$
;

CREATE OR REPLACE FUNCTION public.function_get_classes_id_arise(p_ids text)
 RETURNS TABLE(classes_id uuid)
 LANGUAGE plpgsql
AS $function$  
BEGIN
	/*
	 * Tạo bảng tạm lưu các id
	 */
	drop table if exists tmp_ids;
	create temp table tmp_ids(classes_id uuid);
	insert into tmp_ids (classes_id)
	select unnest(string_to_array(p_ids, ';'))::uuid;

	drop table if exists tmp_result;
	create temp table tmp_result(classes_id uuid);

	insert into tmp_result (classes_id)
    select ti.classes_id
    from tmp_ids ti
    where ti.classes_id in (select s.classes_id from student s); 

	return query select distinct * from tmp_result;
END;
$function$
;

CREATE OR REPLACE FUNCTION public.function_get_faculty_id_arise(p_ids text)
 RETURNS TABLE(faculty_id uuid)
 LANGUAGE plpgsql
AS $function$  
BEGIN
	/*
	 * Tạo bảng tạm lưu các id
	 */
	drop table if exists tmp_ids;
	create temp table tmp_ids(faculty_id uuid);
	insert into tmp_ids (faculty_id)
	select unnest(string_to_array(p_ids, ';'))::uuid;

	drop table if exists tmp_result;
	create temp table tmp_result(faculty_id uuid);

	insert into tmp_result (faculty_id)
    select ti.faculty_id
    from tmp_ids ti
    where ti.faculty_id in (select c.faculty_id from classes c)
       or ti.faculty_id in (select t.faculty_id from teacher t); 

	return query select distinct * from tmp_result;
END;
$function$
;

CREATE OR REPLACE FUNCTION public.function_get_option_filter_score(p_option_filter integer, p_text_search text)
 RETURNS TABLE(condition_code text, condition_name text)
 LANGUAGE plpgsql
AS $function$  
BEGIN
    /**
     * Bảng tạm kết quả
     */
    DROP TABLE IF EXISTS tmp_result;
    CREATE TEMP TABLE tmp_result
    (
        condition_code text,
        condition_name text
    );

   	-- Lọc theo mã sinh viên
    if p_option_filter = 5 then
    	insert into tmp_result
    	select distinct 
    		s.student_code as condition_code,
    		s.student_code as condition_name
    	from student s;
    end if;
   
    -- Lọc theo tên sinh viên
    if p_option_filter = 6 then
    	insert into tmp_result
    	select distinct 
    		s.student_name as condition_code,
    		s.student_name as condition_name
    	from student s;
    end if;
   
    -- Lọc theo tên môn học
    if p_option_filter = 7 then
    	insert into tmp_result
    	select distinct 
    		s.subject_name as condition_code,
    		s.subject_name as condition_name
    	from subject s;
    end if;
   
   -- Lọc theo tên giảng viên
    if p_option_filter = 8 then
    	insert into tmp_result
    	select distinct 
    		t.teacher_name as condition_code,
    		t.teacher_name as condition_name
    	from teacher t;
    end if;

    -- Thực hiện câu lệnh SELECT và trả về kết quả
    RETURN QUERY select t.condition_code, t.condition_name from tmp_result t where t.condition_name ilike concat('%', p_text_search, '%') ;
END;
$function$
;

CREATE OR REPLACE FUNCTION public.function_get_option_filter_student(p_option_filter integer, p_text_search text)
 RETURNS TABLE(condition_code text, condition_name text)
 LANGUAGE plpgsql
AS $function$  
BEGIN
    /**
     * Bảng tạm kết quả
     */
    DROP TABLE IF EXISTS tmp_result;
    CREATE TEMP TABLE tmp_result
    (
        condition_code text,
        condition_name text
    );

   	-- Lọc theo lớp
    if p_option_filter = 1 then
    	insert into tmp_result
    	select distinct 
    		c.classes_code as condition_code,
    		c.classes_name as condition_name
    	from classes c;
    end if;
   
    -- Lọc theo giới tính
    if p_option_filter = 2 then
    	insert into tmp_result
    	select distinct 
    		s.gender as condition_code,
    		s.gender as condition_name
    	from student s;
    end if;
   
    -- Lọc theo tỉnh/thành phố
    if p_option_filter = 3 then
    	insert into tmp_result
    	select distinct 
    		pc.province_city_code as condition_code,
    		pc.province_city_name as condition_name
    	from province_city pc ;
    end if;

    -- Thực hiện câu lệnh SELECT và trả về kết quả
    RETURN QUERY select t.condition_code, t.condition_name from tmp_result t where t.condition_name ilike concat('%', p_text_search, '%') ;
END;
$function$
;

CREATE OR REPLACE FUNCTION public.function_get_option_filter_teacher(p_option_filter integer, p_text_search text)
 RETURNS TABLE(condition_code text, condition_name text)
 LANGUAGE plpgsql
AS $function$  
BEGIN
    /**
     * Bảng tạm kết quả
     */
    DROP TABLE IF EXISTS tmp_result;
    CREATE TEMP TABLE tmp_result
    (
        condition_code text,
        condition_name text
    );

   	-- Lọc theo khoa
    if p_option_filter = 4 then
    	insert into tmp_result
    	select distinct 
    		f.faculty_code as condition_code,
    		f.faculty_name as condition_name
    	from faculty f;
    end if;
   
    -- Lọc theo giới tính
    if p_option_filter = 2 then
    	insert into tmp_result
    	select distinct 
    		t.gender as condition_code,
    		t.gender as condition_name
    	from teacher t;
    end if;
   
    -- Lọc theo tỉnh/thành phố
    if p_option_filter = 3 then
    	insert into tmp_result
    	select distinct 
    		pc.province_city_code as condition_code,
    		pc.province_city_name as condition_name
    	from province_city pc ;
    end if;

    -- Thực hiện câu lệnh SELECT và trả về kết quả
    RETURN QUERY select t.condition_code, t.condition_name from tmp_result t where t.condition_name ilike concat('%', p_text_search, '%') ;
END;
$function$
;

CREATE OR REPLACE FUNCTION public.function_get_registration_id_arise(p_ids text)
 RETURNS TABLE(class_registration_id uuid)
 LANGUAGE plpgsql
AS $function$  
BEGIN
	/*
	 * Tạo bảng tạm lưu các id
	 */
	drop table if exists tmp_ids;
	create temp table tmp_ids(class_registration_id uuid);
	insert into tmp_ids (class_registration_id)
	select unnest(string_to_array(p_ids, ';'))::uuid;

	drop table if exists tmp_result;
	create temp table tmp_result(class_registration_id uuid);

	insert into tmp_result (class_registration_id)
    select ti.class_registration_id
    from tmp_ids ti
    where ti.class_registration_id in (select s.class_registration_id from score s); 

	return query select distinct * from tmp_result;
END;
$function$
;

CREATE OR REPLACE FUNCTION public.function_get_student_id_arise(p_ids text)
 RETURNS TABLE(student_id uuid)
 LANGUAGE plpgsql
AS $function$  
BEGIN
	/*
	 * Tạo bảng tạm lưu các id
	 */
	drop table if exists tmp_ids;
	create temp table tmp_ids(student_id uuid);
	insert into tmp_ids (student_id)
	select unnest(string_to_array(p_ids, ';'))::uuid;

	drop table if exists tmp_result;
	create temp table tmp_result(student_id uuid);

	insert into tmp_result (student_id)
    select ti.student_id
    from tmp_ids ti
    where ti.student_id in (select crd.student_id from class_registration_detail crd)
       or ti.student_id in (select s.student_id from score s); 

	return query select distinct * from tmp_result;
END;
$function$
;

CREATE OR REPLACE FUNCTION public.function_get_subject_id_arise(p_ids text)
 RETURNS TABLE(subject_id uuid)
 LANGUAGE plpgsql
AS $function$  
BEGIN
	/*
	 * Tạo bảng tạm lưu các id
	 */
	drop table if exists tmp_ids;
	create temp table tmp_ids(subject_id uuid);
	insert into tmp_ids (subject_id)
	select unnest(string_to_array(p_ids, ';'))::uuid;

	drop table if exists tmp_result;
	create temp table tmp_result(subject_id uuid);

	insert into tmp_result (subject_id)
    select ti.subject_id
    from tmp_ids ti
    where ti.subject_id in (select cr.subject_id from class_registration cr); 

	return query select distinct * from tmp_result;
END;
$function$
;

CREATE OR REPLACE FUNCTION public.function_get_teacher_id_arise(p_ids text)
 RETURNS TABLE(teacher_id uuid)
 LANGUAGE plpgsql
AS $function$  
BEGIN
	/*
	 * Tạo bảng tạm lưu các id
	 */
	drop table if exists tmp_ids;
	create temp table tmp_ids(teacher_id uuid);
	insert into tmp_ids (teacher_id)
	select unnest(string_to_array(p_ids, ';'))::uuid;

	drop table if exists tmp_result;
	create temp table tmp_result(teacher_id uuid);

	insert into tmp_result (teacher_id)
    select ti.teacher_id
    from tmp_ids ti
    where ti.teacher_id in (select cr.teacher_id from class_registration cr)
       or ti.teacher_id in (select s.teacher_id from score s); 

	return query select distinct * from tmp_result;
END;
$function$
;