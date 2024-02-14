import {createRouter, createWebHistory} from 'vue-router';
import StudyProgram from '@/views/study_program/StudyProgram.vue'
import RegisterStudy from '@/views/register_study/RegisterStudy.vue'
import ExamRegistration from '@/views/exam_registration/ExamRegistration.vue'
import LookUpTuitionFees from '@/views/look_up_tuition_fees/LookUpTuitionFees.vue'
import LookUpPoint from '@/views/look_up_point/LookUpPoint.vue'
import EmployeeList from '@/views/employee/EmployeeList.vue'

/**
 * Mô tả: Định nghĩa các router
 * created by : BNTIEN
 * created date: 01-06-2023 13:11:40
 */
const routers = [
    {path: "/study-program", component: StudyProgram, name: "StudyProgram"},
    {path: "/register-study", component: RegisterStudy, name: "RegisterStudy"},
    {path: "/exam-registration", component: ExamRegistration, name: "ExamRegistration"},
    {path: "/look-up-tuition-fees", component: LookUpTuitionFees, name: "LookUpTuitionFees"},
    {path: "/look-up-point", component: LookUpPoint, name: "LookUpPoint"},
    {path: "/employee", component: EmployeeList, name: "EmployeeList"},
    // {path: "/category/provider", component: ProviderList, name: "ProviderListRouter"},
    // {
    //     path: "/cash", 
    //     component: CashHomePage, 
    //     name: "CashHomePageRouter",
    //     children: [
    //         {
    //             path: 'cash-process',
    //             component: CashProcess,
    //             name: "CashProcess"
    //         },
    //         {
    //             path: "receipt-payment", 
    //             component: ReceiptPayment, 
    //             name: "ReceiptPaymentRouter"
    //         },
    //     ]
    // },
]

/**
 * Mô tả: Khởi tạo vue router
 * created by : BNTIEN
 * created date: 01-06-2023 13:11:54
 */
const vueRouter = createRouter({
    history: createWebHistory(),
    routes: routers
})

export default vueRouter;