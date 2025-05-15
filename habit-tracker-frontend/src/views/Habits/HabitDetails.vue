<script setup>
import { onMounted, ref, computed } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { useHabitStore } from '../../stores/habits';
import axiosInstance from '../../api/axios';

const route = useRoute();
const router = useRouter();
const habitStore = useHabitStore();

const habit = ref(null);
const entries = ref([]);

onMounted(async () => {
  const id = route.params.id;
  habit.value = await habitStore.fetchHabitById(id);

  const res = await axiosInstance.get(`/habitentries/${id}`);
  entries.value = res.data;
});

const completedCount = computed(() => {
  if (!habit.value) return 0;
  return entries.value.filter(e => e.actualCount === habit.value.targetCount).length;
});

const deleteHabit = async () => {
  if (confirm('Are you sure you want to delete this habit?')) {
    await axiosInstance.delete(`/habits/${habit.value.id}`);
    router.push('/habits');
  }
};
</script>

<template>
  <div v-if="habit" class="habit-details">
    <div class="habit-header">
      <i :class="habit.icon.cssClass" class="habit-icon" :style="{ backgroundColor: `var(--${habit.color})` }"></i>
      <div class="habit-info">
        <h1>{{ habit.name }}</h1>
        <p>Target Count: {{ habit.targetCount }}</p>
        <p>Schedule: {{ habit.scheduleType }}</p>

        <p v-if="habit.daysOfWeek?.length">
          Days of Week: {{ habit.daysOfWeek.join(', ') }}
        </p>

        <p v-if="habit.daysOfMonth?.length">
          Days of Month: {{ habit.daysOfMonth.join(', ') }}
        </p>

        <p>You’ve fully completed this habit {{ completedCount }} times.</p>
      </div>
    </div>

    <div class="habit-actions">
      <router-link :to="{ path: `/habits/edit/${habit.id}` }">
          <button >Edit</button>
      </router-link>
      <button @click="deleteHabit" class="danger">Delete</button>
    </div>
  </div>

  <div v-else>
    <p>Loading habit...</p>
  </div>
</template>

<style scoped>
.habit-details {
  width: 100vw;
  min-height: 100dvh;
  padding: 20px;
  box-sizing: border-box;
  background: white;
  color: black;
}

.habit-header {
  display: flex;
  align-items: center;
  margin-bottom: 20px;
}

.habit-icon {
  font-size: 36px;
  padding: 15px;
  border-radius: 50%;
  color: white;
  margin-right: 20px;
  flex-shrink: 0;
}

.habit-info {
  flex: 1;
}

.habit-info h1 {
  margin: 0 0 10px;
  font-size: 26px;
}

.habit-actions {
  margin-top: 20px;
  display: flex;
  gap: 10px;
  flex-wrap: wrap;
}

button {
  padding: 10px 15px;
  border: none;
  border-radius: 10px;
  background: #f0f0f0;
  color: black;
  cursor: pointer;
}

button.danger {
  background: #ff5f5f;
  color: white;
}
</style>
