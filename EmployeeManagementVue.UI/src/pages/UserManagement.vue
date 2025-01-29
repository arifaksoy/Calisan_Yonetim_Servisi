<script setup lang="ts">
import { ref, onMounted } from 'vue'
import { getMyPages } from '@/services/pageService'

const pageTitle = ref('User Management')

const loadPageTitle = async () => {
  try {
    const pages = await getMyPages()
    const userManagementPage = pages.find(page => page.url?.includes('user-management'))
    if (userManagementPage) {
      pageTitle.value = userManagementPage.pageName
    }
  } catch (err) {
    console.error('Error loading page title:', err)
  }
}

onMounted(() => {
  loadPageTitle()
})
</script>

<template>
  <div class="user-management-container">
    <v-container fluid>
      <v-row>
        <v-col cols="12">
          <h1 class="text-h4">{{ pageTitle }}</h1>
        </v-col>
      </v-row>
    </v-container>
  </div>
</template>

<style scoped>
.user-management-container {
  padding: 20px;
}
</style> 