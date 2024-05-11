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
