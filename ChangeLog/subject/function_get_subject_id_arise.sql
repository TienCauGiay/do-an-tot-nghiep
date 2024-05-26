CREATE OR REPLACE FUNCTION public.function_get_subject_id_arise(p_ids text)
 RETURNS TABLE(subject_id uuid)
 LANGUAGE plpgsql
AS $function$  
BEGIN
	/*
	 * Tạo bảng tạm lưu các id
	 */
	drop table if exists tmp_ids;
	create temp table tmp_ids(subject_id uuid);
	insert into tmp_ids (subject_id)
	select unnest(string_to_array(p_ids, ';'))::uuid;

	drop table if exists tmp_result;
	create temp table tmp_result(subject_id uuid);

	insert into tmp_result (subject_id)
    select ti.subject_id
    from tmp_ids ti
    where ti.subject_id in (select cr.subject_id from class_registration cr); 

	return query select distinct * from tmp_result;
END;
$function$
;
