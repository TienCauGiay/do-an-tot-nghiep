CREATE OR REPLACE FUNCTION func_get_filter_paging_user(p_limit int, p_offset int, p_text_search text)
RETURNS TABLE 
(
user_id uuid,
user_name text,
role_id uuid,
role_name text,
status int4
)
AS
$$
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
$$
LANGUAGE plpgsql;
