import { createRouter, createWebHistory } from 'vue-router';
import LoginPage from '../components/login/LoginPage.vue';
import RegisterPage from '../components/register/RegisterPage.vue';
import TaskListPage from '../components/tasks/TaskListPage.vue';
import EditTaskPage from '../components/tasks/EditTaskPage.vue';
import ProfilePage from '../components/Profile/ProfilePage.vue';
import AuthService from '../services/AuthService';

const routes = [
  {
    path: '/',
    name: 'Login',
    component: LoginPage,
    beforeEnter: (_to: any, _from: any, next: any) => {
      if (AuthService.isAuthenticated()) {
        next('/tasks');
      } else {
        next();
      }
    },
  },
  {
    path: '/register',
    name: 'Register',
    component: RegisterPage,
  },
  {
    path: '/tasks',
    name: 'TaskList',
    component: TaskListPage,
    meta: { requiresAuth: true },
  },
  {
    path: '/profile',
    name: 'Profile',
    component: ProfilePage,
    meta: { requiresAuth: true },
  },
  {
    path: '/tasks/edit/:id',
    name: 'EditTask',
    component: EditTaskPage,
    props: true,
    meta: { requiresAuth: true },
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

router.beforeEach((to, _from, next) => {
  if (to.meta.requiresAuth && !AuthService.isAuthenticated()) {
    next('/');
  } else {
    next();
  }
});

export default router;
