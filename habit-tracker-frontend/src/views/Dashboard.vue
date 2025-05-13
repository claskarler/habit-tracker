<script setup>
import HabitList from '../components/Habits/HabitList.vue';
import LogoutButton from '../components/LogoutButton.vue';
import NavMenu from '../components/NavMenu.vue';

</script>

<template>
  <div class="container">
    <div class="content">
      <h1>Dashboard</h1>
      <LogoutButton />
      <HabitList />
      <div class="nav-menu">
        <NavMenu />
      </div>
    </div>
  </div>

</template>

<style scoped>
.content {
    display: flex;
    flex-direction: column;
    position: relative;
    height: 100vh; 
    padding: 10px 0;
    flex: 1;
  }

  .nav-menu {
    position: fixed;
    bottom: 30px;
    margin: 0 10px;
    left: 0;
    right: 0;
    z-index: 20; 
  }

</style>
