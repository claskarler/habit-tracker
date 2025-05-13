<script setup>
import { useRouter } from 'vue-router';


</script>

<template>
    <div class="nav-menu">
        <button class="add-button">
            <i class="fa-solid fa-plus"></i>
        </button>
        <div class="menu">
            <div class="menu-group">
                <router-link to="/dashboard">
                    <i class="fa-solid fa-house"></i>
                </router-link>
                <router-link to="/overview">
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
        width: 60px;
        height: 60px;
        text-align: center;
        background-color: var(--blue);
        color: var(--dark);
        outline: 4px solid white;
        border: 3px solid var(--dark);
        border-radius: 50%;
        font-size: 1.5rem;
    }
    
    .add-button, i {
        color: white;
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
