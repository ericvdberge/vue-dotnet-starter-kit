<script setup lang="ts">
import { toRef, onMounted } from 'vue';
import { useUsers } from '../../composables/useUsers';
import { useUsersTable } from '../../composables/useUsersTable';
import VirtualTable from '@/components/VirtualTable.vue';

const { getUsers } = useUsers();
const { data: users, isFetching, fetchNextPage, fetchPreviousPage } = getUsers();
const { table } = useUsersTable(users);

onMounted(() => {
    if (!users.value) {
        fetchNextPage();
    }
});

const handleReachEnd = () => {
    fetchNextPage();
}

const handleReachStart = () => {
    fetchPreviousPage();
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