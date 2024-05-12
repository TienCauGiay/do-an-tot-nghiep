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
