import {createRouter, createWebHistory} from 'vue-router';
import ManagementStudent from '@/views/management_student/ManagementStudent.vue'
import ManagementTeacher from '@/views/management_teacher/ManagementTeacher.vue'
import ManagementScore from '@/views/management_score/ManagementScore.vue'
import StatisticPage from '@/views/statistic_page/StatisticPage.vue'
import ManagementAccount from '@/views/management_account/ManagementAccount.vue'
import EmployeeList from '@/views/employee/EmployeeList.vue'

/**
 * Mô tả: Định nghĩa các router
 * created by : BNTIEN
 * created date: 01-06-2023 13:11:40
 */
const routers = [
    {path: "/management-student", component: ManagementStudent, name: "ManagementStudent"},
    {path: "/management-teacher", component: ManagementTeacher, name: "ManagementTeacher"},
    {path: "/management-score", component: ManagementScore, name: "ManagementScore"},
    {path: "/statistic", component: StatisticPage, name: "StatisticPage"},
    {path: "/management-account", component: ManagementAccount, name: "ManagementAccount"},
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