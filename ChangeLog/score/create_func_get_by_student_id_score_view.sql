CREATE OR REPLACE FUNCTION public.func_get_by_student_id_score_view(p_student_id uuid)
 RETURNS TABLE(
score_id uuid,
student_id uuid,
student_code text,
student_name text,
teacher_id uuid,
teacher_code text,
teacher_name text,
class_registration_id uuid,
class_registration_code text,
class_registration_name text,
subject_code text,
subject_name text,
number_tc integer,
score_attendance double precision,
score_test double precision,
score_exam double precision,
score_average double precision,
evaluate_state integer
)
 LANGUAGE plpgsql
AS $function$
DECLARE 
begin
	/**
	 * Bảng tạm kết quả
	 */
	drop table if exists tmp_result;
	create temp table tmp_result
	(
		score_id uuid, 
		student_id uuid,
		student_code text,
		student_name text,
		teacher_id uuid,
		teacher_code text,
		teacher_name text,
		class_registration_id uuid,
		class_registration_code text,
		class_registration_name text,
		subject_code text,
		subject_name text,
		number_tc int,
		score_attendance float8,
		score_test float8,
		score_exam float8,
		score_average float8,
		evaluate_state int
	);

	insert into tmp_result 
	select 
		sc.score_id,
		sc.student_id,
		st.student_code,
		st.student_name,
		sc.teacher_id,
		tc.teacher_code,
		tc.teacher_name,
		sc.class_registration_id,
		cr.class_registration_code,
		cr.class_registration_name,
		sb.subject_code,
		sb.subject_name,
		sb.number_tc,
		sc.score_attendance,
		sc.score_test,
		sc.score_exam,
		sc.score_average,
		case when sc.score_average > 4 then 1 else 2 end as evaluate_state
	from score sc
		inner join student st on sc.student_id = st.student_id 
		inner join teacher tc on sc.teacher_id = tc.teacher_id 
		inner join class_registration cr on sc.class_registration_id = cr.class_registration_id
		inner join subject sb on sb.subject_id = cr.subject_id
	where sc.student_id = p_student_id
	order by 
		sc.student_id, sb.subject_code
	;
	
	RETURN QUERY select * from tmp_result;
END;
$function$
;
