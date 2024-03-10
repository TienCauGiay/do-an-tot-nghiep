CREATE OR REPLACE FUNCTION public.func_get_statistic_number_student()
 RETURNS TABLE(label_year text, admission_year_quantity integer, graduation_year_quantity integer)
 LANGUAGE plpgsql
AS $function$ 
BEGIN
    /**
     * Bảng tạm kết quả
     */
	drop table if exists tmp_result;
    CREATE TEMP TABLE IF NOT EXISTS tmp_result
    (
        label_year text,
        admission_year_quantity int,
        graduation_year_quantity int
    ); 

    INSERT INTO tmp_result 
    SELECT	 
        CONCAT('Năm ', EXTRACT(YEAR FROM admission_year)) AS label_year,
        COUNT(*) AS admission_year_quantity,
        COUNT(CASE WHEN graduation_year IS NOT NULL THEN 1 END) AS graduation_year_quantity
    FROM	 
        student
    WHERE	 
        EXTRACT(YEAR FROM admission_year) >= EXTRACT(YEAR FROM CURRENT_DATE) - 5
    GROUP BY 
        EXTRACT(YEAR FROM admission_year)
    ORDER BY 
        EXTRACT(YEAR FROM admission_year);

    RETURN QUERY SELECT * FROM tmp_result;
END;
$function$
;
