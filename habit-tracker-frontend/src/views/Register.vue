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
      <div class="row justify-content-center mt-5">
        <div class="col-md-6">
          <form @submit.prevent="handleRegister" class="card p-4">
            <h3 class="text-center mb-4">Register</h3>
  
            <div class="mb-3">
              <label for="username" class="form-label">Username</label>
              <input type="text" class="form-control" id="username" v-model="username" required />
            </div>
  
            <div class="mb-3">
              <label for="email" class="form-label">Email</label>
              <input type="email" class="form-control" id="email" v-model="email" required />
            </div>
  
            <div class="mb-3">
              <label for="password" class="form-label">Password</label>
              <input type="password" class="form-control" id="password" v-model="password" required />
            </div>
  
            <div class="mb-3">
              <label for="confirmPassword" class="form-label">Confirm Password</label>
              <input type="password" class="form-control" id="confirmPassword" v-model="confirmPassword" required />
            </div>
  
            <button type="submit" class="btn btn-primary w-100">Register</button>
  
            <div v-if="errorMessage" class="mt-3 alert alert-danger">{{ errorMessage }}</div>
          </form>
  
          <div class="text-center mt-3">
            <p>Already have an account? <router-link to="/login">Login here</router-link></p>
          </div>
        </div>
      </div>
    </div>
  </template>