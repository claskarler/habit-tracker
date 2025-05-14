<script setup>
import { onMounted, ref } from 'vue';
import { useRoute } from 'vue-router';
import { useHabitStore } from '../../stores/habits';

const route = useRoute();
const habitStore = useHabitStore();
const habit = ref(null);

onMounted(async () => {
  const id = route.params.id;
  habit.value = await habitStore.fetchHabitById(id);
});
</script>

<template>
  <div v-if="habit">
    <h1>{{ habit.name }}</h1>
    <p>Target Count: {{ habit.targetCount }}</p>
    <p>Schedule Type: {{ habit.scheduleType }}</p>

    <p v-if="habit.daysOfWeek && habit.daysOfWeek.length > 0">
      Days of the Week: {{ habit.daysOfWeek.join(', ') }}
    </p>

    <p v-if="habit.daysOfMonth && habit.daysOfMonth.length > 0">
      Days of the Month: {{ habit.daysOfMonth.join(', ') }}
    </p>
  </div>
  
  <div v-else>
    <p>Loading habit...</p>
  </div>
</template>
