import { createRouter, createWebHistory } from 'vue-router'
import Login from '@/pages/Login.vue'
import Dashboard from '@/pages/Dashboard.vue'
import Unauthorized from '@/pages/Unauthorized.vue'
import TimeEntries from '@/pages/TimeEntries.vue'
import ReviewTimeEntries from '@/pages/ReviewTimeEntries.vue'
import Projects from '@/pages/Projects.vue'
import Company from '@/pages/Company.vue'
import EmployeeManagement from '@/pages/EmployeeManagement.vue'
import UserManagement from '@/pages/UserManagement.vue'
import SystemManagement from '@/pages/SystemManagement.vue'
import Pages from '@/pages/Pages.vue'
import Layout from '@/components/Layout.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/login',
      name: 'login',
      component: Login,
      meta: { requiresAuth: false }
    },
    {
      path: '/unauthorized',
      name: 'unauthorized',
      component: Unauthorized,
      meta: { requiresAuth: false }
    },
    {
      path: '/',
      component: Layout,
      children: [
        {
          path: '',
          redirect: '/dashboard'
        },
        {
          path: 'dashboard',
          name: 'dashboard',
          component: Dashboard
        },
        {
          path: 'company',
          name: 'company',
          component: Company
        },
        {
          path: 'employee',
          name: 'employeeManagement',
          component: EmployeeManagement
        },
        {
          path: 'projects',
          name: 'projects',
          component: Projects
        },
        {
          path: 'pages',
          name: 'pages',
          component: Pages
        },
        {
          path: 'time-entries',
          name: 'timeEntries',
          component: TimeEntries
        },
        {
          path: 'review-time-entries',
          name: 'reviewTimeEntries',
          component: ReviewTimeEntries
        },
        {
          path: 'user-management',
          name: 'userManagement',
          component: UserManagement
        },
        {
          path: 'system-management',
          name: 'systemManagement',
          component: SystemManagement
        }
      ],
      meta: { requiresAuth: true }
    }
  ]
})

// Navigation guard
router.beforeEach((to, from, next) => {
  const token = document.cookie
    .split('; ')
    .find(row => row.startsWith('auth_token='))
    ?.split('=')[1]
  const requiresAuth = to.meta.requiresAuth !== false // Default to true if not specified

  if (requiresAuth && !token) {
    next('/unauthorized')
  } else if (to.path === '/login' && token) {
    next('/dashboard')
  } else {
    next()
  }
})

export default router
