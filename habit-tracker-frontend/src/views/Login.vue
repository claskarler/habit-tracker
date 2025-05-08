<script setup>
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import axios from 'axios';

const email = ref('');
const password = ref('');
const errorMessage = ref('');
const router = useRouter();

const handleLogin = async () => {
  try {
    const response = await axios.post('https://localhost:7245/api/Auth/login', {
      email: email.value,
      password: password.value,
    });

    if (response.status === 200) {
      // Save token to localStorage or sessionStorage
      localStorage.setItem('jwtToken', response.data.token);

      // Redirect to the dashboard or home page
      router.push('/dashboard'); 
    }
  } catch (error) {
    if (error.response && error.response.data) {
      errorMessage.value = error.response.data;
    } else {
      errorMessage.value = 'An error occurred. Please try again later.';
    }
  }
};
</script>

<template>
    <div class="container">
      <div class="row justify-content-center mt-5">
        <div class="col-md-6">
          <form @submit.prevent="handleLogin" class="card p-4">
            <div class="mb-3">
              <label for="email" class="form-label">Email</label>
              <input type="email" class="form-control" id="email" v-model="email" required />
            </div>
            <div class="mb-3">
              <label for="password" class="form-label">Password</label>
              <input type="password" class="form-control" id="password" v-model="password" required />
            </div>
            <button type="submit" class="btn btn-primary w-100">Login</button>
          </form>
        </div>
      </div>
    </div>
  </template>

<style scoped>
</style>