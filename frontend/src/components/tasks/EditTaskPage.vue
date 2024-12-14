<template>
    <div class="max-w-4xl mx-auto p-8">
      <h1 class="text-3xl font-bold mb-6 text-center">Edit Task</h1>
  
      <form @submit.prevent="updateTask" class="space-y-6">
        <div>
          <label for="name" class="block text-sm font-medium mb-2">Task Name</label>
          <input
            id="name"
            v-model="task.name"
            type="text"
            placeholder="Enter task name"
            class="w-full p-2 border rounded focus:outline-none focus:ring-2 focus:ring-blue-500"
          />
        </div>
  
        <div>
          <label for="status" class="block text-sm font-medium mb-2">Status</label>
          <select
            id="status"
            v-model="task.isCompleted"
            class="w-full p-2 border rounded focus:outline-none focus:ring-2 focus:ring-blue-500"
          >
            <option :value="true">Completed</option>
            <option :value="false">Pending</option>
          </select>
        </div>
  
        <button
          type="submit"
          class="w-full bg-green-500 text-white p-2 rounded hover:bg-green-600 transition"
        >
          Save Changes
        </button>
      </form>
    </div>
  </template>
  
  <script setup lang="ts">
  import { ref, onMounted } from 'vue';
  import { useRouter, useRoute } from 'vue-router';
  import api from '../../services/api';
  import { Task } from '../../models/Task';
  
  const task = ref<Task>({
    id: '',
    name: '',
    isCompleted: false,
    createdBy: '',
    createdAt: '',
    modifiedBy: null,
    modifiedAt: '',
  });

  const router = useRouter();
  const route = useRoute();

  const fetchTask = async () => {
    const taskId = route.params.id;
    try {
      const response = await api.get(`/tasks/${taskId}`);
      task.value = response.data;
    } catch (error) {
      console.error('Error fetching task:', error);
      router.push('/tasks');
    }
  };
  
  const updateTask = async () => {
    try {
      const updatedTask = {
        id: task.value.id,
        name: task.value.name,
        isCompleted: task.value.isCompleted,
      };
      await api.put(`/tasks/${task.value.id}`, updatedTask);
      router.push('/tasks');
    } catch (error) {
      console.error('Error updating task:', error);
      alert('Failed to update the task. Please try again.');
    }
  };

  onMounted(fetchTask);
  </script>
  