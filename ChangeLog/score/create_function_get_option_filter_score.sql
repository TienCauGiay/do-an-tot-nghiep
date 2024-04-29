CREATE OR REPLACE FUNCTION public.function_get_option_filter_score(p_option_filter integer, p_text_search text)
 RETURNS TABLE(condition_code text, condition_name text)
 LANGUAGE plpgsql
AS $function$  
BEGIN
    /**
     * Bảng tạm kết quả
     */
    DROP TABLE IF EXISTS tmp_result;
    CREATE TEMP TABLE tmp_result
    (
        condition_code text,
        condition_name text
    );

   	-- Lọc theo mã sinh viên
    if p_option_filter = 5 then
    	insert into tmp_result
    	select distinct 
    		s.student_code as condition_code,
    		s.student_code as condition_name
    	from student s;
    end if;
   
    -- Lọc theo tên sinh viên
    if p_option_filter = 6 then
    	insert into tmp_result
    	select distinct 
    		s.student_name as condition_code,
    		s.student_name as condition_name
    	from student s;
    end if;
   
    -- Lọc theo tên môn học
    if p_option_filter = 7 then
    	insert into tmp_result
    	select distinct 
    		s.subject_name as condition_code,
    		s.subject_name as condition_name
    	from subject s;
    end if;
   
   -- Lọc theo tên giảng viên
    if p_option_filter = 8 then
    	insert into tmp_result
    	select distinct 
    		t.teacher_name as condition_code,
    		t.teacher_name as condition_name
    	from teacher t;
    end if;

    -- Thực hiện câu lệnh SELECT và trả về kết quả
    RETURN QUERY select t.condition_code, t.condition_name from tmp_result t where t.condition_name ilike concat('%', p_text_search, '%') ;
END;
$function$
;
