
import axios from '../api/axios';
import { defineStore } from 'pinia';

export const useHabitStore = defineStore('habit', {
  state: () => ({
    habits: [],
    selectedHabit: null,
  }),
  actions: {
    async fetchHabits() {
      try {
        const response = await axios.get('/habits');
        this.habits = response.data;
      } catch (error) {
        console.error('Failed to fetch habits:', error);
      }
    },
    async fetchHabitById(id) {
      try {
        const response = await axios.get(`/habits/${id}`);
        this.selectedHabit = response.data;
        return response.data;
      } catch (error) {
        console.error(`Failed to fetch habit with ID ${id}:`, error);
      }
    }  
  }
});
