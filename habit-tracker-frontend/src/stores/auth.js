import { defineStore } from 'pinia';

export const useAuthStore = defineStore('auth', {
  state: () => ({
    user: JSON.parse(localStorage.getItem('user')) || null,
    token: localStorage.getItem('jwtToken') || null,
  }),
  actions: {
    login(userData) {
      this.user = userData.user;
      this.token = userData.token;
      localStorage.setItem('user', JSON.stringify(userData.user));
      localStorage.setItem('jwtToken', userData.token);
    },
    logout() {
      this.user = null;
      this.token = null;
      localStorage.removeItem('user');
      localStorage.removeItem('jwtToken');
    },
  },
});
