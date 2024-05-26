CREATE OR REPLACE FUNCTION public.function_get_faculty_id_arise(p_ids text)
 RETURNS TABLE(faculty_id uuid)
 LANGUAGE plpgsql
AS $function$  
BEGIN
	/*
	 * Tạo bảng tạm lưu các id
	 */
	drop table if exists tmp_ids;
	create temp table tmp_ids(faculty_id uuid);
	insert into tmp_ids (faculty_id)
	select unnest(string_to_array(p_ids, ';'))::uuid;

	drop table if exists tmp_result;
	create temp table tmp_result(faculty_id uuid);

	insert into tmp_result (faculty_id)
    select ti.faculty_id
    from tmp_ids ti
    where ti.faculty_id in (select c.faculty_id from classes c)
       or ti.faculty_id in (select t.faculty_id from teacher t); 

	return query select distinct * from tmp_result;
END;
$function$
;
