<template>
  <div class="flex items-center justify-center h-screen bg-gray-100">
    <div class="w-96 p-6 bg-white shadow-md rounded-md">
      <h1 class="text-2xl font-bold mb-4">Login</h1>
      <form @submit.prevent="login">
        <div class="mb-4">
          <label for="username" class="block text-sm font-medium">Username</label>
          <input
            id="username"
            type="text"
            v-model="username"
            class="w-full p-2 border rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
          />
        </div>

        <div class="mb-4">
          <label for="password" class="block text-sm font-medium">Password</label>
          <input
            id="password"
            type="password"
            v-model="password"
            class="w-full p-2 border rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
          />
        </div>

        <button
          type="submit"
          class="w-full p-2 bg-blue-500 text-white rounded-md hover:bg-blue-600 transition"
        >
          Login
        </button>
      </form>

      <p v-if="error" class="mt-2 text-red-500 text-sm">{{ error }}</p>

      <p class="mt-4 text-sm text-center">
        Don't have an account?
        <router-link to="/register" class="text-blue-500 hover:underline">
          Register here
        </router-link>
      </p>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import { useAuthStore } from '../../stores/auth';
import api from '../../services/api';
import { AuthRequest } from '../../models/AuthRequest';

const username = ref<string>('');
const password = ref<string>('');
const error = ref<string>('');
const router = useRouter();
const authStore = useAuthStore();

const login = async () => {
  try {
    const payload: AuthRequest = {
      username: username.value,
      password: password.value,
    };

    const response = await api.post('/auth/login', payload);

    const token = response.data.token;
    authStore.login(token);

    router.push('/tasks');
  } catch (err: any) {
    error.value = err.response?.data?.message || 'Invalid credentials. Please try again.';
  }
};
</script>
