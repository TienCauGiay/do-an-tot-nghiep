CREATE OR REPLACE FUNCTION func_get_filter_paging_student(p_limit int, p_offset int, p_text_search text)
RETURNS TABLE 
(
student_id uuid,
student_code text,
student_name text,
classes_code text,
classes_name text,
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
		student_id uuid,
		student_code text,
		student_name text,
		classes_code text,
		classes_name text,
		birthday date,
		gender text,
		address text,
		phone_number text,
		email text
	);

	insert into tmp_result 
	select 
		st.student_id,
		st.student_code,
		st.student_name,
		cl.classes_code, 
		cl.classes_name,
		st.birthday,
		st.gender,
		st.address,
		st.phone_number,
		st.email
	from student st
	left join classes cl on st.classes_id = cl.classes_id 
	where 
		st.student_code ilike '%' || p_text_search || '%'
		or st.student_name ilike '%' || p_text_search || '%'
	order by 
		student_code, student_name 
	limit p_limit offset (p_offset - 1)* p_limit
	;
	
	RETURN QUERY select * from tmp_result;
END;
$$
LANGUAGE plpgsql;
