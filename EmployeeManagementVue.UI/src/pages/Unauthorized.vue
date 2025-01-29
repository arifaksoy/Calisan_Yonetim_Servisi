<script setup lang="ts">
import { useRouter } from 'vue-router'

const router = useRouter()

const goToLogin = () => {
  router.push('/login')
}
</script>

<template>
  <div class="unauthorized-container">
    <div class="unauthorized-content">
      <h1 class="title">Unauthorized Access</h1>
      <p class="message">You don't have permission to access this page.</p>
      <v-btn
        color="primary"
        @click="goToLogin"
        class="login-button"
      >
        Go to Login
      </v-btn>
    </div>
  </div>
</template>

<style scoped>
.unauthorized-container {
  display: flex;
  justify-content: center;
  align-items: center;
  height: 100%;
  width: 100%;
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background-color: #f5f6fa;
}

.unauthorized-content {
  background-color: white;
  padding: 40px;
  border-radius: 8px;
  box-shadow: 0 2px 12px rgba(0, 0, 0, 0.1);
  text-align: center;
  max-width: 400px;
  width: 100%;
  margin: 0 20px;
}

.title {
  color: #e74c3c;
  font-size: 28px;
  margin-bottom: 20px;
  font-weight: 600;
}

.message {
  color: #2c3e50;
  font-size: 16px;
  margin-bottom: 30px;
  line-height: 1.5;
}

.login-button {
  min-width: 150px;
}
</style> 