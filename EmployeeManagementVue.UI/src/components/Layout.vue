<script setup lang="ts">
import { ref, onMounted } from 'vue'
import { RouterView, useRouter } from 'vue-router'
import Sidebar from './Sidebar.vue'
import { getMyPages } from '@/services/pageService'
import { getTokenClaims } from '@/services/authService'

interface MenuItem {
  id: string
  title: string
  path: string
  icon: string
}

// Default icons mapping
const defaultIcons: { [key: string]: string } = {
  'Dashboard': 'dashboard',
  'Company': 'business',
  'Pages': 'description',
  'Employee': 'people',
  'Project': 'assignment',
  'Time Entries': 'schedule',
  'Review Time Entries': 'fact_check'
}

// URL mapping for pages
const urlMapping: { [key: string]: string } = {
  'Dashboard': '/dashboard',
  'Company': '/company',
  'Pages': '/pages',
  'Employee': '/employee',
  'Project': '/projects',
  'Time Entries': '/time-entries',
  'Review Time Entries': '/review-time-entries'
}

// Dashboard menu item that will always be present
const dashboardMenuItem = {
  id: 'dashboard',
  title: 'Dashboard',
  path: '/dashboard',
  icon: 'dashboard'
}

const router = useRouter()
const menuItems = ref([dashboardMenuItem])
const error = ref<string | null>(null)

onMounted(async () => {
  const token = document.cookie
    .split('; ')
    .find(row => row.startsWith('auth_token='))
    ?.split('=')[1]
  if (!token) {
    router.push('/unauthorized')
    return
  }

  try {
    const pages = await getMyPages()
    console.log('Fetched pages:', pages)
    
    if (Array.isArray(pages)) {
      const formattedMenuItems = pages
        .filter(page => page.status === 1)
        .map(page => ({
          id: page.pageId,
          title: page.pageName,
          path: urlMapping[page.pageName] || `/${page.pageName.toLowerCase().replace(/\s+/g, '-')}`,
          icon: defaultIcons[page.pageName] || 'article'
        }))
      console.log('Formatted menu items:', formattedMenuItems)
      menuItems.value = [dashboardMenuItem, ...formattedMenuItems]
    } else {
      error.value = 'Invalid data format received from server'
    }
  } catch (err) {
    console.error('Failed to fetch pages:', err)
    error.value = 'Failed to load menu items'
    router.push('/unauthorized')
  }
})
</script>

<template>
  <div class="layout">
    <Sidebar :menu-items="menuItems" />
    <div class="main-content">
      <RouterView v-slot="{ Component }">
        <component :is="Component" :key="$route.fullPath" />
      </RouterView>
    </div>
  </div>
</template>

<style scoped>
.layout {
  display: flex;
  min-height: 100vh;
  width: 100%;
  overflow: hidden;
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
}

.main-content {
  flex: 1;
  padding: 20px;
  background-color: #f5f6fa;
  overflow-y: auto;
  min-width: 0;
}

.page-title {
  font-size: 24px;
  margin-bottom: 20px;
  color: #2c3e50;
}
</style> 