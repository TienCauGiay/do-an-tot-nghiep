import { createRouter, createWebHistory } from 'vue-router';
import ManagementFaculty from '@/views/management_faculty/ManagementFaculty.vue';
import ManagementStudent from '@/views/management_student/ManagementStudent.vue';
import ManagementTeacher from '@/views/management_teacher/ManagementTeacher.vue';
import ManagementScore from '@/views/management_score/ManagementScore.vue';
import StatisticPage from '@/views/statistic_page/StatisticPage.vue';
import ManagementUser from '@/views/management_user/ManagementUser.vue';
import LoginPage from '@/views/login_app/LoginPage.vue';
import NotPermission from '@/views/permission/NotPermission.vue'
import enumaration from '@/scripts/enum.js';

/**
 * Mô tả: Định nghĩa các router
 * created by : BNTIEN
 * created date: 01-06-2023 13:11:40
 */
const routes = [
    { path: "/login", component: LoginPage, name: "LoginPage" },
    { path: "/not-permission", component: NotPermission, name: "NotPermission" },
    { path: "/management-faculty", component: ManagementFaculty, name: "ManagementFaculty", meta: { requiresAuth: true, requiredPermission: [enumaration.PERMISSION.Admin] } },
    { path: "/management-student", component: ManagementStudent, name: "ManagementStudent", meta: { requiresAuth: true, requiredPermission: [enumaration.PERMISSION.Admin] } },
    { path: "/management-teacher", component: ManagementTeacher, name: "ManagementTeacher", meta: { requiresAuth: true, requiredPermission: [enumaration.PERMISSION.Admin] } },
    { path: "/management-score", component: ManagementScore, name: "ManagementScore", meta: { requiresAuth: true } },
    { path: "/statistic", component: StatisticPage, name: "StatisticPage", meta: { requiresAuth: true, requiredPermission: [enumaration.PERMISSION.Admin, enumaration.PERMISSION.Teacher] } },
    { path: "/management-user", component: ManagementUser, name: "ManagementUser", meta: { requiresAuth: true}},
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
        const userPermission = parseInt(sessionStorage.getItem('permission'));
        const requiredPermission = to.meta.requiredPermission; 
        // Nếu không có quyền truy cập
        if(requiredPermission && !requiredPermission.includes(userPermission)){
            next('/not-permission');
        }else{
            next(); // Cho phép điều hướng bình thường
        }
    }
}); 

export default vueRouter;
