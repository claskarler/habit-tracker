<script setup>
import { onMounted, computed, ref, reactive } from 'vue'
import { useHabitStore } from '../../stores/habits'
import axiosInstance from '../../api/axios';

const habitEntries = ref([]);
const entryCounts = reactive({})

const habitStore = useHabitStore()

onMounted(async () => {
  await habitStore.fetchHabits()
  await fetchTodaysEntries()
})

const today = new Date()
const currentDayOfWeek = today.toLocaleDateString('en-US', { weekday: 'long' }) // veckodag
const currentDayOfMonth = today.getDate() // 1–31

const todaysHabits = computed(() => {
  return habitStore.habits.filter(habit => {
    switch (habit.scheduleType) {
      case 'daily':
        return true
      case 'weekly':
        return habit.daysOfWeek?.includes(currentDayOfWeek)
      case 'monthly':
        return habit.daysOfMonth?.includes(currentDayOfMonth)
      default:
        return false
    }
  })
})

const fetchTodaysEntries = async () => {
  try {
    const response = await axiosInstance.get('/habitentries/today')
    habitEntries.value = response.data;
    response.data.forEach(entry => {
      entryCounts[entry.habitId] = entry.actualCount
    })
  } catch (error) {
    console.error('Failed to fetch today\'s entries:', error)
  }
}

const handleCheckboxChange = (habitId, i) => {
  const current = entryCounts[habitId] || 0

  if (i <= current) {
    entryCounts[habitId] = i - 1
  } else {
    entryCounts[habitId] = i
  }

  markHabitDone(habitId, entryCounts[habitId])
}

const markHabitDone = async (habitId, actualCount) => {
  try {
    const existingEntry = habitEntries.value.find(e => e.habitId === habitId);

    if (existingEntry) {
      if (actualCount === 0) {
        await axiosInstance.delete(`/habitentries/${existingEntry.id}`);
        habitEntries.value = habitEntries.value.filter(e => e.id !== existingEntry.id);
        delete entryCounts[habitId];
      } else {
        await axiosInstance.put(`/habitentries/${existingEntry.id}`, { actualCount });
        existingEntry.actualCount = actualCount;
        entryCounts[habitId] = actualCount;
      }
    } else {
      if (actualCount > 0) {
        await axiosInstance.post('/habitentries', {
          habitId,
          actualCount,
          date: new Date().toISOString().split('T')[0]
        });
        await fetchTodaysEntries();
      }
    }
  } catch (error) {
    console.error('Failed to update habit entry:', error);
  }
};
</script>

<template>
    <div class="habits-container">
        <h1>Todays habits:</h1>
        <div v-for="habit in todaysHabits" :key="habit.id" class="habit" :style="{ backgroundColor: `var(--${habit.color})` }" >
            <i :class="habit.icon.cssClass"></i>
            <div class="habit-info">
                <h3>{{ habit.name }}</h3>
                <div class="checkboxes">
                    <label v-for="i in habit.targetCount" :key="i" class="habit-checkbox">
                        <input type="checkbox"
                            :checked="i <= (entryCounts[habit.id] || 0)"
                            @change="() => handleCheckboxChange(habit.id, i)" 
                            />
                    </label>
                </div>
            </div>
        </div>
    </div>
</template>

<style scoped>

    h1 {
        text-align: center;
        font-size: 2.4rem;
        font-weight: 600;
    }
    .habit {
        padding: 20px;
        margin: 10px 0;
        border-radius: 15px;
        display: flex;
        align-items: center;
        justify-content: space-between;
        color: white;
    }
    .habit i {
        font-size: 50px;
    }

    .habit-info {
        width: 100%;
        padding-left: 40px;
    }

    .checkboxes {
        display: grid;
        grid-template-columns: repeat(5, auto); 
        gap: 5px;
        
    }

    .habit-checkbox {
        width: 30px;
        height: 30px;
        border-radius: 50%;
        border: 2px solid white;
    }

    .habit-checkbox input {
        display: none;
    }

    .habit-checkbox:has(input:checked) {
        background-color: white;
    }
</style>
