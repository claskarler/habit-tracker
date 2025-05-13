import { createRouter, createWebHistory } from 'vue-router'
import Home from '../views/Home.vue'
import Auth from '../views/Auth/Auth.vue'
import Login from '../views/Auth/Login.vue'
import Register from '../views/Auth/Register.vue'
import Dashboard from '../views/Dashboard.vue'
import HabitDetails from '../views/Habits/HabitDetails.vue'
import Profile from '../views/Auth/Profile.vue'
import Habits from '../views/Habits/Habits.vue'


const routes = [
  {
    path: '/',
    name: 'Home',
    component: Home
  },
  {
    path: '/auth',
    name: 'Auth',
    component: Auth
  },
  {
    path: '/login',
    name: 'Login',
    component: Login
  },
  {
    path: '/register',
    name: 'Register',
    component: Register
  },
  {
    path: '/profile',
    name: 'Profile',
    component: Profile
  },
  {
    path: '/dashboard',
    name: 'Dashboard',
    component: Dashboard
  },
  {
    path: '/habits',
    name: 'Habits',
    component: Habits
  },
  {
    path: '/habit/:id',
    name: 'HabitDetails',
    component: HabitDetails
  }
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});
  export default router;