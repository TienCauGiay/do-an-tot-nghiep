CREATE OR REPLACE FUNCTION public.function_get_classes_id_arise(p_ids text)
 RETURNS TABLE(classes_id uuid)
 LANGUAGE plpgsql
AS $function$  
BEGIN
	/*
	 * Tạo bảng tạm lưu các id
	 */
	drop table if exists tmp_ids;
	create temp table tmp_ids(classes_id uuid);
	insert into tmp_ids (classes_id)
	select unnest(string_to_array(p_ids, ';'))::uuid;

	drop table if exists tmp_result;
	create temp table tmp_result(classes_id uuid);

	insert into tmp_result (classes_id)
    select ti.classes_id
    from tmp_ids ti
    where ti.classes_id in (select s.classes_id from student s); 

	return query select distinct * from tmp_result;
END;
$function$
;
