<script setup>
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import axios from 'axios';

const username = ref('');
const email = ref('');
const password = ref('');
const confirmPassword = ref('');
const errorMessage = ref('');
const router = useRouter();

const handleRegister = async () => {
  if (password.value !== confirmPassword.value) {
    errorMessage.value = 'Passwords do not match!';
    return;
  }

  try {
    const response = await axios.post('https://localhost:7245/api/Auth/register', {
      username: username.value,
      email: email.value,
      password: password.value,
    });

    if (response.status === 200) {
      router.push('/login'); 
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
      <div class="register-container">
          <form @submit.prevent="handleRegister">
            <h1 class="text-center">Create an account</h1>
  
            <div class="form-area">
              <label for="username" class="form-input-label">Username</label>
              <input type="text" class="form-input" id="username" v-model="username" required />
            </div>
  
            <div class="form-area">
              <label for="email" class="form-input-label">Email</label>
              <input type="email" class="form-input" id="email" v-model="email" required />
            </div>
  
            <div class="form-area">
              <label for="password" class="form-input-label">Password</label>
              <input type="password" class="form-input" id="password" v-model="password" required />
            </div>
  
            <div class="form-area">
              <label for="confirmPassword" class="form-input-label">Confirm Password</label>
              <input type="password" class="form-input" id="confirmPassword" v-model="confirmPassword" required />
            </div>
  
            <button type="submit" class="btn-dark">Register</button>
  
            <div v-if="errorMessage" class="mt-3 alert alert-danger">{{ errorMessage }}</div>
          </form>
  
          <div class="text-center mt-3">
            <p>Already have an account? <router-link to="/login">Login here</router-link></p>
          </div>
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