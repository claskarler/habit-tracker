<script setup>
defineProps({
  habit: Object
});
</script>

<template>
  <router-link :to="`/habit/${habit.id}`" class="btn btn-outline-primary">
    {{ habit.name }}
  </router-link>
</template>
