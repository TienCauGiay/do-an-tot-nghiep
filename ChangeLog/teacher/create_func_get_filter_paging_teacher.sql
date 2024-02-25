CREATE OR REPLACE FUNCTION func_get_filter_paging_teacher(p_limit int, p_offset int, p_text_search text)
RETURNS TABLE 
(
teacher_id uuid,
teacher_code text,
teacher_name text,
subject_id uuid,
subject_code text,
subject_name text,
birthday date,
gender text,
address text,
phone_number text,
email text
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
		teacher_id uuid,
		teacher_code text,
		teacher_name text,
		subject_id uuid,
		subject_code text,
		subject_name text,
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
		tc.subject_id,
		sb.subject_code, 
		sb.subject_name,
		tc.birthday,
		tc.gender,
		tc.address,
		tc.phone_number,
		tc.email
	from teacher tc
	left join subject sb on tc.subject_id = sb.subject_id 
	where 
		tc.teacher_code ilike '%' || p_text_search || '%'
		or tc.teacher_name ilike '%' || p_text_search || '%'
	order by 
		tc.modified_date desc
	limit p_limit offset (p_offset - 1)* p_limit
	;
	
	RETURN QUERY select * from tmp_result;
END;
$$
LANGUAGE plpgsql;
