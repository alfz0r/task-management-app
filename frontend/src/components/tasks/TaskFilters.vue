<template>
  <div class="mb-6 flex flex-wrap items-center gap-4">
    <div class="flex-1">
      <label for="user" class="block mb-2 font-bold">Filter by User:</label>
      <select v-model="localSelectedUserId" class="p-2 border rounded w-full">
        <option :value="undefined">All Users</option>
        <option v-for="user in users" :key="user.id" :value="user.id">
          {{ user.username }}
        </option>
      </select>
    </div>

    <div class="flex-1">
      <label for="filter" class="block mb-2 font-bold"
        >Filter by Completion:</label
      >
      <select
        v-model="localIsCompletedFilter"
        class="p-2 border rounded w-full"
      >
        <option :value="undefined">All</option>
        <option :value="true">Completed</option>
        <option :value="false">Not Completed</option>
      </select>
    </div>

    <div class="flex-1">
      <label for="sort" class="block mb-2 font-bold">Sort by:</label>
      <div class="flex items-center gap-4">
        <select v-model="localSortBy" class="p-2 border rounded w-full">
          <option value="name">Name</option>
          <option value="createdAt">Created At</option>
        </select>
        <label class="flex items-center gap-2">
          <input type="checkbox" v-model="localIsAscending" />
          <span class="font-medium">Ascending</span>
        </label>
      </div>
    </div>

    <div class="flex-none self-end">
      <button @click="applyFilters" class="p-2 bg-blue-500 text-white rounded">
        Apply Filters
      </button>
    </div>
  </div>
</template>

<script setup lang="ts">
import { defineProps, defineEmits, ref } from 'vue';
import { User } from '../../models/User';

const props = defineProps({
  users: {
    type: Array as () => User[],
    required: true,
  },
  selectedUserId: {
    type: Number as () => number | undefined,
    required: true,
  },
  isCompletedFilter: {
    type: Boolean as () => boolean | undefined,
    required: true,
  },
  sortBy: {
    type: String as () => string,
    required: true,
  },
  isAscending: {
    type: Boolean,
    required: true,
  },
});

const localSelectedUserId = ref<number | undefined>(props.selectedUserId);
const localIsCompletedFilter = ref<boolean | undefined>(
  props.isCompletedFilter
);
const localSortBy = ref<string>(props.sortBy);
const localIsAscending = ref<boolean>(props.isAscending);

const emit = defineEmits(['apply-filters']);

const applyFilters = () => {
  emit('apply-filters', {
    selectedUserId: localSelectedUserId.value,
    isCompletedFilter: localIsCompletedFilter.value,
    sortBy: localSortBy.value,
    isAscending: localIsAscending.value,
  });
};
</script>
