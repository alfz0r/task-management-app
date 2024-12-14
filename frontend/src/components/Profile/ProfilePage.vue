<template>
    <div class="flex items-center justify-center h-screen bg-gray-100">
      <div class="w-96 p-6 bg-white shadow-md rounded-md">
        <h1 class="text-2xl font-bold mb-4">Profile</h1>

        <div class="mb-6">
          <label class="block text-sm font-medium mb-2">Username</label>
          <p class="w-full p-2 border rounded-md bg-gray-50 text-gray-800">{{ username }}</p>
        </div>
  
        <form @submit.prevent="changePassword">
          <div class="mb-4">
            <label for="currentPassword" class="block text-sm font-medium">Current Password</label>
            <input
              id="currentPassword"
              type="password"
              v-model="currentPassword"
              class="w-full p-2 border rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
            />
          </div>
          <div class="mb-4">
            <label for="newPassword" class="block text-sm font-medium">New Password</label>
            <input
              id="newPassword"
              type="password"
              v-model="newPassword"
              class="w-full p-2 border rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
            />
          </div>
          <button
            type="submit"
            class="w-full p-2 bg-green-500 text-white rounded-md hover:bg-green-600 transition"
          >
            Change Password
          </button>
        </form>
  
        <p v-if="error" class="mt-4 text-red-500 text-sm">{{ error }}</p>
        <p v-if="success" class="mt-4 text-green-500 text-sm">{{ success }}</p>
      </div>
    </div>
  </template>
  
  <script setup lang="ts">
  import { ref, onMounted } from 'vue';
  import api from '../../services/api';
  
  const username = ref<string>('');
  const currentPassword = ref<string>('');
  const newPassword = ref<string>('');
  const error = ref<string>('');
  const success = ref<string>('');
  

  const fetchProfile = async () => {
    try {
      const response = await api.get('/user/profile');
      username.value = response.data.username;
    } catch {
      error.value = 'Failed to load profile. Please try again later.';
    }
  };

  const changePassword = async () => {
    try {
      await api.post('/user/change-password', {
        currentPassword: currentPassword.value,
        newPassword: newPassword.value,
      });
      success.value = 'Password changed successfully!';
      error.value = '';
  
      currentPassword.value = '';
      newPassword.value = '';
    } catch (err: any) {
      error.value = err.response?.data || 'Failed to change password.';
      success.value = '';
    }
  };

  onMounted(fetchProfile);
  </script>
  