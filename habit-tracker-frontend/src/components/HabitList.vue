<script setup>
import { onMounted } from 'vue'
import { useHabitStore } from '../stores/habits'

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
        {{ habit.name }} — {{ habit.targetCount }} times ({{ habit.scheduleType }})
      </li>
    </ul>
    <p v-else>No habits found.</p>
  </div>
</template>
