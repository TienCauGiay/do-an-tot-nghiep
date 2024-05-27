CREATE OR REPLACE FUNCTION public.func_get_class_average_score()
 RETURNS TABLE(class_name text, average_score float8)
 LANGUAGE plpgsql
AS $function$ 
BEGIN
    -- Bảng tạm kết quả
    DROP TABLE IF EXISTS tmp_result;
    CREATE TEMP TABLE IF NOT EXISTS tmp_result
    (
        class_name text,
        average_score float8
    );

    -- Chèn dữ liệu vào bảng tạm
    INSERT INTO tmp_result 
    SELECT
        cr.class_registration_name as class_name,
        AVG(s.score_average) AS average_score
    FROM
        public.score s
    inner JOIN
        public.class_registration cr ON s.class_registration_id = cr.class_registration_id
    GROUP BY
        cr.class_registration_name
    ORDER BY
        cr.class_registration_name;

    -- Trả về kết quả từ bảng tạm
    RETURN QUERY SELECT * FROM tmp_result;
END;
$function$;
