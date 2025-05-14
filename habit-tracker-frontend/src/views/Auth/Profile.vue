<script setup>
import { ref, watch } from 'vue';
import { useAuthStore } from '../../stores/auth';
import axiosInstance from '../../api/axios';
import NavMenu from '../../components/NavMenu.vue';
import LogoutButton from '../../components/LogoutButton.vue';

const authStore = useAuthStore();
const username = ref(authStore.user?.username || '');
const email = ref(authStore.user?.email || '');

watch(() => authStore.user, (newUser) => {
  if (newUser) {
    username.value = newUser.username;
    email.value = newUser.email;
  }
}, { immediate: true });

const oldPassword = ref('');
const newPassword = ref('');
const confirmNewPassword = ref('');

const updateProfile = async () => {
  try {
    const response = await axiosInstance.put('/auth/update', {
      username: username.value,
      email: email.value
    });
    authStore.user.username = username.value;
    authStore.user.email = email.value;
    localStorage.setItem('user', JSON.stringify(authStore.user));

    console.log('Profile updated successfully.');
  } catch (error) {
    console.error(error);
    console.log('Failed to update profile.');
  }
};

const changePassword = async () => {
  if (newPassword.value !== confirmNewPassword.value) {
    console.log('New passwords do not match.');
    return;
  }

  try {
    await axiosInstance.put('/auth/change-password', {
      currentPassword: oldPassword.value,
      newPassword: newPassword.value
    });

    console.log('Password changed successfully.');
    oldPassword.value = '';
    newPassword.value = '';
    confirmNewPassword.value = '';
  } catch (error) {
    console.error(error);
    console.log(error.response?.data || 'Failed to change password.');
  }
};
</script>

<template>
  <div class="container">
    <div class="content">
      <h1>Profile</h1>
      
      <div class="nav-menu">
        <NavMenu />
      </div>
      
      <form @submit.prevent="updateProfile">
        <div class="form-area">
          <label for="username" >Username</label>
          <input
            type="text"
            id="username"
            v-model="username"
            placeholder="Enter your username"
            required
            class="form-input"
          />
        </div>

        <div class="form-area">
          <label for="email">Email</label>
          <input
            type="email"
            id="email"
            v-model="email"
            placeholder="Enter your email"
            required
            class="form-input"
          />
        </div>

        <button type="submit" class="btn-green">Update Profile</button>
      </form>

      <form @submit.prevent="changePassword">
        <div class="form-area">
          <label for="oldPassword">Old Password</label>
          <input
            type="password"
            id="oldPassword"
            v-model="oldPassword"
            placeholder="Enter your old password"
            required
            class="form-input"
          />
        </div>

        <div class="form-area">
          <label for="newPassword">New Password</label>
          <input
            type="password"
            id="newPassword"
            v-model="newPassword"
            placeholder="Enter new password"
            required
            class="form-input"
          />
        </div>

        <div class="form-area">
          <label for="confirmNewPassword">Confirm New Password</label>
          <input
            type="password"
            id="confirmNewPassword"
            v-model="confirmNewPassword"
            placeholder="Confirm new password"
            required
            class="form-input"
          />
        </div>

        <button type="submit" class="btn-green">Change Password</button>
      </form>

      <div class="logout-area">
        <LogoutButton />
      </div>
    </div>
  </div>
</template>

<style scoped>
 .logout-area {
  padding-bottom: 100px;
 }
</style>