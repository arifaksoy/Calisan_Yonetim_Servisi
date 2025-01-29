<script setup lang="ts">
import { ref } from 'vue'

const welcomeMessage = ref('Welcome to Employee Management System')
</script>

<template>
  <v-container>
    <v-row>
      <v-col cols="12">
        <v-card>
          <v-card-title>{{ welcomeMessage }}</v-card-title>
          <v-card-text>
            Select an option from the sidebar to get started.
          </v-card-text>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<style scoped>
.v-card-title {
  font-size: 1.5rem;
  padding: 1rem;
}

.v-card-text {
  padding: 1rem;
  color: rgba(0, 0, 0, 0.6);
}
</style> 