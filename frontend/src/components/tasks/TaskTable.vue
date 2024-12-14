<template>
  <div class="overflow-x-auto">
    <table class="w-full text-left border-collapse border border-gray-200">
      <thead class="bg-gray-100">
        <tr>
          <th class="p-2 border border-gray-300">#</th>
          <th class="p-2 border border-gray-300">Task Name</th>
          <th class="p-2 border border-gray-300">Created By</th>
          <th class="p-2 border border-gray-300">Created At</th>
          <th class="p-2 border border-gray-300">Modified By</th>
          <th class="p-2 border border-gray-300">Modified At</th>
          <th class="p-2 border border-gray-300">Status</th>
          <th class="p-2 border border-gray-300">Actions</th>
        </tr>
      </thead>
      <tbody>
        <tr
          v-for="(task, index) in tasks"
          :key="task.id"
          class="hover:bg-gray-50"
        >
          <td class="p-2 border border-gray-300">{{ index + 1 }}</td>
          <td class="p-2 border border-gray-300">
            <span :class="{ 'line-through text-gray-500': task.isCompleted }">{{
              task.name
            }}</span>
          </td>
          <td class="p-2 border border-gray-300">{{ task.createdBy }}</td>
          <td class="p-2 border border-gray-300">
            {{ formatDate(task.createdAt) }}
          </td>
          <td class="p-2 border border-gray-300">
            <span v-if="task.modifiedBy">{{ task.modifiedBy }}</span>
            <span v-else class="text-gray-400">N/A</span>
          </td>
          <td class="p-2 border border-gray-300">
            <span v-if="task.modifiedAt">{{
              formatDate(task.modifiedAt)
            }}</span>
            <span v-else class="text-gray-400">N/A</span>
          </td>
          <td class="p-2 border border-gray-300">
            <span
              :class="task.isCompleted ? 'text-green-600' : 'text-red-600'"
              class="font-bold"
            >
              {{ task.isCompleted ? 'Completed' : 'Pending' }}
            </span>
          </td>
          <td class="p-2 border border-gray-300 space-x-2">
            <button
              @click="$emit('toggle-completion', task)"
              :class="task.isCompleted ? 'text-green-600' : 'text-blue-600'"
              class="font-medium hover:underline"
            >
              {{ task.isCompleted ? 'Undo' : 'Complete' }}
            </button>
            <button
              @click="$emit('delete-task', task.id)"
              class="text-red-600 font-medium hover:underline"
            >
              Delete
            </button>
            <router-link
              :to="`/tasks/edit/${task.id}`"
              class="text-blue-500 font-medium hover:underline"
            >
              Edit
            </router-link>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script setup lang="ts">
import { defineProps, defineEmits } from 'vue';
import { Task } from '../../models/Task';

defineProps({
  tasks: {
    type: Array as () => Task[],
    required: true,
  },
});

defineEmits(['toggle-completion', 'delete-task']);

const formatDate = (dateString: string | undefined) => {
  if (!dateString) return '';
  const date = new Date(dateString);
  return date.toLocaleString();
};
</script>
