<template>
  <nav class="bg-primary text-white shadow w-full">
    <div class="container mx-auto flex justify-between items-center py-4 px-6">
      <router-link to="/" class="text-xl font-bold hover:text-accent">
        TaskManager
      </router-link>

      <ul class="flex space-x-6">
        <li>
          <router-link to="/tasks" class="hover:underline hover:text-accent">
            Tasks
          </router-link>
        </li>

        <li v-if="authStore.isAuthenticated">
          <router-link to="/profile" class="hover:underline hover:text-accent">
            Profile
          </router-link>
        </li>

        <li v-if="!authStore.isAuthenticated">
          <router-link to="/" class="hover:underline hover:text-accent">
            Login
          </router-link>
        </li>
        <li v-else>
          <button @click="logout" class="hover:underline hover:text-accent">
            Logout
          </button>
        </li>
      </ul>
    </div>
  </nav>
</template>

<script setup lang="ts">
import { useAuthStore } from '../stores/auth';
import { useRouter } from 'vue-router';

const authStore = useAuthStore();
const router = useRouter();

const logout = () => {
  authStore.logout();
  router.push('/');
};
</script>
