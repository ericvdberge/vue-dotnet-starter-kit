<script setup lang="ts">
import { useSecureKeys } from '@/composables/useSecureKeys';
import DashboardLayout from '@/layouts/DashboardLayout.vue';

const { data: secureKeys, isLoading } = useSecureKeys();

</script>

<template>
    <DashboardLayout>
        <h1 class="text-2xl font-bold">Project View</h1>
        <ul v-if="!isLoading">
            <li v-for="value in secureKeys" :key="value">
                {{ value }}
            </li>
        </ul>
    </DashboardLayout>
</template>