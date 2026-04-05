<script setup lang="ts">
import DashboardLayout from '@/layouts/DashboardLayout.vue';
import { useAuth } from '@/composables/useAuth';
const { getToken } = useAuth();

const data = await fetch('http://localhost:5001/secure', {
    headers: {
        Authorization: `Bearer ${getToken()}`
    }
}).then(res => res.text());

</script>

<template>
    <DashboardLayout>
        <h1 class="text-2xl font-bold">Project View</h1>
        <p>{{ data }}</p>
    </DashboardLayout>
</template>