<script setup>
import TodayHabits from '../components/Habits/TodayHabits.vue';
import LogoutButton from '../components/LogoutButton.vue';
import NavMenu from '../components/NavMenu.vue';

</script>

<template>
  <div class="container">
    <div class="content">
      <TodayHabits />
      <div class="nav-menu">
        <NavMenu />
      </div>
    </div>
  </div>
</template>

<style scoped>
.container {
  padding-bottom: 150px;
}

.content {
    display: flex;
    flex-direction: column;
    position: relative;
    height: 100vh; 
    padding: 10px 0;
    flex: 1;
  }

  

</style>
