import { createRouter, createWebHistory } from 'vue-router';
import ManagementStudent from '@/views/management_student/ManagementStudent.vue'
import ManagementTeacher from '@/views/management_teacher/ManagementTeacher.vue'
import ManagementScore from '@/views/management_score/ManagementScore.vue'
import StatisticPage from '@/views/statistic_page/StatisticPage.vue'
import ManagementUser from '@/views/management_user/ManagementUser.vue'  
import LoginPage from '@/views/login_app/LoginPage.vue'

/**
 * Mô tả: Định nghĩa các router
 * created by : BNTIEN
 * created date: 01-06-2023 13:11:40
 */
const routes = [
    { path: "/login", component: LoginPage, name: "LoginPage" },
    { path: "/management-student", component: ManagementStudent, name: "ManagementStudent", meta: { requiresAuth: true } },
    { path: "/management-teacher", component: ManagementTeacher, name: "ManagementTeacher", meta: { requiresAuth: true } },
    { path: "/management-score", component: ManagementScore, name: "ManagementScore", meta: { requiresAuth: true } },
    { path: "/statistic", component: StatisticPage, name: "StatisticPage", meta: { requiresAuth: true } },
    { path: "/management-user", component: ManagementUser, name: "ManagementUser", meta: { requiresAuth: true } },
]

/**
 * Mô tả: Khởi tạo vue router
 * created by : BNTIEN
 * created date: 01-06-2023 13:11:54
 */
const vueRouter = createRouter({
    history: createWebHistory(),
    routes
});

// Kiểm tra trạng thái xác thực trước khi điều hướng
vueRouter.beforeEach((to, from, next) => {
    const isAuthenticated = sessionStorage.getItem('token'); // Kiểm tra token

    if (to.matched.some(record => record.meta.requiresAuth) && !isAuthenticated) {
        // Nếu trang yêu cầu xác thực và người dùng chưa đăng nhập, chuyển hướng đến trang đăng nhập
        next('/login');
    } else {
        next(); // Cho phép điều hướng bình thường
    }
});

export default vueRouter;
