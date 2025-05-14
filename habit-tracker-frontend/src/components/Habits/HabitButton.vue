<script setup>
defineProps({
  habit: Object
});
</script>

<template>
  <router-link :to="`/habits/edit/${habit.id}`" class="btn-">
    {{ habit.name }}
  </router-link>
</template>
