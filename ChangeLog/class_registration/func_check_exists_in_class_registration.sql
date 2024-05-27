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