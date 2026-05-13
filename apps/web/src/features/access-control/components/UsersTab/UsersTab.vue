<script setup lang="ts">
import { computed } from 'vue';
import { useUsers } from '../../composables/useUsers';
import { useUsersTable } from '../../composables/useUsersTable';
import VirtualTable from '@/components/VirtualTable.vue';

const { usersQuery } = useUsers();
const users = computed(() => usersQuery.data?.value || []);
const { table } = useUsersTable(users);
</script>

<template>
    <h1 class="text-lg font-bold mt-6">Users</h1>

    <VirtualTable
        :table="table"
        class="mt-5 h-110 w-full" 
    />
</template>