<template>
  <div class="max-w-6xl mx-auto p-8">
    <h1 class="text-3xl font-bold mb-6 text-center">Task List</h1>

    <TaskFilters
      :users="users"
      :selectedUserId="selectedUserId"
      :isCompletedFilter="isCompletedFilter"
      :sortBy="sortBy"
      :isAscending="isAscending"
      @apply-filters="applyFilters"
    />

    <form @submit.prevent="addTask" class="flex items-center mb-6">
      <input
        v-model="newTask"
        type="text"
        placeholder="Enter a new task"
        class="flex-grow p-2 border rounded-md focus:outline-none focus:ring-2 focus:ring-green-500"
      />
      <button
        type="submit"
        class="ml-4 px-4 py-2 bg-green-500 text-white rounded-md hover:bg-green-600 transition"
      >
        Add Task
      </button>
    </form>

    <TaskTable
      :tasks="tasks"
      @toggle-completion="toggleTaskCompletion"
      @delete-task="deleteTask"
    />
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted, watch } from 'vue';
import { Task } from '../../models/Task';
import { User } from '../../models/User';
import { TaskQueryParams } from '../../models/TaskQueryParams';
import TaskService from '../../services/TaskService';
import UserService from '../../services/UserService';
import TaskFilters from './TaskFilters.vue';
import TaskTable from './TaskTable.vue';

const tasks = ref<Task[]>([]);
const users = ref<User[]>([]);
const newTask = ref<string>('');
const selectedUserId = ref<number | undefined>(undefined);
const sortBy = ref<string>('name');
const isAscending = ref<boolean>(true);
const isCompletedFilter = ref<boolean | undefined>(undefined);

const fetchTasks = async () => {
  const params: TaskQueryParams = {
    sortBy: sortBy.value,
    isAscending: isAscending.value,
    isCompleted: isCompletedFilter.value,
    userId: selectedUserId.value,
  };

  console.log('Fetching tasks with params:', params);

  tasks.value = await TaskService.getTasks(params);
};

const fetchUsers = async () => {
  try {
    users.value = await UserService.getAllUsers();
  } catch (error) {
    console.error('Error fetching users:', error);
  }
};

const addTask = async () => {
  if (!newTask.value.trim()) return;
  try {
    await TaskService.createTask({ name: newTask.value, isCompleted: false });
    newTask.value = '';
    await fetchTasks();
  } catch (error) {
    console.error('Error adding task:', error);
    alert('Failed to add task. Please try again.');
  }
};

const toggleTaskCompletion = async (task: Task) => {
  try {
    await TaskService.updateTask({ ...task, isCompleted: !task.isCompleted });
    await fetchTasks();
  } catch (error) {
    console.error('Error toggling task completion:', error);
    alert('Failed to update task. Please try again.');
  }
};

const deleteTask = async (id: number) => {
  try {
    await TaskService.deleteTask(id);
    await fetchTasks();
  } catch (error) {
    console.error('Error deleting task:', error);
    alert('Failed to delete task. Please try again.');
  }
};

const applyFilters = (filters: {
  selectedUserId: number | undefined;
  isCompletedFilter: boolean | undefined;
  sortBy: string;
  isAscending: boolean;
}) => {
  selectedUserId.value = filters.selectedUserId;
  isCompletedFilter.value = filters.isCompletedFilter;
  sortBy.value = filters.sortBy;
  isAscending.value = filters.isAscending;

  console.log('Filters applied:', filters);

  fetchTasks();
};

watch(tasks, (newTasks) => {
  console.log('Tasks updated:', newTasks);
});

onMounted(async () => {
  await fetchUsers();
  await fetchTasks();
});
</script>
