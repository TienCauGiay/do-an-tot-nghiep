CREATE OR REPLACE FUNCTION public.func_get_filter_paging_teacher(p_limit integer, p_offset integer, p_text_search text, p_custom_filter text)
 RETURNS TABLE(teacher_id uuid, teacher_code text, teacher_name text, faculty_id uuid, faculty_code text, faculty_name text, birthday date, gender text, address text, phone_number text, email text)
 LANGUAGE plpgsql
AS $function$
DECLARE 
    v_query text;
begin
	-- Xây dựng câu lệnh SELECT để lấy dữ liệu từ bảng tạm
	v_query := '
        SELECT 
            tc.teacher_id,
			tc.teacher_code,
			tc.teacher_name,
			tc.faculty_id,
			f.faculty_code, 
			f.faculty_name,
			tc.birthday,
			tc.gender,
			tc.address,
			tc.phone_number,
			tc.email
        FROM 
            teacher tc
        LEFT JOIN 
            faculty f on tc.faculty_id = f.faculty_id 
        WHERE 
            (tc.teacher_code ILIKE ''%' || p_text_search || '%''
            OR tc.teacher_name ILIKE ''%' || p_text_search || '%'')
            AND (' || p_custom_filter || ')
        ORDER BY 
            tc.modified_date DESC
        LIMIT 
            ' || p_limit || ' OFFSET ' || (p_offset - 1) * p_limit;

    -- Thực hiện câu lệnh SELECT và trả về kết quả
    RETURN QUERY EXECUTE v_query; 
END;
$function$
;
