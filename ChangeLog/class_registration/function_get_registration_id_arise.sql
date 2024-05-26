CREATE OR REPLACE FUNCTION public.function_get_registration_id_arise(p_ids text)
 RETURNS TABLE(class_registration_id uuid)
 LANGUAGE plpgsql
AS $function$  
BEGIN
	/*
	 * Tạo bảng tạm lưu các id
	 */
	drop table if exists tmp_ids;
	create temp table tmp_ids(class_registration_id uuid);
	insert into tmp_ids (class_registration_id)
	select unnest(string_to_array(p_ids, ';'))::uuid;

	drop table if exists tmp_result;
	create temp table tmp_result(class_registration_id uuid);

	insert into tmp_result (class_registration_id)
    select ti.class_registration_id
    from tmp_ids ti
    where ti.class_registration_id in (select s.class_registration_id from score s); 

	return query select distinct * from tmp_result;
END;
$function$
; 
