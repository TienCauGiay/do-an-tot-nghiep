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
