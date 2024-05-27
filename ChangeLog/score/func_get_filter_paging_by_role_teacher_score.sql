CREATE OR REPLACE FUNCTION public.func_get_filter_paging_by_role_teacher_score(p_limit integer, p_offset integer, p_text_search text, p_custom_filter text, p_user_id uuid)
 RETURNS TABLE(score_id uuid, student_id uuid, student_code text, student_name text, teacher_id uuid, teacher_code text, teacher_name text, class_registration_id uuid, class_registration_code text, class_registration_name text, subject_code text, subject_name text, number_tc integer, score_attendance double precision, score_test double precision, score_exam double precision, score_average double precision, evaluate_state integer)
 LANGUAGE plpgsql
AS $function$
DECLARE 
	v_query text;
begin 
	-- Xây dựng câu lệnh SELECT để lấy dữ liệu từ bảng tạm
	v_query := '
    SELECT 
        sc.score_id,
		sc.student_id,
		st.student_code,
		st.student_name,
		sc.teacher_id,
		tc.teacher_code,
		tc.teacher_name,
		sc.class_registration_id,
		cr.class_registration_code,
		cr.class_registration_name,
		sb.subject_code,
		sb.subject_name,
		sb.number_tc,
		sc.score_attendance,
		sc.score_test,
		sc.score_exam,
		sc.score_average,
		case when sc.score_average > 4 then 1 else 2 end as evaluate_state
    FROM score sc
		inner join teacher tc on sc.teacher_id = tc.teacher_id  
		inner join public.user u on tc.teacher_code = u.user_name  
		inner join student st on sc.student_id = st.student_id 
		inner join class_registration cr on sc.class_registration_id = cr.class_registration_id 
		inner join subject sb on sb.subject_id = cr.subject_id
    WHERE 
        (
			st.student_code ILIKE ''%' || p_text_search || '%''
        	OR st.student_name ILIKE ''%' || p_text_search || '%''
			OR tc.teacher_code ILIKE ''%' || p_text_search || '%''
			OR tc.teacher_name ILIKE ''%' || p_text_search || '%''
			OR sb.subject_code ILIKE ''%' || p_text_search || '%''
			OR sb.subject_name ILIKE ''%' || p_text_search || '%''
		)
        AND (' || p_custom_filter || ')
		and u.user_id = ''' || p_user_id || '''
    ORDER BY 
        st.student_code, sb.subject_code
    LIMIT 
        ' || p_limit || ' OFFSET ' || (p_offset - 1) * p_limit;  
           
	-- Thực hiện câu lệnh SELECT và trả về kết quả
    RETURN QUERY EXECUTE v_query;
END;
$function$
;
