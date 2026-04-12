<template>
    <Suspense>
        <RouterView/>
        <template #fallback>
            <div class="flex items-center justify-center min-h-screen">
                <div class="text-lg text-gray-600">Loading...</div>
            </div>
        </template>
    </Suspense>
    <Toaster />
</template>

<script setup lang="ts">
import { RouterView } from 'vue-router';
import { Suspense } from 'vue';
import { useSecureClient } from './composables/useSecureClient';
import 'vue-sonner/style.css'
import { Toaster } from './components/ui/sonner';

useSecureClient();
</script>