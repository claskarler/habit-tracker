<script setup>
import { ref, watch, onMounted } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import NavMenu from '../../components/NavMenu.vue';
import axiosInstance from '../../api/axios';

const route = useRoute()
const router = useRouter()
const icons = ref([])

const form = ref({
  name: '',
  targetCount: 1,
  scheduleType: 'daily',
  daysOfWeek: [],
  daysOfMonth: [],
  iconId: 1,
  color: 'blue'
})

const daysOfWeek = ['Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday', 'Sunday'];
const daysOfMonth = Array.from({ length: 31 }, (_, i) => i + 1);
const availableColors = ['blue', 'light-blue', 'green', 'lime', 'yellow', 'orange', 'red', 'pink', 'purple', 'light-gray'];

watch(() => form.value.scheduleType, (newType) => {
  if (newType !== 'weekly') {
    form.value.daysOfWeek = [];
  }
  if (newType !== 'monthly') {
    form.value.daysOfMonth = [];
  }
});

onMounted( async () => {
  const habitId = route.params.id
  if (habitId) {
    fetchHabitById(habitId)
  }
  try {
    const response = await axiosInstance.get('/icons')
    icons.value = response.data
  } catch (error) {
    console.error('Failed to fetch icons:', error)
  }
})

const fetchHabitById = async (id) => {
  try {
    const response = await axiosInstance.get(`/habits/${id}`)
    const habit = response.data
    console.log(response.data)
    form.value.name = habit.name
    form.value.targetCount = habit.targetCount
    form.value.scheduleType = habit.scheduleType
    form.value.daysOfWeek = habit.daysOfWeek
    form.value.daysOfMonth = habit.daysOfMonth 
    form.value.iconId = habit.icon.id
    form.value.color = habit.color
  } catch (error) {
    console.error(`Failed to fetch habit with ID ${id}:`, error)
  }
}

const handleFormSubmit = async () => {
  const habitId = route.params.id
  if (habitId) {
    updateHabit(habitId)
  } else {
    createHabit()
  }
}

const createHabit = async () => {
    try {
        console.log(form.value)
    const response = await axiosInstance.post('/habits', form.value);
    console.log('Habit created:', response.data);
    router.push('/dashboard')
  } catch (error) {
    console.error('Error creating habit:', error);
  }
};

const updateHabit = async () => {
  try {
    const response = await axiosInstance.put(`/habits/${route.params.id}`, form.value)
    console.log('Habit updated:', response.data)
    router.push('/dashboard')
  } catch (error) {
    console.error('Error updating habit:', error)
  }
}


</script>

<template>
    <div class="container">
        <div class="content">
            <form @submit.prevent="handleFormSubmit" class="form">
                <h1 v-if="route.params.id">Edit habit</h1>
                <h1 v-else>Create habit</h1>
                <div class="form-area">
                    <label>Name:</label>
                    <input v-model="form.name" required class="form-input" placeholder="Ex. 'Drink water'"/>
                </div>

                <div class="form-area">
                    <label>Target Count:</label>
                    <input type="number" v-model.number="form.targetCount" required min="1" class="form-input"/>
                </div>

                <div class="form-area">
                    <label>Schedule Type:</label>
                    <div class="pill-row">
                         <label class="pill-wrapper-horizontal">
                            <input type="radio" class="pill-input" value="daily" v-model="form.scheduleType" />
                            <span class="pill-box"></span>
                            <span class="pill-label-right">Daily</span>
                        </label>
                        <label class="pill-wrapper-horizontal">
                            <input type="radio" class="pill-input" value="weekly" v-model="form.scheduleType" />
                            <span class="pill-box"></span>
                            <span class="pill-label-right">Weekly</span>
                        </label>
                        <div v-if="form.scheduleType === 'weekly'" class="pill-grid">
                            <label v-for="day in daysOfWeek" :key="day" class="pill-wrapper-vertical">
                                <span class="pill-label">{{ day.charAt(0) }}</span>
                                <input type="checkbox" class="pill-input" :value="day" v-model="form.daysOfWeek" />
                                <div class="pill-box"></div>
                            </label>
                        </div>


                         <label class="pill-wrapper-horizontal">
                            <input type="radio" class="pill-input" value="monthly" v-model="form.scheduleType" />
                            <span class="pill-box"></span>
                            <span class="pill-label-right">Monthly</span>
                        </label>
                        <div v-if="form.scheduleType === 'monthly'" class="pill-row">
                            <label>Select Days of Month:</label>
                            <div class="pill-grid">
                                <label v-for="day in daysOfMonth" :key="day" class="pill-wrapper-vertical">
                                <span class="pill-label">{{ day }}</span>
                                <input type="checkbox" class="pill-input" :value="day" v-model="form.daysOfMonth" />
                                <div class="pill-box"></div>
                                </label>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="form-color">
                    <label>Select Color:</label>
                    <div class="color-picker">
                        <div class="color-preview" :style="{ backgroundColor: `var(--${form.color})` }"></div>
                        <div class="color-options">
                            <label
                            v-for="color in availableColors"
                            :key="color"
                            class="color-circle"
                            :class="{ checked: form.color === color }"
                            :style="{ backgroundColor: `var(--${color})` }"
                            >
                            <input type="radio" :value="color" v-model="form.color" />
                            </label>
                        </div>
                    </div>
                </div>

                <div class="form-icon">
                  <label>Select Icon:</label>
                  <div class="icon-picker">
                    <div class="icon-options">
                      <label v-for="icon in icons" :key="icon.id" class="icon" :class="{ checked: form.iconId === icon.id }" :style="{'--form-color': form.iconId === icon.id ? `var(--${form.color})` : ''}">
                        <i :class="icon.cssClass"></i>
                        <input type="radio" :value="icon.id" v-model="form.iconId" />
                      </label>
                    </div>
                  </div>
                </div>


                <button class="btn-green" type="submit">Save Habit</button>
            </form>
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

.pill-wrapper-horizontal {
  display: flex;
  align-items: center;
  gap: 0.5rem;
  flex-wrap: wrap;
  margin-top: 0.3rem;
}

.pill-wrapper-vertical {
  display: flex;
  flex-direction: column;
  align-items: center;
  gap: 0.2rem;
}

.pill-input {
  display: none;
}

.pill-row {
  display: flex;
  flex-direction: column;
  gap: 0.5rem;
  margin-top: 0.2rem;
}

.pill-grid {
  display: flex;
  justify-content: center;
  flex-wrap: wrap;
  gap: 0.3rem;
  padding-bottom: 10px;
  border-radius: 15px;
  border: 1px solid var(--dark);
}

.pill-wrapper {
  cursor: pointer;
  user-select: none;
  position: relative;
}

.pill-label {
  font-size: 0.7rem;
  margin-bottom: 0.2rem;
  color: var(--dark);
}

.day-pill-box {
    text-align: center;
}

.pill-box {
  width: 40px;
  height: 40px;
  border-radius: 15px;
  border: 1px solid var(--dark);
  display: flex;
  align-items: center;
  justify-content: center;
  font-weight: bold;
  font-size: 1.3rem;
  transition: all 0.2s ease;
}

.pill-input:checked + .pill-box {
  background-color: var(--light-blue);
}

.pill-input:checked + .pill-box::before {
  content: "✕";
  color: var(--dark);
}
.form-color {
  margin-top: 1rem;
}

.color-picker {
  display: flex;
  align-items: center;
  padding: 10px 20px;
  border: 1px solid var(--dark);
  border-radius: 15px;
  gap: 1rem;
  margin-bottom: 20px;
}

.color-preview {
  width: 60px;
  height: 60px;
  border-radius: 50%;
  flex-shrink: 0;
}

.color-options {
  display: grid;
  width: 100%;
  grid-template-columns: repeat(5, auto); 
  gap: 0.5rem;
  justify-content: center;
}

.color-circle {
  width: 30px;
  height: 30px;
  border-radius: 50%;
  border: 2px solid transparent;
  cursor: pointer;
  position: relative;
}

.color-circle input {
  display: none;
}

.color-circle.checked {
  border: 2px solid var(--dark);
}

.btn-green {
    margin-bottom: 120px;
}

.icon-options {
  display: grid;
  gap: 10px;
  width: 100%;
  grid-template-columns: repeat(5, auto); 
  justify-content: space-between;
  border: 1px solid var(--dark);
  padding: 10px;
  border-radius: 15px;
  margin-bottom: 10px;
}

.icon-options input {
  display: none;
}

.icon {
  display: flex;
  flex-direction: column;
  justify-content: center;
  font-size: 1.2rem;
  height: 40px;
  width: 40px;
  text-align: center;
}

.icon.checked {
  background-color: var(--form-color);
  border-radius: 15%;
}

</style>