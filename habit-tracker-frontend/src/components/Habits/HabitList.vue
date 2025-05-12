<script setup>
import { onMounted } from 'vue'
import { useHabitStore } from '../../stores/habits'

import HabitButton from '../Habits/HabitButton.vue';

const habitStore = useHabitStore()

onMounted(() => {
  habitStore.fetchHabits()
})
</script>

<template>
  <div>
    <h2>Your Habits</h2>
    <ul v-if="habitStore.habits.length">
      <li v-for="habit in habitStore.habits" :key="habit.id">
         <HabitButton :habit="habit" />
      </li>
    </ul>
    <p v-else>No habits found.</p>
  </div>
</template>
