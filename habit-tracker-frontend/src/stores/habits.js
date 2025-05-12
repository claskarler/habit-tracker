
import axios from '../api/axios';
import { defineStore } from 'pinia';

export const useHabitStore = defineStore('habit', {
  state: () => ({
    habits: [],
  }),
  actions: {
    async fetchHabits() {
      try {
        const response = await axios.get('/habits');
        this.habits = response.data;
      } catch (error) {
        console.error('Failed to fetch habits:', error);
      }
    }
  }
});
