<script setup>
import { onMounted, ref, computed } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { useHabitStore } from '../../stores/habits';
import axiosInstance from '../../api/axios';
import NavMenu from '../../components/NavMenu.vue';

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
 <div class="container">
    <div class="content">
      <div v-if="habit">
        <div class="habit-details" :style="{ backgroundColor: `var(--${habit.color})` }">
          <header class="habit-title">
          <i :class="habit.icon.cssClass" class="habit-icon" aria-hidden="true"></i>
          <h1>{{ habit.name }}</h1>
        </header>

        <section class="habit-info">
          <h6><strong>Target Count:</strong> {{ habit.targetCount }}</h6>
          <h6><strong>Schedule:</strong> {{ habit.scheduleType }}</h6>

          <h6 v-if="habit.daysOfWeek?.length">
            <strong>Days of Week:</strong> {{ habit.daysOfWeek.join(', ') }}
          </h6>

          <h6 v-if="habit.daysOfMonth?.length">
            <strong>Days of Month:</strong> {{ habit.daysOfMonth.join(', ') }}
          </h6>

        </section>
      
        </div>
        <div class="habit-actions">
          <router-link :to="`/habits/edit/${habit.id}`">
            <button class="btn-dark">Edit</button>
          </router-link>
          <button @click="deleteHabit" class="btn-red">Delete</button>
        </div>
      </div>
      

      <div v-else class="loading">
        <p>Loading habit...</p>
      </div>

      <nav class="nav-menu">
        <NavMenu />
      </nav>
    </div>
  </div>
</template>

<style scoped>
.habit-header {
  display: flex;
  align-items: center;
  margin-bottom: 20px;
}

.habit-details {
  border-radius: 15px;
  padding: 20px;
  border: 1px solid var(--dark);
}

.habit-title {
  display: flex;
  align-items: center;
  justify-content: space-between;
  padding: 20px;
  text-decoration: none;
  color: white;
  width: 100%;
  border-radius: 15px;
}

.habit-icon {
  font-size: 2.5rem;
}

.habit-info {
  flex: 1;
  color: white;
}

h6 {
  font-size: 1.2rem;
}

</style>
