<template>
  <form @submit.prevent="submit" class="mt-4">
    <input
      v-model="newTask"
      type="text"
      placeholder="New Task"
      class="p-2 border rounded w-64"
    />
    <button type="submit" class="ml-2 p-2 bg-green-500 text-white rounded">
      Add
    </button>
  </form>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import { defineEmits } from 'vue';

const emit = defineEmits<{
  (event: 'add', taskName: string): void;
}>();

const newTask = ref<string>('');

const submit = () => {
  if (!newTask.value.trim()) return;
  emit('add', newTask.value);
  newTask.value = '';
};
</script>
