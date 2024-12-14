<template>
  <div class="flex items-center justify-center h-screen bg-gray-100">
    <div class="w-96 p-6 bg-white shadow-md rounded-md">
      <h1 class="text-2xl font-bold mb-4">Register</h1>
      <form @submit.prevent="register">
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

        <div class="mb-4">
          <label for="confirmPassword" class="block text-sm font-medium">Confirm Password</label>
          <input
            id="confirmPassword"
            type="password"
            v-model="confirmPassword"
            class="w-full p-2 border rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
          />
        </div>

        <button
          type="submit"
          class="w-full p-2 bg-green-500 text-white rounded-md hover:bg-green-600 transition"
        >
          Register
        </button>
      </form>

      <p v-if="error" class="mt-4 text-red-500 text-sm">{{ error }}</p>
      <p v-if="success" class="mt-4 text-green-500 text-sm">{{ success }}</p>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import api from '../../services/api';

const username = ref<string>('');
const password = ref<string>('');
const confirmPassword = ref<string>('');
const error = ref<string>('');
const success = ref<string>('');
const router = useRouter();

const register = async (): Promise<void> => {
  if (password.value !== confirmPassword.value) {
    error.value = 'Passwords do not match!';
    success.value = '';
    return;
  }

  try {
    await api.post('/auth/register', { username: username.value, password: password.value });
    success.value = 'Registration successful! Redirecting to login...';
    error.value = '';

    setTimeout(() => {
      router.push('/');
    }, 2000);
  } catch (err: any) {
    error.value = err.response?.data || 'An error occurred during registration.';
    success.value = '';
  }
};
</script>
