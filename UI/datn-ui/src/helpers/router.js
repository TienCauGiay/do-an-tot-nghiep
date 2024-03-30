import { createRouter, createWebHistory } from 'vue-router';

import HomePage from '@/components/HomePage.vue';
import LoginPage from '@/components/account/LoginPage.vue';
import RegisterPage from '@/components/account/SignupPage.vue';
import ManagementStudent from '@/views/management_student/ManagementStudent.vue';
import ManagementTeacher from '@/views/management_teacher/ManagementTeacher.vue';
import ManagementScore from '@/views/management_score/ManagementScore.vue';
import StatisticPage from '@/views/statistic_page/StatisticPage.vue';
import ManagementUser from '@/views/management_user/ManagementUser.vue';

const routes = [
  { path: '/', component: HomePage },
  { path: '/login', component: LoginPage },
  { path: '/register', component: RegisterPage },
  { path: "/management-student", component: ManagementStudent, name: "ManagementStudent" },
  { path: "/management-teacher", component: ManagementTeacher, name: "ManagementTeacher" },
  { path: "/management-score", component: ManagementScore, name: "ManagementScore" },
  { path: "/statistic", component: StatisticPage, name: "StatisticPage" },
  { path: "/management-user", component: ManagementUser, name: "ManagementUser" },
  { path: '/:pathMatch(.*)*', redirect: '/' } 
];

const router = createRouter({
  history: createWebHistory(),
  routes
});

router.beforeEach((to, from, next) => {
  const publicPages = ['/login', '/register'];
  const authRequired = !publicPages.includes(to.path);
  const loggedIn = localStorage.getItem('user');

  if (authRequired && !loggedIn) {
    return next('/login');
  }

  next();
});

export { router };

