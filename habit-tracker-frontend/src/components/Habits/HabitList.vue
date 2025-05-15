<script setup>
import { onMounted } from 'vue'
import { useHabitStore } from '../../stores/habits'

const habitStore = useHabitStore()

onMounted(() => {
  habitStore.fetchHabits()
})
</script>

<template>
  <div v-if="habitStore.habits.length">
    <div v-for="habit in habitStore.habits" :key="habit.id" class="habit" :style="{ backgroundColor: `var(--${habit.color})` }">
      <router-link :to="{ path: `/habits/${habit.id}` }" class="habit-link">
        <i :class="habit.icon.cssClass" class="habit-icon"></i>
        <h3 class="habit-name">{{ habit.name }}</h3>
        <i class="fa-solid fa-chevron-right habit-arrow"></i>
      </router-link>
    </div>
  </div>
  <p v-else>No habits found</p>
</template>

<style scoped>
  .habit {
    align-items: center;
    margin: 10px 0;
    border-radius: 15px;
    overflow: hidden;
  }

  .habit-link {
    display: flex;
    align-items: center;
    justify-content: space-between;
    padding: 20px;
    text-decoration: none;
    color: white;
    width: 100%;
  }

  .habit-icon {
    font-size: 32px;
    margin-right: 20px;
  }

  .habit-name {
    flex-grow: 1;
    font-size: 18px;
  }

  .habit-arrow {
    font-size: 18px;
    margin-left: 20px;
  }
</style>