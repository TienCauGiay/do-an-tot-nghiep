/**
* Lấy các student_id đã có phát sinh ở bảng lớp học phần hoặc bảng điểm
*/

CREATE OR REPLACE FUNCTION public.function_get_student_id_arise(p_ids text)
 RETURNS TABLE(student_id uuid)
 LANGUAGE plpgsql
AS $function$  
BEGIN
	/*
	 * Tạo bảng tạm lưu các id
	 */
	drop table if exists tmp_ids;
	create temp table tmp_ids(student_id uuid);
	insert into tmp_ids (student_id)
	select unnest(string_to_array(p_ids, ';'))::uuid;

	drop table if exists tmp_result;
	create temp table tmp_result(student_id uuid);

	insert into tmp_result (student_id)
    select ti.student_id
    from tmp_ids ti
    where ti.student_id in (select crd.student_id from class_registration_detail crd)
       or ti.student_id in (select s.student_id from score s); 

	return query select distinct * from tmp_result;
END;
$function$
;
