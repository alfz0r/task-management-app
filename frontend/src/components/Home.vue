<template>
  <div class="text-center">
    <h1 class="text-3xl font-bold text-gray-800">Welcome to TaskManager</h1>
    <p class="text-gray-600 mt-4">
      Manage your tasks efficiently and stay organized.
    </p>
    <router-link
      to="/tasks"
      class="mt-6 inline-block bg-blue-600 text-white py-2 px-4 rounded hover:bg-blue-700"
    >
      View Tasks
    </router-link>
  </div>
</template>

<script>
export default {
  name: 'HomeView',
};
</script>
