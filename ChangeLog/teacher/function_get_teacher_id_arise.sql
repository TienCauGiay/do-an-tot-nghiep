CREATE OR REPLACE FUNCTION public.function_get_teacher_id_arise(p_teacher_ids text)
 RETURNS TABLE(teacher_id uuid)
 LANGUAGE plpgsql
AS $function$  
BEGIN
	/*
	 * Tạo bảng tạm lưu các id
	 */
	drop table if exists tmp_ids;
	create temp table tmp_ids(teacher_id uuid);
	insert into tmp_ids (teacher_id)
	select unnest(string_to_array(p_teacher_ids, ';'))::uuid;

	drop table if exists tmp_result;
	create temp table tmp_result(teacher_id uuid);

	insert into tmp_result (teacher_id)
    select ti.teacher_id
    from tmp_ids ti
    where ti.teacher_id in (select cr.teacher_id from class_registration cr)
       or ti.teacher_id in (select s.teacher_id from score s); 

	return query select distinct * from tmp_result;
END;
$function$
;
