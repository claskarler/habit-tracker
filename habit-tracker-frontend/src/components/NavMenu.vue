<script setup>
import { ref } from 'vue';
const showAddMenu = ref(false);

const toggleAddMenu = () => {
    showAddMenu.value = !showAddMenu.value;
}
</script>

<template>
    <div v-if="showAddMenu" class="dark-overlay" @click="toggleAddMenu"></div>
    <div v-if="showAddMenu" class="add-menu">
            <router-link to="/habits/create" class="router-link">
                <i class="fa-solid fa-plus"></i>
                <h3>New habit</h3>
            </router-link>
    </div>
    <div class="nav-menu">
        <button class="add-button" @click="toggleAddMenu">
            <i :class="['fa-solid', 'fa-plus', { 'rotated': showAddMenu }]"></i>
        </button>
        <div class="menu">
            <div class="menu-group">
                <router-link to="/dashboard">
                    <i class="fa-solid fa-house"></i>
                </router-link>
                <router-link to="/habits/overview">
                    <i class="fa-regular fa-calendar"></i>
                </router-link>
            </div>
            <div class="menu-space"></div>
            <div class="menu-group">
                <router-link to="/habits">
                    <i class="fa-solid fa-list-check"></i>
                </router-link>
                <router-link to="/profile">
                    <i class="fa-solid fa-user"></i>
                </router-link>
            </div>
        </div>
    </div>
</template>

<style scoped>
    .dark-overlay {
        position: fixed;
        inset: 0;
        background-color: rgba(0, 0, 0, 0.4);
        z-index: 2;
    }

    .add-menu {
        position: relative;
        z-index: 10;
        background-color: var(--dark);
        margin: 0 10px;
        color: white;
        border-radius: 15px;
        margin-bottom: 30px;
        padding: 20px 20px;
    }

    .router-link {
        display: flex;
        align-items: center;
    }
    .add-menu i {
        display: flex;
        justify-content: center;
        align-items: center;
        width: 60px;
        height: 60px;
        background-color: var(--green);
        border-radius: 50%;
        font-size: 1.5rem;
        margin-right: 20px;
    }

    .nav-menu {
        position: relative;
        display: flex;
        justify-content: center;
        align-items: center;
        margin: 0 10px;
    }

    .add-button {
        z-index: 10;
        position: relative;
        display: flex;
        justify-content: center;
        align-items: center;
        width: 65px;
        height: 65px;
        text-align: center;
        background-color: var(--blue);
        color: var(--dark);
        border: 5px solid var(--dark);
        border-radius: 50%;
        font-size: 1.5rem;
    }
    
    .add-button, i {
        color: white;
        transition: transform 0.3s ease;
    }
    .rotated {
        transform: rotate(45deg);
    }

    .menu {
        display: flex;
        justify-content: space-between;
        position: absolute;
        top: 30px;
        background-color: var(--dark);
        width: 100%;
        height: 50px;
        padding: 10px 0;
        border-radius: 15px;
    }

    .menu-group {
        display: flex;
        justify-content: space-between;
        width: 100%;
        margin: 0 35px;
    }

    .menu-space {
        width: 200px;
    }
</style>
