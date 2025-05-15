<script setup>
    import HabitList from '../../components/Habits/HabitList.vue';
    import NavMenu from '../../components/NavMenu.vue';
</script>

<template>

    <div class="container">
    <div class="content">
        <h1>Your habits:</h1>
        <HabitList />
        <div class="nav-menu">
            <NavMenu />
        </div>
    </div>
  </div>
</template>

<style scoped>
h1 {
    text-align: center;
}

.container {
    padding-bottom: 150px;
}
</style>