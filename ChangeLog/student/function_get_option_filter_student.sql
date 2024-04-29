/**
* Lấy dữ liệu điều kiện lọc màn hình quản lí sinh viên của admin
*/

CREATE OR REPLACE FUNCTION public.function_get_option_filter_student(p_option_filter integer)
 RETURNS table (condition_code text, condition_name text)
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

   	-- Lọc theo lớp
    if p_option_filter = 1 then
    	insert into tmp_result
    	select distinct 
    		c.classes_code as condition_code,
    		c.classes_name as condition_name
    	from classes c;
    end if;
   
    -- Lọc theo giới tính
    if p_option_filter = 2 then
    	insert into tmp_result
    	select distinct 
    		s.gender as condition_code,
    		s.gender as condition_name
    	from student s;
    end if;
   
    -- Lọc theo tỉnh/thành phố
    if p_option_filter = 3 then
    	insert into tmp_result
    	select distinct 
    		pc.province_city_code as condition_code,
    		pc.province_city_name as condition_name
    	from province_city pc ;
    end if;

    -- Thực hiện câu lệnh SELECT và trả về kết quả
    RETURN QUERY select * from tmp_result;
END;
$function$
; 
