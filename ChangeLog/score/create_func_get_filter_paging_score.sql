CREATE OR REPLACE FUNCTION func_get_filter_paging_score(p_limit integer, p_offset integer, p_text_search text)
RETURNS TABLE 
(
score_id uuid, 
student_id uuid,
student_code text,
student_name text,
teacher_id uuid,
teacher_code text,
teacher_name text,
subject_code text,
subject_name text,
number_tc int,
score_attendance float8,
score_test float8,
score_exam float8,
score_average float8,
evaluate_state int
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
		score_id uuid, 
		student_id uuid,
		student_code text,
		student_name text,
		teacher_id uuid,
		teacher_code text,
		teacher_name text,
		subject_code text,
		subject_name text,
		number_tc int,
		score_attendance float8,
		score_test float8,
		score_exam float8,
		score_average float8,
		evaluate_state int
	);

	insert into tmp_result 
	select 
		sc.score_id,
		sc.student_id,
		st.student_code,
		st.student_name,
		sc.teacher_id,
		tc.teacher_code,
		tc.teacher_name,
		sb.subject_code,
		sb.subject_name,
		sb.number_tc,
		sc.score_attendance,
		sc.score_test,
		sc.score_exam,
		sc.score_average,
		case when sc.score_average > 4 then 1 else 2 end as evaluate_state
	from score sc
		left join student st on sc.student_id = st.student_id 
		left join teacher tc on sc.teacher_id = tc.teacher_id 
		left join subject sb on tc.subject_id = sb.subject_id
	where 
		st.student_code ilike '%' || p_text_search || '%'
		or st.student_name ilike '%' || p_text_search || '%'
		or tc.teacher_code ilike '%' || p_text_search || '%'
		or tc.teacher_name ilike '%' || p_text_search || '%'
		or sb.subject_code ilike '%' || p_text_search || '%'
		or sb.subject_name ilike '%' || p_text_search || '%'
	order by 
		st.student_code, sb.subject_code
	limit p_limit offset (p_offset - 1)* p_limit
	;
	
	RETURN QUERY select * from tmp_result;
END;
$$
LANGUAGE plpgsql;