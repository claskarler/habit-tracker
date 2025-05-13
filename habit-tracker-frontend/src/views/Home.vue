<script setup>
import { useRouter } from 'vue-router';
</script>

<template>
  <div class="container">
    <div class="home-container">
        <div class="home-card">
            <h1 class="text-white">Let’s build some new habits!</h1>
            <h4 class="home-card-box">Are you ready?</h4>
        </div>
        <router-link to="/auth" class="router-link" >
            <button class="btn-dark">
                Start
            </button>
        </router-link>
    </div>
  </div>
</template>

<style scoped>
    .home-container {
        display: flex;
        flex-direction: column;
        height: 100vh; 
        padding: 10px 0;
    }

    .home-card {
        background-color: var(--green);
        border: 1px solid var(--dark);
        flex-grow: 1; 
        border-radius: 20px;
        padding: 1rem;
        position: relative;
        display: flex;
        flex-direction: column;
        justify-content: space-between;
        align-items: center;
    }

    .home-card-box {
        position: absolute;
        bottom: 1rem;
        right: 1rem;
        background-color: var(--yellow);
        border: 1px solid var(--dark);
        padding: 0.75rem 1.5rem;
        text-align: center;
        border-radius: 15px;
    }
    h1 {
        font-size: 2.2rem;
    }

</style>
