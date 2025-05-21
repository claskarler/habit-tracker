<script setup>
import { onMounted, ref, computed } from 'vue';
import { useHabitStore } from '../../stores/habits';
import axiosInstance from '../../api/axios';

const habitStore = useHabitStore();
const monthDays = ref([]);
const habitEntries = ref([]);
const today = new Date();

const getCurrentMonthDays = () => {
  const year = today.getFullYear();
  const month = today.getMonth();
  const days = [];

  const firstDay = new Date(year, month, 1);
  const lastDay = new Date(year, month + 1, 0);

  const startDayIndex = (firstDay.getDay() + 6) % 7;

  for (let i = 0; i < startDayIndex; i++) {
    days.push(null);
  }

  for (let day = 1; day <= lastDay.getDate(); day++) {
    days.push(new Date(year, month, day));
  }

  return days;
};

const fetchEntries = async () => {
  await habitStore.fetchHabits();
  const res = await axiosInstance.get('/habitentries');
  habitEntries.value = res.data;
};

onMounted(async () => {
  monthDays.value = getCurrentMonthDays();
  await fetchEntries();
});

const getCompletionColor = (date) => {
  const dateStr = date.toLocaleDateString('sv-SE'); // YYYY-MM-DD
  const isFuture = date > new Date().setHours(0, 0, 0, 0); 

  const scheduledHabits = habitStore.habits.filter(habit => {
    if (habit.scheduleType === 'daily') return true;
    if (habit.scheduleType === 'weekly') {
      const weekday = date.toLocaleDateString('en-US', { weekday: 'long' });
      return habit.daysOfWeek?.includes(weekday);
    }
    if (habit.scheduleType === 'monthly') {
      return habit.daysOfMonth?.includes(date.getDate());
    }
    return false;
  });

  if (!scheduledHabits.length) return '';

  if (isFuture) return 'blue';

  const entriesForDay = habitEntries.value.filter(e => e.date.split('T')[0] === dateStr);

  let completed = 0;
  for (const habit of scheduledHabits) {
    const entry = entriesForDay.find(e => e.habitId === habit.id);
    if (entry?.actualCount >= habit.targetCount) {
      completed++;
    }
  }

  if (completed === scheduledHabits.length) return 'green';
  if (completed > 0) return 'yellow';
  return 'red';
};
</script>

<template>
    <h2>{{ today.toLocaleDateString('en-US', { month: 'long', year: 'numeric' }) }}</h2>
  <div class="calendar">
    
    <div class="weekday-labels">
        <div v-for="d in ['M', 'T', 'W', 'Th', 'F', 'Sa', 'S']" :key="d" class="day-label">
            {{ d }}
        </div>
    </div>


    <div class="calendar-grid">
        <div
            v-for="day in monthDays"
            :key="day?.toDateString?.() || Math.random()"
            class="pill-wrapper-vertical"
        >
            <div v-if="day" 
                class="pill-box"
                :class="[
                getCompletionColor(day),
                day.toDateString() === new Date().toDateString() ? 'today' : ''
                ]">
            {{ day.getDate() }}
            </div>
            <div v-else class="pill-placeholder"></div>
        </div>
    </div>
  </div>
</template>

<style scoped>

h2 {
    text-align: center;
}

.calendar {
  width: 100%;
  text-align: center;
  padding: 10px;
  border-radius: 15px;
  border: 1px solid var(--dark);
}

.weekday-labels {
  display: grid;
  grid-template-columns: repeat(7, 1fr);
  margin-bottom: 8px;
  font-weight: bold;
  font-size: 0.85rem;
}

.calendar-grid {
  display: grid;
  grid-template-columns: repeat(7, 1fr);
  gap: 0.3rem;
}

.pill-wrapper-vertical {
  display: flex;
  flex-direction: column;
  align-items: center;
  gap: 0.2rem;
}

.pill-box {
  width: 40px;
  height: 40px;
  border-radius: 15px;
  border: 1px solid var(--dark);
  display: flex;
  align-items: center;
  justify-content: center;
  transition: all 0.2s ease;
}

.pill-placeholder {
  width: 40px;
  height: 40px;
  visibility: hidden;
}


.pill-box.green {
  background-color: var(--green);
}

.pill-box.yellow {
  background-color: var(--yellow);
}

.pill-box.red {
  background-color: var(--red);
}

.pill-box.blue {
  background-color: var(--light-blue);
}

.pill-box.today {
  border: 2px solid black;
}

.day-label {
    color: var(--light-gray);
    padding-bottom: 2px;
    border-bottom: 2px solid var(--light-gray);
}
</style>

