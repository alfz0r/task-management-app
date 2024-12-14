import { createRouter, createWebHistory } from 'vue-router';
import LoginPage from '../components/login/LoginPage.vue';
import RegisterPage from '../components/register/RegisterPage.vue';
import TaskListPage from '../components/tasks/TaskListPage.vue';
import EditTaskPage from '../components/tasks/EditTaskPage.vue';
import ProfilePage from '../components/Profile/ProfilePage.vue';

const routes = [
  {
    path: '/',
    name: 'Login',
    component: LoginPage,
    beforeEnter: (to, _from, next) => {
      const token = localStorage.getItem('token');
      if (token) {
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
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});


router.beforeEach((to, from, next) => {
  const token = localStorage.getItem('token');
  if (to.meta.requiresAuth && !token) {
    next('/');
  } else {
    next();
  }
});

export default router;
