<script setup>
import { useAuthStore } from '../stores/auth'

import HabitList from '../components/HabitList.vue';
import LogoutButton from '../components/LogoutButton.vue';

const authStore = useAuthStore();
const user = authStore.user;

</script>

<template>
  <div class="container mt-5">
    <h1>Dashboard</h1>
    <h2>Welcome, {{ user.username }}</h2>
    <LogoutButton />
    <HabitList />
  </div>

</template>

<style scoped>
</style>
