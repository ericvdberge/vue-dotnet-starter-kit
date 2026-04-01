<template>
    <DashboardLayout>
        <h1 class="text-2xl font-bold">Dashboard</h1>
        <div class="grid auto-rows-min gap-4 md:grid-cols-3">
            <div class="aspect-video rounded-xl bg-muted/50" />
            <div class="aspect-video rounded-xl bg-muted/50" />
            <div class="aspect-video rounded-xl bg-muted/50" />
        </div>
        <div class="min-h-screen flex-1 rounded-xl bg-muted/50 md:min-h-min" />
    </DashboardLayout>
</template>

<script setup lang="ts">
import DashboardLayout from '@/layouts/DashboardLayout.vue';
</script>