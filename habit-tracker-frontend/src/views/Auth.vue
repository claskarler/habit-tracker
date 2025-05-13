<script setup>

</script>

<template>
  <div class="container">
      <div class="auth-container">
        <div class="habit-tracker">
          <h2>Habit Tracker</h2>
        </div>
        <div>
          <router-link to="/register">
            <button class="btn-green">
              Register
            </button>
          </router-link>
          <router-link to="/login">
            <button class="btn-dark">
              Login
            </button>
          </router-link>
        </div>
      </div>
  </div>
</template>

<style scoped>
  .auth-container {
    display: flex;
    flex-direction: column;
    justify-content: space-between;
    height: 100vh; 
    padding: 10px 0;
  }

  .habit-tracker {
    background-color: var(--pink);
    width: 200px;
    height: 200px;
    border-radius: 50%;
    display: flex;
    align-items: center;
    justify-content: center;
    text-align: center;
    margin: 2rem auto; 
  }
</style>
