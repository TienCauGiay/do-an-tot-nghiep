CREATE OR REPLACE FUNCTION public.func_get_filter_paging_subject(p_limit integer, p_offset integer, p_text_search text)
 RETURNS TABLE(subject_id uuid, subject_code text, subject_name text, semester_id uuid, semester_name text)
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
		semester_name text
	);

	insert into tmp_result 
	select 
		u.subject_id,
		u.subject_code,
		u.subject_name,
		f.semester_id,
		f.semester_name 
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
