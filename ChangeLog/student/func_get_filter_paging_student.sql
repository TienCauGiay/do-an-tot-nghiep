CREATE OR REPLACE FUNCTION public.func_get_filter_paging_student(p_limit integer, p_offset integer, p_text_search text, p_custom_filter text)
 RETURNS TABLE(student_id uuid, student_code text, student_name text, classes_id uuid, classes_code text, classes_name text, birthday date, gender text, address text, phone_number text, email text)
 LANGUAGE plpgsql
AS $function$ 
DECLARE 
    v_query text;
BEGIN
    /**
     * Bảng tạm kết quả
     */
    DROP TABLE IF EXISTS tmp_result;
    CREATE TEMP TABLE tmp_result
    (
        student_id uuid,
        student_code text,
        student_name text,
        classes_id uuid,
        classes_code text,
        classes_name text,
        birthday date,
        gender text,
        address text,
        phone_number text,
        email text
    );

    -- Xây dựng câu lệnh SELECT để lấy dữ liệu từ bảng tạm
    v_query := '
        SELECT 
            st.student_id,
            st.student_code,
            st.student_name,
            st.classes_id,
            cl.classes_code, 
            cl.classes_name,
            st.birthday,
            st.gender,
            st.address,
            st.phone_number,
            st.email
        FROM 
            student st
        LEFT JOIN 
            classes cl ON st.classes_id = cl.classes_id 
        WHERE 
            (st.student_code ILIKE ''%' || p_text_search || '%''
            OR st.student_name ILIKE ''%' || p_text_search || '%'')
            AND (' || p_custom_filter || ')
        ORDER BY 
            st.modified_date DESC
        LIMIT 
            ' || p_limit || ' OFFSET ' || (p_offset - 1) * p_limit;

    -- Thực hiện câu lệnh SELECT và trả về kết quả
    RETURN QUERY EXECUTE v_query;
END;
$function$;
