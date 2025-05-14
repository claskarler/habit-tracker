
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
    },
    async createHabit(payload) {
      try {
        const response = await axios.post('/habits', payload);
        this.habits.push(response.data);
        return response.data;
      } catch (error) {
        console.error('Failed to create habit:', error);
        throw error;
      }
    },
    async deleteHabit(id) {
      try {
        await axios.delete(`/habits/${id}`);
        this.habits = this.habits.filter(habit => habit.id !== id);
        if (this.selectedHabit?.id === id) {
          this.selectedHabit = null;
        }
      } catch (error) {
        console.error(`Failed to delete habit with ID ${id}:`, error);
        throw error;
      }
    }  
  }
});
