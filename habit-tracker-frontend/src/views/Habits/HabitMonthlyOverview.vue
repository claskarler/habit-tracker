<script setup>
import HabitsMonth from '../../components/Habits/HabitMonth.vue';
import NavMenu from '../../components/NavMenu.vue';
</script>

<template>
    <div class="container">
        <div class="content">
        <HabitsMonth />
        <div class="nav-menu">
            <NavMenu />
        </div>
        </div>
  </div>
</template>

<style scoped>
.content {
    justify-content: start;
}
</style>