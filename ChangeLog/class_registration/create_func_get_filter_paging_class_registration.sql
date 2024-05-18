CREATE OR REPLACE FUNCTION public.func_get_filter_paging_class_registration(p_limit integer, p_offset integer, p_text_search text)
 RETURNS TABLE(class_registration_code text, class_registration_name text)
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
		class_registration_code text,
		class_registration_name text
	);

	insert into tmp_result 
	select 
		u.class_registration_code,
		u.class_registration_name
	from class_registration u
	where 
		u.class_registration_code ilike '%' || p_text_search || '%'
		or u.class_registration_name ilike '%' || p_text_search || '%'
	order by 
		u.modified_date desc
	limit p_limit offset (p_offset - 1)* p_limit
	;
	
	RETURN QUERY select distinct * from tmp_result;
END;
$function$
;
