<script setup>
import HabitList from '../components/Habits/HabitList.vue';
import LogoutButton from '../components/LogoutButton.vue';

</script>

<template>
  <div class="container mt-5">
    <h1>Dashboard</h1>
    <LogoutButton />
    <HabitList />
  </div>

</template>

<style scoped>
</style>
