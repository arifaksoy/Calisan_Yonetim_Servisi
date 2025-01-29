<script setup lang="ts">
import { ref, onMounted } from 'vue'
import { useRouter, useRoute } from 'vue-router'
import { getTokenClaims, logout, getUserInfo } from '@/services/authService'

interface MenuItem {
  id: string
  title: string
  path: string
  icon: string
}

const props = defineProps<{
  menuItems: MenuItem[]
}>()

const router = useRouter()
const route = useRoute()
const userInfo = ref({
  firstName: '',
  lastName: '',
  role: '',
  companyName: '',
  username: ''
})

onMounted(() => {
  const claims = getTokenClaims()
  if (claims) {
    userInfo.value = {
      firstName: claims.FirstName,
      lastName: claims.LastName,
      role: claims.Role,
      companyName: claims.CompanyName,
      username: claims.sub
    }
  }
})

const handleLogout = () => {
  logout()
  router.push('/login')
}

const getInitials = () => {
  if (!userInfo.value.username) return 'U'
  return userInfo.value.username.substring(0, 2).toUpperCase()
}
</script>

<template>
  <div class="sidebar">
    <div class="user-info">
      <div class="avatar">{{ getInitials() }}</div>
      <div class="user-details">
        <div class="company-name">{{ userInfo.companyName }}</div>
        <div class="username">{{ userInfo.username }}</div>
      </div>
    </div>

    <nav class="menu">
      <router-link
        v-for="item in menuItems"
        :key="item.id"
        :to="item.path"
        class="menu-item"
        :class="{ active: route.path === item.path }"
      >
        <span class="material-icons">{{ item.icon }}</span>
        <span class="menu-text">{{ item.title }}</span>
      </router-link>
    </nav>

    <div class="logout" @click="handleLogout">
      <span class="material-icons">logout</span>
      <span class="menu-text">Logout</span>
    </div>
  </div>
</template>

<style scoped>
.sidebar {
  width: 250px;
  background-color: #2c3e50;
  color: white;
  height: 100vh;
  display: flex;
  flex-direction: column;
  flex-shrink: 0;
}

.user-info {
  padding: 16px;
  display: flex;
  align-items: center;
  gap: 12px;
  background-color: rgba(0, 0, 0, 0.2);
}

.avatar {
  width: 32px;
  height: 32px;
  background-color: #e74c3c;
  border-radius: 50%;
  display: flex;
  align-items: center;
  justify-content: center;
  font-size: 14px;
  font-weight: bold;
}

.user-details {
  font-size: 14px;
  line-height: 1.4;
}

.company-name {
  font-weight: 600;
  font-size: 14px;
}

.username {
  opacity: 0.7;
  font-size: 12px;
}

.menu {
  flex: 1;
  padding: 8px 0;
}

.menu-item {
  display: flex;
  align-items: center;
  padding: 12px 16px;
  color: white;
  text-decoration: none;
  gap: 12px;
  font-size: 14px;
  transition: background-color 0.2s;
}

.menu-item:hover, .menu-item.active {
  background-color: rgba(255, 255, 255, 0.1);
}

.material-icons {
  font-size: 20px;
  opacity: 0.8;
}

.menu-text {
  font-size: 14px;
}

.logout {
  padding: 12px 16px;
  display: flex;
  align-items: center;
  gap: 12px;
  cursor: pointer;
  font-size: 14px;
  transition: background-color 0.2s;
}

.logout:hover {
  background-color: rgba(255, 255, 255, 0.1);
}
</style> 