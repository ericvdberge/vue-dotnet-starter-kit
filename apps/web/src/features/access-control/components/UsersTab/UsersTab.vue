<script setup lang="ts">
import { toRef, onMounted } from 'vue';
import { useUsers } from '../../composables/useUsers';
import { useUsersTable } from '../../composables/useUsersTable';
import VirtualTable from '@/components/VirtualTable.vue';

const { getUsers } = useUsers();
const { data: users, isFetching, fetchNextPage, fetchPreviousPage } = getUsers();
const { table } = useUsersTable(users);

onMounted(async () => {
  // Load 2 pages for buffering
  await fetchNextPage();
  await fetchNextPage();
})

const handleReachEnd = async () => {
    console.log('handleReachEnd: isFetching=', isFetching.value);
    if (
        isFetching.value
    ) {
        return;
    }

    await fetchNextPage();
}

const handleReachStart = async () => {
    console.log('handleReachStart: isFetching=', isFetching.value);
    if (
        isFetching.value
    ) {
        return;
    }

    await fetchPreviousPage();
}
</script>

<template>
    <h1 class="text-lg font-bold mt-6">Users</h1>

    <VirtualTable
        :table="table"
        class="mt-5 h-110 w-full"
        @reach-end="handleReachEnd"
        @reach-start="handleReachStart" 
    />
</template>