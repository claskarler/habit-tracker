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
      localStorage.setItem('jwtToken', response.data.token);
      localStorage.setItem('user', JSON.stringify(response.data.user));
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
      <div class="login-container">
        <form @submit.prevent="handleLogin" class="form">
          <h1 class="text-center">Welcome back!</h1>
          <div>
              <div class="form-area">
                <label for="email" class="form-input-label">Email</label>
                <input type="email" class="form-input" id="email" v-model="email" required />
              </div>
              <div class="form-area">
                <label for="password" class="form-input-label">Password</label>
                <input type="password" class="form-input" id="password" v-model="password" required />
              </div>
          </div>
          <button type="submit" class="btn-dark">Login</button>
        </form>
      </div>
    </div>
  </template>

<style scoped>

  h1 {
    margin: 5rem 2rem;
    font-size: 2.8rem;
  }

  form {
      height: 100%; 
      display: flex;
      flex-direction: column;
      justify-content: space-between;
  }

  .login-container {
      height: 100vh; 
      padding: 10px 0;
  }

</style>