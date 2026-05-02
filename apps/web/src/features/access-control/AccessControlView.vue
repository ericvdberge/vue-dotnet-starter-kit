<script setup lang="ts">
import DashboardLayout from '@/features/dashboard/layouts/DashboardLayout.vue';
import AccessControlTabs from './components/AccessControlTabs.vue';

</script>

<template>
    <DashboardLayout>
        <h1 class="text-2xl font-bold">Access Control</h1>

        <AccessControlTabs />
    </DashboardLayout>
</template>