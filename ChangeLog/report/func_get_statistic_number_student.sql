CREATE OR REPLACE FUNCTION public.func_get_statistic_number_student()
 RETURNS TABLE(label_year text, admission_year_quantity integer, graduation_year_quantity integer)
 LANGUAGE plpgsql
AS $function$ 
BEGIN
    /**
     * Bảng tạm kết quả
     */
    DROP TABLE IF EXISTS tmp_result;
    CREATE TEMP TABLE IF NOT EXISTS tmp_result
    (
        label_year text,
        admission_year_quantity int,
        graduation_year_quantity int
    );

    INSERT INTO tmp_result 
    SELECT
        CONCAT('Năm ', admission_year::text) AS label_year,
        SUM(admission_count) AS admission_year_quantity,
        SUM(graduation_count) AS graduation_year_quantity
    FROM
    (
        SELECT
            EXTRACT(YEAR FROM admission_year) AS admission_year,
            COUNT(*) AS admission_count,
            0 AS graduation_count
        FROM
            student
        WHERE
            EXTRACT(YEAR FROM admission_year) >= EXTRACT(YEAR FROM CURRENT_DATE) - 5
        GROUP BY
            EXTRACT(YEAR FROM admission_year)
        UNION ALL
        SELECT
            EXTRACT(YEAR FROM graduation_year) AS graduation_year,
            0 AS admission_count,
            COUNT(*) AS graduation_count
        FROM
            student
        WHERE
            graduation_year IS NOT NULL AND EXTRACT(YEAR FROM graduation_year) >= EXTRACT(YEAR FROM CURRENT_DATE) - 5
        GROUP BY
            EXTRACT(YEAR FROM graduation_year)
    ) AS yearly_data
    GROUP BY
        admission_year
    ORDER BY
        admission_year;

    RETURN QUERY SELECT * FROM tmp_result;
END;
$function$
;
