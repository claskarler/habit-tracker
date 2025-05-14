<script setup>
import { useAuthStore } from '../stores/auth'
import { useRouter } from 'vue-router'

const auth = useAuthStore()
const router = useRouter()

const handleLogout = () => {
  auth.logout()
  router.push('/')
}
</script>

<template>
  <button class="btn-red" @click="handleLogout">Logout</button>
</template>

<style scoped>
  
</style>
