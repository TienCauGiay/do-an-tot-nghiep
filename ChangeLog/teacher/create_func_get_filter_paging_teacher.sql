CREATE OR REPLACE FUNCTION public.func_get_filter_paging_teacher(p_limit integer, p_offset integer, p_text_search text)
 RETURNS TABLE(teacher_id uuid, teacher_code text, teacher_name text, faculty_id uuid, faculty_code text, faculty_name text, birthday date, gender text, address text, phone_number text, email text)
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
		teacher_id uuid,
		teacher_code text,
		teacher_name text,
		faculty_id uuid,
		faculty_code text,
		faculty_name text,
		birthday date,
		gender text,
		address text,
		phone_number text,
		email text
	);

	insert into tmp_result 
	select 
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
	from teacher tc
	left join faculty f on tc.faculty_id = f.faculty_id 
	where 
		tc.teacher_code ilike '%' || p_text_search || '%'
		or tc.teacher_name ilike '%' || p_text_search || '%'
	order by 
		tc.modified_date desc
	limit p_limit offset (p_offset - 1)* p_limit
	;
	
	RETURN QUERY select * from tmp_result;
END;
$function$
;
